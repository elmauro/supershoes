using System.ComponentModel.DataAnnotations;

namespace SuperShoes.Models
{
    public class Article
    {
        public int id { get; set; }
        [Required]
        public string name { get; set; }
        public string description { get; set; }
        public decimal price { get; set; }
        public int total_in_shelf { get; set; }
        public int total_in_vault { get; set; }

        // Foreign Key
        public int StoreId { get; set; }
        // Navigation property
        public Store Store { get; set; }
    }
}