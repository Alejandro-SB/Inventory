using System;

namespace Inventory.Domain.DateTimeProvider
{
    public interface IDateTimeProvider
    {
        DateTime UtcNow { get; }
    }
}