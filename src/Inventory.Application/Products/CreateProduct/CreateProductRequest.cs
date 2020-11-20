﻿using System;
using System.Collections.Generic;
using System.Text;
using Inventory.Domain.Extensions;
using Inventory.Domain.UseCases;

namespace Inventory.Application.Products.CreateProduct
{
    public class CreateProductRequest : IRequest<CreateProductResponse>
    {
        public string Name { get; }
        public DateTime? ExpirationDate { get; set; }
        public string? ProductType { get; set; }

        public CreateProductRequest(string name)
        {
            if(name.IsNullOrEmpty())
            {
                throw new ArgumentNullException(nameof(name));
            }

            Name = name;
        }
    }
}