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
    public class DepartmentsController : BaseController
    {
        string apiUrl = Properties.Settings.Default.ApiUrl;
        private ProcessResult result = new ProcessResult();
        // GET: Departments
        public async Task<ActionResult> Index()
        {
            List<V_DEPARTMENT> ObjResponse = new List<V_DEPARTMENT>();
            using (var client = new HttpClient())
            {
                string token = Session["token"].ToString();
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.GetAsync("Svc/Departments/GetDepartments");
                if (response.IsSuccessStatusCode)
                {
                    var Response = response.Content.ReadAsStringAsync().Result;
                    ObjResponse = JsonConvert.DeserializeObject<List<V_DEPARTMENT>>(Response);
                }
            }
            return View(ObjResponse);
        }

        // GET: Departments/Create
        public async Task<ActionResult> Create()
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

            List<SelectListItem> company = new List<SelectListItem>();
            foreach (var item in ObjResponse)
            {
                company.Add(new SelectListItem { Text = item.company_name, Value = item.company_id });
            }

            ViewBag.Company = company;

            DEPARTMENT obj = new DEPARTMENT();
            ViewBag.Title = "Create Departments";
            ViewBag.Header = "Create Departments";
            return PartialView("Create", obj);
        }

        // POST: Departments/Create
        [HttpPost]
        public async Task<ActionResult> Create(DEPARTMENT collection)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Message = "NOTE : Proses save gagal, silahkan lengkapi kembali data anda.";
                ViewBag.Header = collection.id == 0 ? "Create Devisions" : "Edit Devisions";
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
                        var response = await client.PostAsync("Svc/Departments/PostDepartment", httpContent);
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

        // GET: Departments/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            try
            {
                // TODO: Add update logic here
                List<COMPANY> CompResponse = new List<COMPANY>();
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
                        CompResponse = JsonConvert.DeserializeObject<List<COMPANY>>(Response);
                    }
                }

                List<SelectListItem> company = new List<SelectListItem>();
                foreach (var item in CompResponse)
                {
                    company.Add(new SelectListItem { Text = item.company_name, Value = item.company_id });
                }

                ViewBag.Company = company;


                DEPARTMENT ObjResponse = new DEPARTMENT();
                using (var client = new HttpClient())
                {
                    string token = Session["token"].ToString();
                    client.BaseAddress = new Uri(apiUrl);
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var response = await client.GetAsync("Svc/Departments/GetDepartment/" + id);
                    if (response.IsSuccessStatusCode)
                    {
                        var Response = response.Content.ReadAsStringAsync().Result;
                        ObjResponse = JsonConvert.DeserializeObject<DEPARTMENT>(Response);
                    }
                }
                ViewBag.Title = "Edit Departments";
                ViewBag.Header = "Edit Departments";
                return PartialView("Edit", ObjResponse);
            }
            catch
            {
                return View();
            }
        }

        // POST: Departments/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(int id, DEPARTMENT collection)
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
                    var response = await client.PutAsync("Svc/Departments/PutDepartment/" + id, httpContent);
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

        // GET: Departments/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            using (var client = new HttpClient())
            {
                string token = Session["token"].ToString();
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.DeleteAsync("Svc/Departments/DeleteDepartment/" + id);
                if (response.IsSuccessStatusCode)
                {
                    var Response = response.Content.ReadAsStringAsync().Result;
                }
            }

            //return RedirectToAction("Index");

            result.DeleteSucceed();
            return Json(new { result }, JsonRequestBehavior.AllowGet);
        }

        // POST: Departments/Delete/5
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