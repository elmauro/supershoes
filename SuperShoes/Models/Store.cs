using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SuperShoes.Models
{
    public class Store
    {
        public int id { get; set; }
        [Required]
        public string name { get; set; }
        public string address { get; set; }
    }
}