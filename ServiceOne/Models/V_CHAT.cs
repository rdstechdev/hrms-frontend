using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiceOne.Models
{
    public partial class V_CHAT
    {
        public int id { get; set; }
        public string batch_no { get; set; }
        public string message { get; set; }
        public Nullable<System.DateTime> created_at { get; set; }
        public string created_by { get; set; }
        public string username { get; set; }
    }
}