using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiceOne.Models
{
    public partial class MSFIELD
    {
        public int id { get; set; }
        public string field_key { get; set; }
        public string field_name { get; set; }
        public string field_value { get; set; }
        public string description { get; set; }
    }
}