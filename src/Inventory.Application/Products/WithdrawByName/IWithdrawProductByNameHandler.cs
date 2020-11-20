using Inventory.Domain.UseCases;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.Application.Products.WithdrawByName
{
    public interface IWithdrawProductByNameHandler : IUseCaseHandler<WithdrawProductByNameRequest, WithdrawProductByNameResponse>
    {
    }
}