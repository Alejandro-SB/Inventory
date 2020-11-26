using Inventory.Domain.Persistence;

namespace Inventory.Infrastructure
{
    /// <summary>
    /// The user accessing the application for audit purposes
    /// </summary>
    public class AuditUser : IAuditUser
    {
        public string? Username { get; }

        public AuditUser(string? username)
        {
            Username = username;
        }
    }
}