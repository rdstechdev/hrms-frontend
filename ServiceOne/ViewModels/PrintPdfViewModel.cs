using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceOne.Models;

namespace ServiceOne.ViewModels
{
    public class PrintPdfViewModel
    {
        public SURAT surat { get; set; }
        public TEMPLATE_SURAT template { get; set; }
        public V_CONTACT_ORG pengirim { get; set; }
        public Nullable<int> lampiran { get; set; }
    }
}