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
    public class Addresses
    {
        [Key]
        public int Id { get; set; }

//        [ForeignKey("UserId")]
        public int UserId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Street { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string Zip { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;

        //[ForeignKey("UserId")]
        //[JsonIgnore]
        //public virtual Users Users { get; set; }

        [JsonIgnore]
        [ForeignKey("UserId")]
        public virtual Users User { get; set; } = new Users();
    }
}
