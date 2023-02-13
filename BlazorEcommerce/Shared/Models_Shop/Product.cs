using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BlazorEcommerce.Shared
{
    public class Products
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public List<Images> Images { get; set; } = new List<Images>();
        public Categories? Category { get; set; }
        public int CategoryId { get; set; }
        public bool Featured { get; set; } = false;
        public List<ProductVariants> Variants { get; set; } = new List<ProductVariants>();
        public bool Visible { get; set; } = true;
        public bool Deleted { get; set; } = false;
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
