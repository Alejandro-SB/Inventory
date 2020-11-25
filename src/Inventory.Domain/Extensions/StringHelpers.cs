using System.Diagnostics.CodeAnalysis;

namespace Inventory.Domain.Extensions
{
    public static class StringHelpers
    {
        public static bool IsNullOrEmpty([NotNullWhen(false)] this string? s)
        {
            return string.IsNullOrEmpty(s);
        }
    }
}
