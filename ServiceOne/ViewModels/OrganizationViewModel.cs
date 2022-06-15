using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceOne.Models;

namespace ServiceOne.ViewModels
{
    public class OrganizationViewModel
    {
        public USER user { get; set; }
        public ORGANIZATION organization { get; set; }
        public List<DEPARTMENT> departments { get; set; }
        public List<DIVISION> divisions { get; set; }
        public List<MSFIELD> positions { get; set; }
        public List<V_USER> uplines { get; set; }
    }
}