
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BlazorEcommerce.Shared.Models
{
    public partial class ClassDto
    {
        [Key]
        public Guid Classid { get; set; }

        [Required]
        public string Classname { get; set; }
        public string Note { get; set; }

       
    }
}