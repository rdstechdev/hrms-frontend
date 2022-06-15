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
    public class CompanyController : BaseController
    {
        string apiUrl = Properties.Settings.Default.ApiUrl;
        private ProcessResult result = new ProcessResult();
        // GET: Company
        public async Task<ActionResult> Index()
        {
            List<COMPANY> ObjResponse = new List<COMPANY>();
            using (var client = new HttpClient())
            {
                string token = Session["token"].ToString();
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.GetAsync("Svc/Companies/GetCompanies");
                if (response.IsSuccessStatusCode)
                {
                    var Response = response.Content.ReadAsStringAsync().Result;
                    ObjResponse = JsonConvert.DeserializeObject<List<COMPANY>>(Response);
                }
            }
            return View(ObjResponse);
        }

        // GET: Company/Create
        public ActionResult Create()
        {
            COMPANY obj = new COMPANY();
            ViewBag.Title = "Create Company";
            ViewBag.Header = "Create Company";
            return PartialView("Create", obj);
        }

        // POST: Company/Create
        [HttpPost]
        public async Task<ActionResult> Create(COMPANY collection)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Message = "NOTE : Proses save gagal, silahkan lengkapi kembali data anda.";
                ViewBag.Header = collection.id == 0 ? "Create Company" : "Edit Company";
                result.ProcessFailed("ValidationError");
                return Json(new { result, partialView = RenderPartialViewToString("CreateEdit", collection) }, JsonRequestBehavior.AllowGet);
            }
            else
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
                        var response = await client.PostAsync("Svc/Companies/PostCompany", httpContent);
                        if (response.IsSuccessStatusCode)
                        {
                            var Response = response.Content.ReadAsStringAsync().Result;
                        }
                    }

                    result.InsertSucceed();
                    return Json(new { result }, JsonRequestBehavior.AllowGet);
                }
                catch
                {
                    return View();
                }
            }
        }

        // GET: Company/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            try
            {
                // TODO: Add update logic here

                COMPANY ObjResponse = new COMPANY();
                using (var client = new HttpClient())
                {
                    string token = Session["token"].ToString();
                    client.BaseAddress = new Uri(apiUrl);
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var response = await client.GetAsync("Svc/Companies/GetCompany/" + id);
                    if (response.IsSuccessStatusCode)
                    {
                        var Response = response.Content.ReadAsStringAsync().Result;
                        ObjResponse = JsonConvert.DeserializeObject<COMPANY>(Response);
                    }
                }
                ViewBag.Title = "Edit Company";
                ViewBag.Header = "Edit Company";
                return PartialView("Edit", ObjResponse);
            }
            catch
            {
                return View();
            }
        }

        // POST: Company/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(int id, COMPANY collection)
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
                    var response = await client.PutAsync("Svc/Companies/PutCompany/" + id, httpContent);
                    if (response.IsSuccessStatusCode)
                    {
                        var Response = response.Content.ReadAsStringAsync().Result;
                    }
                }

                //return RedirectToAction("Index");

                result.UpdateSucceed();
                return Json(new { result }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return View();
            }
        }

        // GET: Company/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            using (var client = new HttpClient())
            {
                string token = Session["token"].ToString();
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.DeleteAsync("Svc/Companies/DeleteCompany/" + id);
                if (response.IsSuccessStatusCode)
                {
                    var Response = response.Content.ReadAsStringAsync().Result;
                }
            }

            //return RedirectToAction("Index");

            result.DeleteSucceed();
            return Json(new { result }, JsonRequestBehavior.AllowGet);
        }

        // POST: Company/Delete/5
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