using FluentValidation.AspNetCore;
using Hangfire;
using Inventory.API.Infrastructure.BackgroundJobs;
using Inventory.API.Infrastructure.Filters;
using Inventory.Application;
using Inventory.Application.Products.CreateProduct;
using Inventory.Domain.DateTimeProvider;
using Inventory.Domain.Events;
using Inventory.Domain.Persistence;
using Inventory.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Security.Claims;
using System.Text;

namespace Inventory.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var key = Encoding.ASCII.GetBytes(Configuration.GetValue<string>("SecretKey"));

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            IdentityModelEventSource.ShowPII = true;

            services.AddDbContext<InventoryDbContext>(options =>
            {
                options.UseInMemoryDatabase("Inventory");
            });

            services.AddHangfire(config =>
            {
                config.UseIgnoredAssemblyVersionTypeResolver().UseInMemoryStorage();
            });

            services.AddControllers(options =>
            {
                options.Filters.Add(typeof(GlobalExceptionFilter));
                options.SuppressAsyncSuffixInActionNames = false;
            }).AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Startup>());

            services.AddSwaggerGen(config =>
            {
                config.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Inventory API",
                    Version = "v1"
                });

                config.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Enter your token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    BearerFormat = "Bearer {JWT token}"
                });

                config.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });

            services.Scan(scan =>
            {
                scan.FromAssemblyOf<CreateProductRequest>()
                    .AddClasses(classes => classes.Where(x => x.IsClass && x.Name.EndsWith("Handler")))
                    .AsImplementedInterfaces()
                    .WithTransientLifetime();

                scan.FromAssemblyOf<InventoryDbContext>()
                    .AddClasses(classes => classes.Where(x => x.IsClass && x.Name.EndsWith("Repository")))
                    .AsImplementedInterfaces()
                    .WithTransientLifetime();
            });

            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
            services.AddSingleton<IEventBus, NullEventBus>();
            services.AddScoped<IAuditUser>(provider =>
            {
                var contextAccessor = provider.GetRequiredService<IHttpContextAccessor>();

                var user = contextAccessor?.HttpContext?.User;

                string? username = null;

                if(user is ClaimsPrincipal && user.Identity.IsAuthenticated)
                {
                    username = user.Identity.Name;
                }

                return new AuditUser(username);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Inventory API");
            });

            app.UseHangfireServer();
            RecurringJob.AddOrUpdate<NotifyExpiredProductsJob>(nameof(NotifyExpiredProductsJob), x => x.Run(), Cron.Daily(1));

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
