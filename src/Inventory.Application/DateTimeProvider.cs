using Inventory.Domain.DateTimeProvider;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.Application
{
    public sealed class DateTimeProvider : IDateTimeProvider
    {
        public DateTime UtcNow => DateTime.UtcNow;
    }
}