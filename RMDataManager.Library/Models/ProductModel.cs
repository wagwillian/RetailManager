using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMDataManager.Library.Models
{
    public class ProductModel
    {
        /// <summary>
        /// The unique indentifier for a given product.
        /// </summary>
        public int Id { get; set; }        
        public string ProductName { get; set; }        
        public string Description { get; set; }        
        public decimal RetailPrice { get; set; }
        /// <summary>
        /// Current quantity in stock
        /// </summary>
        public int QuantityInStock { get; set; }
        public bool IsTaxable { get; set; }
        public string ProductImage { get; set; }

    }
}
