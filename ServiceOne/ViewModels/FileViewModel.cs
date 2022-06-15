using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ServiceOne.ViewModels
{
    public class FileViewModel
    {
        public int id { get; set; }
        public string batch_no { get; set; }
        public string no_surat { get; set; }
        public string nama_file { get; set; }
        public string content_type { get; set; }
        public string binary_file { get; set; }
        public Nullable<double> file_size { get; set; }
        public string created_by { get; set; }
    }
}