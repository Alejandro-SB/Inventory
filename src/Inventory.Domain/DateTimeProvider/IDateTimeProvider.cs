using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.Domain.DateTimeProvider
{
    public interface IDateTimeProvider
    {
        DateTime UtcNow { get; }
    }
}