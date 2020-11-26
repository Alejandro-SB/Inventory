using Inventory.Domain.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.Infrastructure
{
    public class AuditUser : IAuditUser
    {
        public string? Username { get; }

        public AuditUser(string? username)
        {
            Username = username;
        }
    }
}