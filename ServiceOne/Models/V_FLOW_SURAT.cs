using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiceOne.Models
{
    public partial class V_FLOW_SURAT
    {
        public int id { get; set; }
        public string batch_no { get; set; }
        public string no_surat { get; set; }
        public string user_id { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        public string user_status { get; set; }
        public Nullable<int> read_status { get; set; }
        public Nullable<System.DateTime> read_at { get; set; }
        public string approved_status { get; set; }
        public Nullable<System.DateTime> approved_at { get; set; }
        public Nullable<int> day { get; set; }
        public string day_name { get; set; }
        public string month { get; set; }
        public Nullable<int> year { get; set; }
        public string time { get; set; }
    }
}