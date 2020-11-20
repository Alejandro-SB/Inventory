using Inventory.Domain.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.Domain.Entities
{
    /// <summary>
    /// Base entity with audit data
    /// </summary>
    public abstract class BaseEntity
    {
        public DateTime? CreationDate { get; set; }
        public string? CreationBy { get; set; }
        public DateTime? ModificationDate { get; set; }
        public string? ModificationBy { get; set; }
    }
}