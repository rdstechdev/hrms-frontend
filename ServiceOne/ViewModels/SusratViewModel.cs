using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceOne.Models;

namespace ServiceOne.ViewModels
{
    public class SusratViewModel
    {
        public SURAT surat { get; set; }
        public List<LAMPIRAN> lampiran { get; set; }
        public List<V_CONTACT_ORG> pengirim { get; set; }
        public List<V_CONTACT_ORG> penerima { get; set; }
        public List<TEMPLATE_SURAT> templates { get; set; }
        public List<MSFIELD> tipe { get; set; }
        public List<V_CHAT> chats { get; set; }
        public List<V_FLOW_SURAT> flow { get; set; }
    }
}