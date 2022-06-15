using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiceOne.Models
{
    public partial class V_USER
    {
        public int id { get; set; }
        public string user_id { get; set; }
        public string role_id { get; set; }
        public string role { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        public string telepon { get; set; }
        public string device_id { get; set; }
        public string refresh_token { get; set; }
        public Nullable<int> invalid_login { get; set; }
        public string avatar { get; set; }
        public Nullable<int> status { get; set; }
        public Nullable<System.DateTime> join_at { get; set; }
        public Nullable<System.DateTime> expired_at { get; set; }
        public Nullable<System.DateTime> created_at { get; set; }
        public Nullable<System.DateTime> updated_at { get; set; }
        public Nullable<System.DateTime> last_updated_at { get; set; }
        public string created_by { get; set; }
    }
}