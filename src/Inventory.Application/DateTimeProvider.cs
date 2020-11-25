using Inventory.Domain.DateTimeProvider;
using System;

namespace Inventory.Application
{
    public sealed class DateTimeProvider : IDateTimeProvider
    {
        public DateTime UtcNow => DateTime.UtcNow;
    }
}