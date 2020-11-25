using System;
using System.ComponentModel.DataAnnotations;

namespace Inventory.Web.Models
{
    public class NewProductModel
    {
        [Display(Name = "Name")]
        [Required]
        public string Name { get; set; }
        [Display(Name = "Expiration date")]
        [DataType(DataType.Date)]
        public DateTime? ExpirationDate { get; set; }
        [Display(Name = "Product type")]
        public string ProductType { get; set; }
    }
}