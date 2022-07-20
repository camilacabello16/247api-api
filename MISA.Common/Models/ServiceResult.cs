using System;
using System.Collections.Generic;
using System.Text;

namespace API.Common.Models
{
    /// <summary>
    /// Kết quả service trả về
    /// </summary>
    /// CreatedBy: BDHIEU (09/02/2021)
    public class ServiceResult
    {
        #region PROPERTY
        /// <summary>
        /// Dữ liệu của service 
        /// </summary>
        /// CreatedBy: BDHIEU (09/02/2021)
        public object Data { get; set; }
        /// <summary>
        /// Trang thái của service
        /// </summary>
        public bool Success { get; set; } = true;
        /// <summary>
        /// Mã code tự định nghĩa
        /// </summary>
        public string MISACode { get; set; }
        #endregion
    }
}
