using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorEcommerce.Shared
{
    public class OrderItems
    {
        public Orders Order { get; set; }
        public int OrderId { get; set; }
        public Products Product { get; set; }
        public int ProductId { get; set; }
        public ProductTypes ProductType { get; set; }
        public int ProductTypeId { get; set; }
        public int Quantity { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalPrice { get; set; }
    }
}
