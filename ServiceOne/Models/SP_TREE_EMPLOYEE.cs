using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiceOne.Models
{
    public partial class SP_TREE_EMPLOYEE
    {
        public Nullable<int> id { get; set; }
        public Nullable<long> seq { get; set; }
        public string employee_no { get; set; }
        public string full_name { get; set; }
        public string position_code { get; set; }
        public string grade_code { get; set; }
        public Nullable<int> level { get; set; }
        public string hierachy { get; set; }
        public string tree_sequance { get; set; }
    }
}