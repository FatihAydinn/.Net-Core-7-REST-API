using System.ComponentModel.DataAnnotations;

namespace DenemeAPI.Models
{
    public class Product
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Image { get; set; }

        [Required]
        public DateTime Insert_Date { get; set; }
    }
}
