using Newtonsoft.Json;
using ServiceOne.Models;
using ServiceOne.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ServiceOne.ViewModels;

namespace ServiceOne.Controllers
{
    [AuthorizeSession]
    public class EmailsController : Controller
    {
        string apiUrl = Properties.Settings.Default.ApiUrl;
        // GET: Emails
        public async Task<ActionResult> Index()
        {
            List<EMAIL> ObjResponse = new List<EMAIL>();
            using (var client = new HttpClient())
            {
                string token = Session["token"].ToString();
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.GetAsync("Emails");
                if (response.IsSuccessStatusCode)
                {
                    var Response = response.Content.ReadAsStringAsync().Result;
                    ObjResponse = JsonConvert.DeserializeObject<List<EMAIL>>(Response);
                }
            }
            return View(ObjResponse);
        }

        // GET: Emails/Sent
        public async Task<ActionResult> Sent()
        {
            List<EMAIL> ObjResponse = new List<EMAIL>();
            using (var client = new HttpClient())
            {
                string token = Session["token"].ToString();
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.GetAsync("Emails");
                if (response.IsSuccessStatusCode)
                {
                    var Response = response.Content.ReadAsStringAsync().Result;
                    ObjResponse = JsonConvert.DeserializeObject<List<EMAIL>>(Response);
                }
            }
            return View(ObjResponse);
        }

        // GET: Emails/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Emails/Compose
        public async Task<ActionResult> Compose()
        {
            List<V_CONTACT> ObjResponse = new List<V_CONTACT>();
            using (var client = new HttpClient())
            {
                string token = Session["token"].ToString();
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.GetAsync("Users/contact");
                if (response.IsSuccessStatusCode)
                {
                    var Response = response.Content.ReadAsStringAsync().Result;
                    ObjResponse = JsonConvert.DeserializeObject<List<V_CONTACT>>(Response);
                }
            }

            List<SelectListItem> contact = new List<SelectListItem>();
            foreach (var item in ObjResponse)
            {
                contact.Add(new SelectListItem { Text = item.username, Value = item.email.ToString() });
            }

            ViewBag.Contact = contact;

            return View();
        }

        // POST: Settings/UpdateMail
        [HttpPost]
        public async Task<ActionResult> Compose(EMAIL collection)
        {
            try
            {
                // TODO: Add insert logic here
                using (var client = new HttpClient())
                {
                    string token = Session["token"].ToString();
                    string user_id = Session["user_id"].ToString();
                    collection.created_by = user_id;
                    client.BaseAddress = new Uri(apiUrl);
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var jsonString = JsonConvert.SerializeObject(collection);
                    HttpContent httpContent = new StringContent(jsonString);
                    httpContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                    var response = await client.PostAsync("Emails/Compose", httpContent);
                    if (response.IsSuccessStatusCode)
                    {
                        var Response = response.Content.ReadAsStringAsync().Result;
                    }
                }
                return RedirectToAction("Email");
            }
            catch
            {
                return View();
            }
        }

        // GET: Emails/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Emails/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Emails/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Emails/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Emails/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Emails/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
