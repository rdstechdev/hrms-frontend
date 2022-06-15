using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiceOne.ViewModels
{
    public class ResponseLoginViewModel
    {
        public int code { get; set; }
        public bool status { get; set; }
        public string message { get; set; }
        public string user_id { get; set; }
        public string username { get; set; }
        public string role_id { get; set; }
        public string role { get; set; }
        public string token { get; set; }
        public string refreshToken { get; set; }
        public DateTime? tokenExpiration { get; set; }
    }
}