using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiceOne.Models
{
    public partial class V_GROUP_ACCESS
    {
        public int id { get; set; }
        public string role_id { get; set; }
        public string role_name { get; set; }
        public string menu_id { get; set; }
        public string menu_name { get; set; }
        public string site_url { get; set; }
        public string icon { get; set; }
        public Nullable<bool> parent { get; set; }
        public Nullable<int> parent_id { get; set; }
        public Nullable<int> status { get; set; }
    }
}