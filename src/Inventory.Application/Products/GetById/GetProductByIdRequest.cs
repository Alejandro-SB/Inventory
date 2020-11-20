using Inventory.Domain.UseCases;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.Application.Products.GetById
{
    public class GetProductByIdRequest : IRequest<GetProductByIdResponse>
    {
        public int Id { get; set; }

        public GetProductByIdRequest(int id)
        {
            Id = id;
        }
    }
}