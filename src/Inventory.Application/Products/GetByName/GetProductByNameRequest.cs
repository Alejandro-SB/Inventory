using Inventory.Domain.UseCases;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.Application.Products.GetByName
{
    public class GetProductByNameRequest : IRequest<GetProductByNameResponse>
    {
        public string Name { get; set; }

        public GetProductByNameRequest(string name)
        {
            Name = name;
        }
    }
}