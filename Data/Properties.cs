﻿using System.ComponentModel.DataAnnotations;

namespace uowpublic.Data
{
    public class Properties
    {
        [Key]
        public int PropertyID { get; set; }

        [Required]
        public int OwnerID { get; set; } 

        [Required]
        [MaxLength(255)]
        public string Address { get; set; }

        [Required]
        [MaxLength(255)]
        public string Type { get; set; }

        [Required]
        public decimal Rent { get; set; }

        public string Photos { get; set; } 

        public string Description { get; set; }

    }
}