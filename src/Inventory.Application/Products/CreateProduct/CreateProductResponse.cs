using Inventory.Domain.UseCases;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.Application.Products.CreateProduct
{
    public class CreateProductResponse : BaseResponse
    {
        public int Id { get; set; }

        public CreateProductResponse(int id)
        {
            Id = id;
        }
    }
}