using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiceOne.Models
{
    public partial class HOME_SLIDERS
    {
        public int id { get; set; }
        public string file_name { get; set; }
        public string file_ext { get; set; }
        public string content_type { get; set; }
        public string file_path { get; set; }
        public byte[] file_binary { get; set; }
        public Nullable<int> status { get; set; }
        public Nullable<System.DateTime> created_at { get; set; }
        public Nullable<System.DateTime> updated_at { get; set; }
        public Nullable<System.DateTime> last_updated_at { get; set; }
        public string created_by { get; set; }
    }
}