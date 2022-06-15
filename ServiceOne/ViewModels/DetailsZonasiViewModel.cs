using ServiceOne.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiceOne.ViewModels
{
    public class DetailsZonasiViewModel
    {
        public V_EMPLOYEE employee { get; set; }
        public List<V_ZONASI> list_zonasi { get; set; }
        public List<ZONA> list_zona { get; set; }
    }
}