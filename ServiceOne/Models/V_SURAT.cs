using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiceOne.Models
{
    public partial class V_SURAT
    {
        public int id { get; set; }
        public string batch_no { get; set; }
        public string no_surat { get; set; }
        public string kode_surat { get; set; }
        public string jenis_surat { get; set; }
        public string tipe_surat { get; set; }
        public string pengirim { get; set; }
        public string penerima { get; set; }
        public string mengetahui { get; set; }
        public string perihal { get; set; }
        public string subject { get; set; }
        public string status { get; set; }
        public Nullable<System.DateTime> created_at { get; set; }
        public Nullable<System.DateTime> updated_at { get; set; }
        public Nullable<System.DateTime> last_updated_at { get; set; }
        public string created_by { get; set; }
    }
}