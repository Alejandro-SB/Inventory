using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

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
