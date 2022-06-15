using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceOne.Models;

namespace ServiceOne.ViewModels
{
    public class EmployeeViewModel
    {
        public EMPLOYEE employee { get; set; }
        public List<DEPARTMENT> departments { get; set; }
        public List<DIVISION> divisions { get; set; }
        public List<MSFIELD> positions { get; set; }
        public List<V_USER> uplines { get; set; }
    }
}