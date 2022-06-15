﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiceOne.Models
{
    public partial class V_DIVISION
    {
        public int id { get; set; }
        public string company_id { get; set; }
        public string company_name { get; set; }
        public string department_id { get; set; }
        public string department_name { get; set; }
        public string division_id { get; set; }
        public string division_name { get; set; }
        public string description { get; set; }
        public Nullable<int> status { get; set; }
        public Nullable<System.DateTime> created_at { get; set; }
        public Nullable<System.DateTime> updated_at { get; set; }
        public Nullable<System.DateTime> last_updated_at { get; set; }
        public string created_by { get; set; }
    }
}