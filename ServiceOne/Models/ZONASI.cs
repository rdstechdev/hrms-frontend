using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiceOne.Models
{
    public partial class ZONASI
    {
        public int id { get; set; }
        public string user_id { get; set; }
        public Nullable<int> zona_id { get; set; }
        public Nullable<int> status { get; set; }
        public Nullable<System.DateTime> created_at { get; set; }
        public Nullable<System.DateTime> updated_at { get; set; }
        public Nullable<System.DateTime> last_updated_at { get; set; }
        public string created_by { get; set; }
    }
}