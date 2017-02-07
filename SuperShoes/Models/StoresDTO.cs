using SuperShoes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SuperShoes.Models
{
    public class StoresDTO
    {
        public IQueryable<StoreDTO> stores { get; set; }
        public string success { get; set; }
        public int total_elements { get; set; }
    }
}