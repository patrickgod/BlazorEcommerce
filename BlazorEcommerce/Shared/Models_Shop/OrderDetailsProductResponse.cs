using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorEcommerce.Shared
{
    public class OrderDetailsProductResponse
    {
        public int ProductId { get; set; }
        public string Title { get; set; }
        public string ProductType { get; set; }
        public string ImageUrl { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
