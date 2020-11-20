using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.Domain.Exceptions
{
    public abstract class NotFoundException : ApplicationException
    {
        protected NotFoundException(string? message) : base(message)
        {
        }
    }
}