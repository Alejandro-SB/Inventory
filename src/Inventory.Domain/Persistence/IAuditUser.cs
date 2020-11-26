namespace Inventory.Domain.Persistence
{
    /// <summary>
    /// Represents an abstract user (for audit purposes)
    /// </summary>
    public interface IAuditUser
    {
        public string? Username { get; }
    }
}