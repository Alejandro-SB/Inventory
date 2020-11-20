using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.Domain.UseCases
{
    public interface IRequest<out TUseCaseResponse> { }
}