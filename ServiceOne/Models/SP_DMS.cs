using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiceOne.Models
{
    public class SP_DMS
    {
        public Nullable<long> id { get; set; }
        public string batch_no { get; set; }
        public string no_surat { get; set; }
        public Nullable<int> total_file { get; set; }
    }
}