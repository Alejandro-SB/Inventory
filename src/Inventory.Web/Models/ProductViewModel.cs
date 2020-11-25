using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Inventory.Web.Models
{
    public class ProductViewModel
    {
        public string Name { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public string ProductType { get; set; }
    }
}