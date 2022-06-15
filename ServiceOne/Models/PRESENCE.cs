using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiceOne.Models
{
    public partial class PRESENCE
    {
        public int id { get; set; }
        public string user_id { get; set; }
        public string device_id { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public string address { get; set; }
        public Nullable<int> presence_type { get; set; }
        public string remark { get; set; }
        public Nullable<System.DateTime> created_date { get; set; }
        public Nullable<System.DateTime> created_at { get; set; }
        public string base64_image { get; set; }
    }
}