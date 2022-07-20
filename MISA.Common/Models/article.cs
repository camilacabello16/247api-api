using System;
using System.Collections.Generic;
using System.Text;

namespace API.Common.Models
{
    public class article
    {
        public Guid ArticleID { get; set; }
        public Guid CategoryID { get; set; }
        public string ArticleName { get; set; }
        public string BriefDescription { get; set; }
        public string ApiName { get; set; }
        public int ApiType { get; set; }
        public string ApiLink { get; set; }
        public string ApiExample { get; set; }
    }
}
