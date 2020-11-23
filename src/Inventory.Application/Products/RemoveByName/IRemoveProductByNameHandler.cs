using Inventory.Domain.UseCases;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.Application.Products.RemoveByName
{
    public interface IRemoveProductByNameHandler : IUseCaseHandler<RemoveProductByNameRequest, RemoveProductByNameResponse>
    {
    }
}