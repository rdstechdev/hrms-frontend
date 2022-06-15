using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiceOne.Models
{
    public partial class LAMPIRAN
    {
        public int id { get; set; }
        public string batch_no { get; set; }
        public string no_surat { get; set; }
        public string nama_file { get; set; }
        public string content_type { get; set; }
        public string path_file { get; set; }
        public Nullable<double> file_size { get; set; }
        public Nullable<int> preview { get; set; }
        public Nullable<int> download { get; set; }
        public Nullable<int> status { get; set; }
        public Nullable<System.DateTime> created_at { get; set; }
        public Nullable<System.DateTime> updated_at { get; set; }
        public Nullable<System.DateTime> last_updated_at { get; set; }
        public string created_by { get; set; }
    }
}