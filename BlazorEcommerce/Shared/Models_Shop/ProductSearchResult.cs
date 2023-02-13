using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorEcommerce.Shared
{
    public class ProductSearchResult
    {
        public List<Products> Products { get; set; } = new List<Products>();
        public int Pages { get; set; }
        public int CurrentPage { get; set; }
    }
}
