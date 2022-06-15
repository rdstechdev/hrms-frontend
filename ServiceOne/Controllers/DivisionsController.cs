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
    public class DivisionsController : BaseController
    {
        string apiUrl = Properties.Settings.Default.ApiUrl;
        private ProcessResult result = new ProcessResult();
        // GET: Divisions
        public async Task<ActionResult> Index()
        {
            List<V_DIVISION> ObjResponse = new List<V_DIVISION>();
            using (var client = new HttpClient())
            {
                string token = Session["token"].ToString();
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.GetAsync("Svc/Divisions/GetDivisions");
                if (response.IsSuccessStatusCode)
                {
                    var Response = response.Content.ReadAsStringAsync().Result;
                    ObjResponse = JsonConvert.DeserializeObject<List<V_DIVISION>>(Response);
                }
            }
            return View(ObjResponse);
        }

        // GET: Divisions/Create
        public async Task<ActionResult> Create()
        {
            List<COMPANY> COMResponse = new List<COMPANY>();
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
                    COMResponse = JsonConvert.DeserializeObject<List<COMPANY>>(Response);
                }
            }

            List<SelectListItem> company = new List<SelectListItem>();
            foreach (var item in COMResponse)
            {
                company.Add(new SelectListItem { Text = item.company_name, Value = item.company_id });
            }

            

            List<DEPARTMENT> DEPResponse = new List<DEPARTMENT>();
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
                    DEPResponse = JsonConvert.DeserializeObject<List<DEPARTMENT>>(Response);
                }
            }

            List<SelectListItem> departements = new List<SelectListItem>();
            foreach (var item in DEPResponse)
            {
                departements.Add(new SelectListItem { Text = item.department_name, Value = item.department_id });
            }

            ViewBag.Company = company;
            ViewBag.Departements = departements;

            DIVISION obj = new DIVISION();
            ViewBag.Title = "Create Divisions";
            ViewBag.Header = "Create Divisions";
            return PartialView("Create", obj);
        }

        // POST: Divisions/Create
        [HttpPost]
        public async Task<ActionResult> Create(DIVISION collection)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Message = "NOTE : Proses save gagal, silahkan lengkapi kembali data anda.";
                ViewBag.Header = collection.id == 0 ? "Create Divisions" : "Edit Divisions";
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
                        var response = await client.PostAsync("Svc/Divisions/PostDivision", httpContent);
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

        // GET: Divisions/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            try
            {
                // TODO: Add update logic here

                DIVISION ObjResponse = new DIVISION();
                using (var client = new HttpClient())
                {
                    string token = Session["token"].ToString();
                    client.BaseAddress = new Uri(apiUrl);
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var response = await client.GetAsync("Svc/Divisions/GetDivision/" + id);
                    if (response.IsSuccessStatusCode)
                    {
                        var Response = response.Content.ReadAsStringAsync().Result;
                        ObjResponse = JsonConvert.DeserializeObject<DIVISION>(Response);
                    }
                }

                List<COMPANY> COMResponse = new List<COMPANY>();
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
                        COMResponse = JsonConvert.DeserializeObject<List<COMPANY>>(Response);
                    }
                }

                List<SelectListItem> company = new List<SelectListItem>();
                foreach (var item in COMResponse)
                {
                    company.Add(new SelectListItem { Text = item.company_name, Value = item.company_id });
                }

                List<DEPARTMENT> ObjDept = new List<DEPARTMENT>();
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
                        ObjDept = JsonConvert.DeserializeObject<List<DEPARTMENT>>(Response);
                    }
                }

                List<SelectListItem> departements = new List<SelectListItem>();
                foreach (var item in ObjDept)
                {
                    departements.Add(new SelectListItem { Text = item.department_name, Value = item.department_id });
                }

                ViewBag.Company = company;
                ViewBag.Departements = departements;

                ViewBag.Title = "Edit Divisions";
                ViewBag.Header = "Edit Divisions";
                return PartialView("Edit", ObjResponse);
            }
            catch
            {
                return View();
            }
        }

        // POST: Divisions/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(int id, DIVISION collection)
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
                    var response = await client.PutAsync("Svc/Divisions/PutDivision/" + id, httpContent);
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

        // GET: Divisions/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            using (var client = new HttpClient())
            {
                string token = Session["token"].ToString();
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.DeleteAsync("Svc/Divisions/GetDivision/" + id);
                if (response.IsSuccessStatusCode)
                {
                    var Response = response.Content.ReadAsStringAsync().Result;
                }
            }

            //return RedirectToAction("Index");

            result.DeleteSucceed();
            return Json(new { result }, JsonRequestBehavior.AllowGet);
        }

        // POST: Divisions/Delete/5
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

        // GET: Divisions/GetCompany
        public async Task<ActionResult> GetDepartments(string company_id)
        {
            List<DEPARTMENT> ObjResponse = new List<DEPARTMENT>();
            using (var client = new HttpClient())
            {
                string token = Session["token"].ToString();
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.GetAsync("Divisions/Departments/" + company_id);
                if (response.IsSuccessStatusCode)
                {
                    var Response = response.Content.ReadAsStringAsync().Result;
                    ObjResponse = JsonConvert.DeserializeObject<List<DEPARTMENT>>(Response);
                }
            }

            List<SelectListItem> department = new List<SelectListItem>();
            foreach (var item in ObjResponse)
            {
                department.Add(new SelectListItem { Text = item.department_name, Value = item.department_id });
            }

            ViewBag.Department = department;

            return Json(new { department }, JsonRequestBehavior.AllowGet);
        }
    }
}