using Inventory.Domain.Persistence;

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