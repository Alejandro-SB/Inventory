using System.Diagnostics.CodeAnalysis;

namespace Inventory.Domain.Extensions
{
    /// <summary>
    /// Helpers for string management
    /// </summary>
    public static class StringHelpers
    {
        /// <summary>
        /// Checks if a string is null or empty
        /// </summary>
        /// <param name="s">The string to check</param>
        /// <returns>True if string is null or empty, false otherwise</returns>
        public static bool IsNullOrEmpty([NotNullWhen(false)] this string? s)
        {
            return string.IsNullOrEmpty(s);
        }
    }
}