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
using Rotativa;
using System.IO;

namespace ServiceOne.Controllers
{
    [AuthorizeSession]
    public class TemplateSuratController : BaseController
    {
        string apiUrl = Properties.Settings.Default.ApiUrl;
        private ProcessResult result = new ProcessResult();

        // GET: TemplateSurat
        public async Task<ActionResult> Index()
        {
            List<TEMPLATE_SURAT> ObjResponse = new List<TEMPLATE_SURAT>();
            using (var client = new HttpClient())
            {
                string token = Session["token"].ToString();
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.GetAsync("TemplateSurat");
                if (response.IsSuccessStatusCode)
                {
                    var Response = response.Content.ReadAsStringAsync().Result;
                    ObjResponse = JsonConvert.DeserializeObject<List<TEMPLATE_SURAT>>(Response);
                }
            }
            return View(ObjResponse);
        }

        // GET: TemplateSurat/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: TemplateSurat/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TemplateSurat/Create
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

        // GET: TemplateSurat/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            TEMPLATE_SURAT ObjResponse = new TEMPLATE_SURAT();
            using (var client = new HttpClient())
            {
                string token = Session["token"].ToString();
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.GetAsync("TemplateSurat/" + id);
                if (response.IsSuccessStatusCode)
                {
                    var Response = response.Content.ReadAsStringAsync().Result;
                    ObjResponse = JsonConvert.DeserializeObject<TEMPLATE_SURAT>(Response);
                }
            }
            List<SelectListItem> Status = new List<SelectListItem>();
            Status.Add(new SelectListItem { Text = "Inactive", Value = "0" });
            Status.Add(new SelectListItem { Text = "Active", Value = "1" });
            ViewBag.Status = Status;

            return View(ObjResponse);
        }

        // POST: TemplateSurat/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(int id, TEMPLATE_SURAT collection)
        {
            try
            {
                // TODO: Add update logic here
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
                    var response = await client.PutAsync("TemplateSurat/" + id, httpContent);
                    if (response.IsSuccessStatusCode)
                    {
                        var Response = response.Content.ReadAsStringAsync().Result;
                    }
                }
                return RedirectToAction("Edit", new { id = id });
            }
            catch
            {
                return View();
            }
        }

        // GET: TemplateSurat/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TemplateSurat/Delete/5
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
