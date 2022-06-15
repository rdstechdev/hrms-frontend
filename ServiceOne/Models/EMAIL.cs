using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ServiceOne.Models
{
    public partial class EMAIL
    {
        public int id { get; set; }
        public string mail_from { get; set; }
        public string mail_to { get; set; }
        public string mail_cc { get; set; }
        public string mail_bcc { get; set; }
        public string mail_subject { get; set; }

        [AllowHtml]
        public string message { get; set; }
        public string mail_status { get; set; }
        public string created_by { get; set; }
        public Nullable<System.DateTime> created_at { get; set; }
    }
}