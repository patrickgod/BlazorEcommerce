using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BlazorEcommerce.Shared
{
    public class Images
    {
        public int Id { get; set; }
        public string Data { get; set; } = string.Empty;

        public int ProductId { get; set; }

        [JsonIgnore]
        [NotMapped]
        public List<ProductVariants> ProductVariants { get; set; } = new List<ProductVariants>();

        [JsonIgnore]
        [NotMapped]
        public Products Products { get; set; } = new Products();

        
    }
}
