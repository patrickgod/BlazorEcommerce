﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BlazorEcommerce.Shared.Models
{
    public partial class PersonDto
    {
        [Key]
        public Guid Personid { get; set; }

        [Required]
        public string FullName { get; set; }

        public string ClassName { get; set; }

        public Guid ClassId { get; set; }
        public string Note { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        public bool? Ismature { get; set; }
        
        
        //public bool Deleted { get; set; } = false;
        
        public string Parentname { get; set; }

    }
}