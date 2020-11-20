using Inventory.Domain.UseCases;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Inventory.Application.Products.CreateProduct
{
    public interface ICreateProductHandler : IUseCaseHandler<CreateProductRequest, CreateProductResponse>
    {
    }
}