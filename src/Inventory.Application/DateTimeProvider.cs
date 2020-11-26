using Inventory.Domain.DateTimeProvider;
using System;

namespace Inventory.Application
{
    /// <summary>
    /// The default datetime provider
    /// </summary>
    public sealed class DateTimeProvider : IDateTimeProvider
    {
        public DateTime UtcNow => DateTime.UtcNow;
    }
}