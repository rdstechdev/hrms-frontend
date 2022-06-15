using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiceOne.ViewModels
{
    public class JSTreeView
    {
        public string id { get; set; }
        public string parent { get; set; }
        public string text { get; set; }
        public string type { get; set; }
        public string icon { get; set; }
        public int selected { get; set; }
    }
}