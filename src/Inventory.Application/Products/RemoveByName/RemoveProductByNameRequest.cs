using Inventory.Domain.Extensions;
using Inventory.Domain.UseCases;
using System;

namespace Inventory.Application.Products.RemoveByName
{
    public class RemoveProductByNameRequest : IRequest<RemoveProductByNameResponse>
    {
        public string Name { get; set; }

        public RemoveProductByNameRequest(string name)
        {
            if(name.IsNullOrEmpty())
            {
                throw new ArgumentNullException(nameof(name));
            }

            Name = name;
        }
    }
}