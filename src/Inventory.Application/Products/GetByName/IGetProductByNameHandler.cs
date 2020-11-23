using Inventory.Domain.UseCases;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.Application.Products.GetByName
{
    public interface IGetProductByNameHandler : IUseCaseHandler<GetProductByNameRequest, GetProductByNameResponse>
    {
    }
}