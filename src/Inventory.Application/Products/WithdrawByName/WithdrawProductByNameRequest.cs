using Inventory.Domain.Extensions;
using Inventory.Domain.UseCases;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.Application.Products.WithdrawByName
{
    public class WithdrawProductByNameRequest : IRequest<WithdrawProductByNameResponse>
    {
        public string Name { get; set; }

        public WithdrawProductByNameRequest(string name)
        {
            if(name.IsNullOrEmpty())
            {
                throw new ArgumentNullException(nameof(name));
            }

            Name = name;
        }
    }
}