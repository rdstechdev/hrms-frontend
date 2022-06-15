using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiceOne.ViewModels
{
    public class GlobalSetting
    {
        public Global global { get; set; }
        public Localisation localisation { get; set; }

    }

    public class Global
    {
        public string app_name { get; set; }
        public string app_short_description { get; set; }
        public string app_logo { get; set; }
        public string app_version { get; set; }
        public string enable_version { get; set; }
        public string theme_contrast { get; set; }
        public string theme_color { get; set; }
        public string navbar_color { get; set; }
        public string fixed_header { get; set; }
        public string fixed_footer { get; set; }

    }

    public class Localisation
    {
        public string date_format { get; set; }
        public string is_human_date_format { get; set; }
        public string language { get; set; }
        public string timezone { get; set; }
    }

    public class Mail
    {
        public string mail_driver { get; set; }
        public string mail_host { get; set; }
        public string mail_port { get; set; }
        public string mail_username { get; set; }
        public string mail_password { get; set; }
        public string mail_encryption { get; set; }
        public string mail_from_address { get; set; }
        public string mail_from_name { get; set; }
    }

    public class Notifications
    {
        public string enable_notifications { get; set; }
        public string fcm_key { get; set; }
        public string firebase_api_key { get; set; }
        public string firebase_auth_domain { get; set; }
        public string firebase_database_url { get; set; }
        public string firebase_project_id { get; set; }
        public string firebase_storage_bucket { get; set; }
        public string firebase_messaging_sender_id { get; set; }
        public string firebase_app_id { get; set; }
        public string firebase_measurement_id { get; set; }
    }
}