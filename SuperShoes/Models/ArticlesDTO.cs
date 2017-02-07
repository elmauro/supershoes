using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SuperShoes.Models
{
    public class ArticlesDTO
    {
        public IQueryable<ArticleDTO> articles { get; set; }
        public string success { get; set; }
        public int total_elements { get; set; }
    }
}