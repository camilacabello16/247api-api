using System;
using System.Collections.Generic;
using System.Text;

namespace API.Common.Models
{
    public class user
    {
        public Guid UserID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Fullname { get; set; }
    }
}
