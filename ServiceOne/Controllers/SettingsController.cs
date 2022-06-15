using Newtonsoft.Json;
using ServiceOne.Models;
using ServiceOne.Services;
using ServiceOne.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ServiceOne.Controllers
{
    [AuthorizeSession]
    public class SettingsController : Controller
    {
        string apiUrl = Properties.Settings.Default.ApiUrl;
        // GET: Global
        public async Task<ActionResult> Global()
        {
            List<MSFIELD> ObjResponse = new List<MSFIELD>();
            using (var client = new HttpClient())
            {
                string token = Session["token"].ToString();
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.GetAsync("Settings/global_settings");
                if (response.IsSuccessStatusCode)
                {
                    var Response = response.Content.ReadAsStringAsync().Result;
                    ObjResponse = JsonConvert.DeserializeObject<List<MSFIELD>>(Response);
                }
            }

            Global globalSetting = new Global();
            foreach (var item in ObjResponse)
            {
                if (item.field_name == "app_name") { globalSetting.app_name = item.field_value; }
                if (item.field_name == "app_short_description") { globalSetting.app_short_description = item.field_value; }
                if (item.field_name == "app_logo") { globalSetting.app_logo = item.field_value; }
                if (item.field_name == "app_version") { globalSetting.app_version = item.field_value; }
                if (item.field_name == "enable_version") { globalSetting.enable_version = item.field_value; }
                if (item.field_name == "theme_contrast") { globalSetting.theme_contrast = item.field_value; }
                if (item.field_name == "theme_color") { globalSetting.theme_color = item.field_value; }
                if (item.field_name == "navbar_color") { globalSetting.navbar_color = item.field_value; }
                if (item.field_name == "fixed_header") { globalSetting.fixed_header = item.field_value; }
                if (item.field_name == "fixed_footer") { globalSetting.fixed_footer = item.field_value; }
                
            }

            return View(globalSetting);
        }

        // GET: Localisation
        public async Task<ActionResult> Localisation()
        {
            List<MSFIELD> ObjResponse = new List<MSFIELD>();
            using (var client = new HttpClient())
            {
                string token = Session["token"].ToString();
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.GetAsync("Settings/localisation");
                if (response.IsSuccessStatusCode)
                {
                    var Response = response.Content.ReadAsStringAsync().Result;
                    ObjResponse = JsonConvert.DeserializeObject<List<MSFIELD>>(Response);
                }
            }

            Localisation localisationSetting = new Localisation();
            foreach (var item in ObjResponse)
            {
                if (item.field_name == "date_format") { localisationSetting.date_format = item.field_value; }
                if (item.field_name == "is_human_date_format") { localisationSetting.is_human_date_format = item.field_value; }
                if (item.field_name == "language") { localisationSetting.language = item.field_value; }
                if (item.field_name == "timezone") { localisationSetting.timezone = item.field_value; }
                
            }
            
            return View(localisationSetting);
        }

        // GET: Mail
        public async Task<ActionResult> Mail()
        {
            List<MSFIELD> ObjResponse = new List<MSFIELD>();
            using (var client = new HttpClient())
            {
                string token = Session["token"].ToString();
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.GetAsync("Settings/mail");
                if (response.IsSuccessStatusCode)
                {
                    var Response = response.Content.ReadAsStringAsync().Result;
                    ObjResponse = JsonConvert.DeserializeObject<List<MSFIELD>>(Response);
                }
            }

            Mail mail = new Mail();
            foreach (var item in ObjResponse)
            {
                if (item.field_name == "mail_driver") { mail.mail_driver = item.field_value; }
                if (item.field_name == "mail_host") { mail.mail_host = item.field_value; }
                if (item.field_name == "mail_port") { mail.mail_port = item.field_value; }
                if (item.field_name == "mail_username") { mail.mail_username = item.field_value; }
                if (item.field_name == "mail_password") { mail.mail_password = item.field_value; }
                if (item.field_name == "mail_encryption") { mail.mail_encryption = item.field_value; }
                if (item.field_name == "mail_from_address") { mail.mail_from_address = item.field_value; }
                if (item.field_name == "mail_from_name") { mail.mail_from_name = item.field_value; }

            }

            return View(mail);
        }

        // GET: Mail
        public async Task<ActionResult> Notifications()
        {
            List<MSFIELD> ObjResponse = new List<MSFIELD>();
            using (var client = new HttpClient())
            {
                string token = Session["token"].ToString();
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.GetAsync("Settings/notifications");
                if (response.IsSuccessStatusCode)
                {
                    var Response = response.Content.ReadAsStringAsync().Result;
                    ObjResponse = JsonConvert.DeserializeObject<List<MSFIELD>>(Response);
                }
            }

            Notifications notifications = new Notifications();
            foreach (var item in ObjResponse)
            {
                if (item.field_name == "enable_notifications") { notifications.enable_notifications = item.field_value; }
                if (item.field_name == "fcm_key") { notifications.fcm_key = item.field_value; }
                if (item.field_name == "firebase_api_key") { notifications.firebase_api_key = item.field_value; }
                if (item.field_name == "firebase_auth_domain") { notifications.firebase_auth_domain = item.field_value; }
                if (item.field_name == "firebase_database_url") { notifications.firebase_database_url = item.field_value; }
                if (item.field_name == "firebase_project_id") { notifications.firebase_project_id = item.field_value; }
                if (item.field_name == "firebase_storage_bucket") { notifications.firebase_storage_bucket = item.field_value; }
                if (item.field_name == "firebase_messaging_sender_id") { notifications.firebase_messaging_sender_id = item.field_value; }
                if (item.field_name == "firebase_app_id") { notifications.firebase_app_id = item.field_value; }
                if (item.field_name == "firebase_measurement_id") { notifications.firebase_measurement_id = item.field_value; }


            }

            return View(notifications);
        }


        // POST: Settings/UpdateGlobal
        [HttpPost]
        public async Task<ActionResult> UpdateGlobal(Global collection)
        {
            try
            {
                List<MSFIELD> msfield = new List<MSFIELD>();
                msfield.Add(new MSFIELD { field_key = "global_settings", field_name = "app_name", field_value = collection.app_name });
                msfield.Add(new MSFIELD { field_key = "global_settings", field_name = "app_short_description", field_value = collection.app_short_description });
                msfield.Add(new MSFIELD { field_key = "global_settings", field_name = "app_logo", field_value = collection.app_logo });
                msfield.Add(new MSFIELD { field_key = "global_settings", field_name = "app_version", field_value = collection.app_version });
                msfield.Add(new MSFIELD { field_key = "global_settings", field_name = "enable_version", field_value = collection.enable_version });
                msfield.Add(new MSFIELD { field_key = "global_settings", field_name = "theme_contrast", field_value = collection.theme_contrast });
                msfield.Add(new MSFIELD { field_key = "global_settings", field_name = "theme_color", field_value = collection.theme_color });
                msfield.Add(new MSFIELD { field_key = "global_settings", field_name = "navbar_color", field_value = collection.navbar_color });
                msfield.Add(new MSFIELD { field_key = "global_settings", field_name = "fixed_header", field_value = collection.fixed_header });
                msfield.Add(new MSFIELD { field_key = "global_settings", field_name = "fixed_footer", field_value = collection.fixed_footer });

                // TODO: Add insert logic here
                using (var client = new HttpClient())
                {
                    string token = Session["token"].ToString();
                    client.BaseAddress = new Uri(apiUrl);
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var jsonString = JsonConvert.SerializeObject(msfield);
                    HttpContent httpContent = new StringContent(jsonString);
                    httpContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                    var response = await client.PostAsync("Settings/UpdateList", httpContent);
                    if (response.IsSuccessStatusCode)
                    {
                        var Response = response.Content.ReadAsStringAsync().Result;
                    }
                }
                return RedirectToAction("Global");
            }
            catch
            {
                return View();
            }
        }

        // POST: Settings/Updatelocalisation
        [HttpPost]
        public async Task<ActionResult> Updatelocalisation(Localisation collection)
        {
            try
            {
                List<MSFIELD> msfield = new List<MSFIELD>();
                msfield.Add(new MSFIELD { field_key = "localisation", field_name = "date_format", field_value = collection.date_format });
                msfield.Add(new MSFIELD { field_key = "localisation", field_name = "is_human_date_format", field_value = collection.is_human_date_format });
                msfield.Add(new MSFIELD { field_key = "localisation", field_name = "language", field_value = collection.language });
                msfield.Add(new MSFIELD { field_key = "localisation", field_name = "timezone", field_value = collection.timezone });
               
                // TODO: Add insert logic here
                using (var client = new HttpClient())
                {
                    string token = Session["token"].ToString();
                    client.BaseAddress = new Uri(apiUrl);
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var jsonString = JsonConvert.SerializeObject(msfield);
                    HttpContent httpContent = new StringContent(jsonString);
                    httpContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                    var response = await client.PostAsync("Settings/UpdateList", httpContent);
                    if (response.IsSuccessStatusCode)
                    {
                        var Response = response.Content.ReadAsStringAsync().Result;
                    }
                }
                return RedirectToAction("Localisation");
            }
            catch
            {
                return View();
            }
        }

        // POST: Settings/UpdateMail
        [HttpPost]
        public async Task<ActionResult> UpdateMail(Mail collection)
        {
            try
            {
                List<MSFIELD> msfield = new List<MSFIELD>();
                msfield.Add(new MSFIELD { field_key = "mail", field_name = "mail_driver", field_value = collection.mail_driver });
                msfield.Add(new MSFIELD { field_key = "mail", field_name = "mail_host", field_value = collection.mail_host });
                msfield.Add(new MSFIELD { field_key = "mail", field_name = "mail_port", field_value = collection.mail_port });
                msfield.Add(new MSFIELD { field_key = "mail", field_name = "mail_username", field_value = collection.mail_username });
                msfield.Add(new MSFIELD { field_key = "mail", field_name = "mail_password", field_value = collection.mail_password });
                msfield.Add(new MSFIELD { field_key = "mail", field_name = "mail_encryption", field_value = collection.mail_encryption });
                msfield.Add(new MSFIELD { field_key = "mail", field_name = "mail_from_address", field_value = collection.mail_from_address });
                msfield.Add(new MSFIELD { field_key = "mail", field_name = "mail_from_name", field_value = collection.mail_from_name });

                // TODO: Add insert logic here
                using (var client = new HttpClient())
                {
                    string token = Session["token"].ToString();
                    client.BaseAddress = new Uri(apiUrl);
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var jsonString = JsonConvert.SerializeObject(msfield);
                    HttpContent httpContent = new StringContent(jsonString);
                    httpContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                    var response = await client.PostAsync("Settings/UpdateList", httpContent);
                    if (response.IsSuccessStatusCode)
                    {
                        var Response = response.Content.ReadAsStringAsync().Result;
                    }
                }
                return RedirectToAction("Mail");
            }
            catch
            {
                return View();
            }
        }

        // POST: Settings/UpdateNotifications
        [HttpPost]
        public async Task<ActionResult> UpdateNotifications(Notifications collection)
        {
            try
            {
                List<MSFIELD> msfield = new List<MSFIELD>();
                msfield.Add(new MSFIELD { field_key = "notifications", field_name = "enable_notifications", field_value = collection.enable_notifications });
                msfield.Add(new MSFIELD { field_key = "notifications", field_name = "fcm_key", field_value = collection.fcm_key });
                msfield.Add(new MSFIELD { field_key = "notifications", field_name = "firebase_api_key", field_value = collection.firebase_api_key });
                msfield.Add(new MSFIELD { field_key = "notifications", field_name = "firebase_auth_domain", field_value = collection.firebase_auth_domain });
                msfield.Add(new MSFIELD { field_key = "notifications", field_name = "firebase_database_url", field_value = collection.firebase_database_url });
                msfield.Add(new MSFIELD { field_key = "notifications", field_name = "firebase_project_id", field_value = collection.firebase_project_id });
                msfield.Add(new MSFIELD { field_key = "notifications", field_name = "firebase_storage_bucket", field_value = collection.firebase_storage_bucket });
                msfield.Add(new MSFIELD { field_key = "notifications", field_name = "firebase_messaging_sender_id", field_value = collection.firebase_messaging_sender_id });
                msfield.Add(new MSFIELD { field_key = "notifications", field_name = "firebase_app_id", field_value = collection.firebase_app_id });
                msfield.Add(new MSFIELD { field_key = "notifications", field_name = "firebase_measurement_id", field_value = collection.firebase_measurement_id });
                // TODO: Add insert logic here
                using (var client = new HttpClient())
                {
                    string token = Session["token"].ToString();
                    client.BaseAddress = new Uri(apiUrl);
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var jsonString = JsonConvert.SerializeObject(msfield);
                    HttpContent httpContent = new StringContent(jsonString);
                    httpContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                    var response = await client.PostAsync("Settings/UpdateList", httpContent);
                    if (response.IsSuccessStatusCode)
                    {
                        var Response = response.Content.ReadAsStringAsync().Result;
                    }
                }
                return RedirectToAction("Notifications");
            }
            catch
            {
                return View();
            }
        }

    }
}