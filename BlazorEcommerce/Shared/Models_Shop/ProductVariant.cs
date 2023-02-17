using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BlazorEcommerce.Shared
{
    public class ProductVariants
    {
        
        public int Id { get; set; }

        public int ProductId { get; set; }

        public int ProductTypeId { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal OriginalPrice { get; set; }
        public bool Visible { get; set; } = true;
        public bool Deleted { get; set; } = false;
        [NotMapped]
        public bool Editing { get; set; } = false;
        [NotMapped]
        public bool IsNew { get; set; } = false;

        //[JsonIgnore]
        //[NotMapped]
        //public Products? Products { get; set; }

        [JsonIgnore]
        [NotMapped]
        public Products? Product { get; set; }

        [JsonIgnore]
        [NotMapped]
        public ProductTypes? ProductType { get; set; }


        //[ForeignKey("ProductId")]
        //public virtual Products Products { get; set; }

        //[ForeignKey("ProductTypeId")]
        //public virtual ProductTypes ProductTypes { get; set; }


    }
}
