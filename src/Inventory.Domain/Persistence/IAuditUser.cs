using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.Domain.Persistence
{
    public interface IAuditUser
    {
        public string? Username { get; }
    }
}