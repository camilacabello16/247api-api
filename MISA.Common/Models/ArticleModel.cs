using System;
using System.Collections.Generic;
using System.Text;

namespace API.Common.Models
{
    public class ArticleModel
    {
        public article articleApi { get; set; }
        public List<request> requestApi { get; set; }
        public List<response> responseApi { get; set; }
    }
}
