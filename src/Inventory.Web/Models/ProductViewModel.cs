using System;

namespace Inventory.Web.Models
{
    public class ProductViewModel
    {
        public string Name { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public string ProductType { get; set; }
    }
}