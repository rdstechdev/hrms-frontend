using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiceOne.Models
{
    public partial class V_EMPLOYEE
    {
        public int id { get; set; }
        public string employee_no { get; set; }
        public string full_name { get; set; }
        public string gender { get; set; }
        public string personal_mobile_phone { get; set; }
        public string personal_email_address { get; set; }
        public string company_email_address { get; set; }
        public string work_group_code { get; set; }
        public string level3_code { get; set; }
        public string position_code { get; set; }
    }
}