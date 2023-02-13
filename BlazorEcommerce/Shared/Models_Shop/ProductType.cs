using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BlazorEcommerce.Shared
{
    public class ProductTypes
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        [NotMapped]
        public bool Editing { get; set; } = false;
        [NotMapped]
        public bool IsNew { get; set; } = false;

        [JsonIgnore]
        [NotMapped]
        public List<ProductVariants> ProductVariants { get; set; } = new List<ProductVariants>();

        [JsonIgnore]
        [NotMapped]
        public List<OrderItems> OrderItems { get; set; } = new List<OrderItems>();
    }
}
