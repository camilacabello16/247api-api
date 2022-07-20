using System;
using System.Collections.Generic;
using System.Text;

namespace API.Common.Models
{
    /// <summary>
    /// Câu thông báo 
    /// </summary>
    /// CreatedBy: BDHIEU (09/02/2021)
    public class ErrorMsg
    {
        #region PROPERTY
        public List<string> devMsg { get; set; } = new List<string>();
        public List<string> userMsg { get; set; } = new List<string>();
        public string errorCode { get; set; }
        public string moreInfo { get; set; }
        public string traceId { get; set; }
        #endregion
    }
}
