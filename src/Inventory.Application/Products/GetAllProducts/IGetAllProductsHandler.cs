﻿using Inventory.Domain.UseCases;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.Application.Products.GetAllProducts
{
    public interface IGetAllProductsHandler : IUseCaseHandler<GetAllProductsRequest, GetAllProductsResponse>
    {
    }
}