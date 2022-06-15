using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiceOne.Models
{
    public partial class SP_SELECT_ZONASI
    {
        public Nullable<int> id { get; set; }
        public string area_name { get; set; }
        public string address { get; set; }
        public string description { get; set; }
        public int selected { get; set; }
    }
}