using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ServiceOne.Models;

namespace ServiceOne.ViewModels
{
    public class UserViewModel
    {
        public MainFields main_fields { get; set; }
        public CustomFields custom_fields { get; set; }
        public Security security { get; set; }
        public Zonasi zonasi { get; set; }
    }

    public class MainFields
    {
        public int id { get; set; }
        public string user_id { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        public string telepon { get; set; }
        public string avatar { get; set; }
        public string role_id { get; set; }
        public string created_by { get; set; }
    }

    public class CustomFields
    {
        public int id { get; set; }
        public string user_id { get; set; }
        public Nullable<System.DateTime> join_at { get; set; }
        public Nullable<System.DateTime> expired_at { get; set; }
        public Nullable<int> invalid_login { get; set; }
        public Nullable<int> status { get; set; }
        public string created_by { get; set; }
    }
    public class Security
    {
        public int id { get; set; }
        public string user_id { get; set; }
        [Required]
        public string new_password { get; set; }
        [Required]
        public string confirm_password { get; set; }
        public string created_by { get; set; }
    }

    public class Zonasi
    {
        public int id { get; set; }
        public string user_id { get; set; }
        public string zonasi_id { get; set; }
        public string area_name { get; set; }
        public string description { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public Nullable<int> radius { get; set; }
        public string created_by { get; set; }
    }
}