using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiceOne.Models
{
    public partial class V_CONTACT
    {
        public int id { get; set; }
        public string user_id { get; set; }
        public string role_id { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        public string telepon { get; set; }
    }
}