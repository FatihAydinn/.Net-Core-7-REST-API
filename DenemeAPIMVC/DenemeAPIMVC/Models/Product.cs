using System.ComponentModel.DataAnnotations;

namespace DenemeAPIMVC.Models
{
    public class Product
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string Image { get; set; }

        public DateTime Insert_Date { get; set; }
    }
}
