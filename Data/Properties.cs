using System.ComponentModel.DataAnnotations;

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




    }
}
