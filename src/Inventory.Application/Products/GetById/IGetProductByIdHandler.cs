using Inventory.Domain.UseCases;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.Application.Products.GetById
{
    public interface IGetProductByIdHandler : IUseCaseHandler<GetProductByIdRequest, GetProductByIdResponse>
    {
    }
}