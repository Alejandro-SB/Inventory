using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inventory.Web.Dto
{
    public class ProductDto
    {
        public string Name { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public string ProductType { get; set; }
    }
}