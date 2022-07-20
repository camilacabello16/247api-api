using System;
using System.Collections.Generic;
using System.Text;

namespace API.Common.Models
{
    public class response
    {
        public Guid ResponseID { get; set; }
        public Guid ArticleID { get; set; }
        public string ResponseContent { get; set; }
        public string ResponseDescription { get; set; }
        /// <summary>
        /// 1: 200
        /// 2: 404
        /// 3: 500
        /// </summary>
        public int ResponseCode { get; set; }
    }
}
