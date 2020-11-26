using System;

namespace Inventory.Domain.DateTimeProvider
{
    /// <summary>
    /// Represents a datetime provider
    /// </summary>
    public interface IDateTimeProvider
    {
        DateTime UtcNow { get; }
    }
}