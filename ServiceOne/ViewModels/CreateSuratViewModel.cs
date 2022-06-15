using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceOne.Models;

namespace ServiceOne.ViewModels
{
    public class CreateSuratViewModel
    {
        public SURAT surat { get; set; }
        public List<FileViewModel> files { get; set;}
    }
}