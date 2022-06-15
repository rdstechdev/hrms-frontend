using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiceOne.Models
{
    public partial class V_ORGANIZATION
    {
        public int id { get; set; }
        public string user_id { get; set; }
        public string username { get; set; }
        public string dep_id { get; set; }
        public string dep_name { get; set; }
        public string dev_id { get; set; }
        public string dev_name { get; set; }
        public string position { get; set; }
        public string upline_id { get; set; }
        public string upline_name { get; set; }
        public Nullable<int> status { get; set; }
        public Nullable<System.DateTime> created_at { get; set; }
        public Nullable<System.DateTime> updated_at { get; set; }
        public Nullable<System.DateTime> last_updated_at { get; set; }
        public string created_by { get; set; }
    }
}