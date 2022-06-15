using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ServiceOne.Models
{
    public partial class TEMPLATE_SURAT
    {
        public int id { get; set; }
        public string kode_surat { get; set; }
        public string jenis_surat { get; set; }
        public string path_file { get; set; }

        [AllowHtml]
        public string header { get; set; }

        [AllowHtml]
        public string footer { get; set; }
        public Nullable<int> status { get; set; }
        public Nullable<System.DateTime> created_at { get; set; }
        public Nullable<System.DateTime> updated_at { get; set; }
        public Nullable<System.DateTime> last_updated_at { get; set; }
        public string created_by { get; set; }
    }
}