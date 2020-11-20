using FluentValidation.AspNetCore;
using Inventory.API.Filters;
using Inventory.Application;
using Inventory.Application.Products.CreateProduct;
using Inventory.Domain.DateTimeProvider;
using Inventory.Domain.Persistence;
using Inventory.Domain.UseCases;
using Inventory.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            services.AddDbContext<InventoryDbContext>(options =>
            {
                options.UseInMemoryDatabase("Inventory");
            });

            services.AddControllers(options =>
            {
                options.Filters.Add(typeof(GlobalExceptionFilter));
                options.SuppressAsyncSuffixInActionNames = false;
            }).AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Startup>());

            services.AddSwaggerGen();

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

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
