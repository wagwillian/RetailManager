using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMDesktopUI.Library.Models
{
    public class CreateProductModel
    {
        
        [Required]
        [DisplayName("Product Name")]
        public string ProductName { get; set; }
        [Required]
        [DisplayName("Description")]
        public string Description { get; set; }
        [Required]
        [DisplayName("Retail Price")]
        public double RetailPrice { get; set; }
        [Required]
        public int QuantityInStock { get; set; }
        [Required]
        public bool IsTaxable { get; set; }

        public string ProductImage { get; set; }
    }
}
