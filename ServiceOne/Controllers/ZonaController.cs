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
    public class ZonaController : BaseController
    {
        string apiUrl = Properties.Settings.Default.ApiUrl;
        private ProcessResult result = new ProcessResult();

        // GET: Zona/
        public async Task<ActionResult> Index()
        {
            List<ZONA> ObjResponse = new List<ZONA>();
            using (var client = new HttpClient())
            {
                string token = Session["token"].ToString();
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.GetAsync("Svc/Zona/GetZonas");
                if (response.IsSuccessStatusCode)
                {
                    var Response = response.Content.ReadAsStringAsync().Result;
                    ObjResponse = JsonConvert.DeserializeObject<List<ZONA>>(Response);
                }
            }
            return View(ObjResponse);
        }

        // GET: Zonasi/Create
        public ActionResult Create()
        {
            ZONA obj = new ZONA();
            ViewBag.Title = "Set Zona";
            ViewBag.Header = "Set Zona";
            return PartialView("Create", obj);
        }

        // POST: Zonasi/Create
        [HttpPost]
        public async Task<ActionResult> Create(ZONA collection)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Message = "NOTE : Proses save gagal, silahkan lengkapi kembali data anda.";
                ViewBag.Header = collection.id == 0 ? "Create Zona" : "Edit Zona";
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
                        var response = await client.PostAsync("Svc/Zona/PostZona", httpContent);
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

        // GET: Zonasi/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            try
            {
                // TODO: Add update logic here

                ZONA ObjResponse = new ZONA();
                using (var client = new HttpClient())
                {
                    string token = Session["token"].ToString();
                    client.BaseAddress = new Uri(apiUrl);
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var response = await client.GetAsync("Svc/Zona/GetZona/" + id);
                    if (response.IsSuccessStatusCode)
                    {
                        var Response = response.Content.ReadAsStringAsync().Result;
                        ObjResponse = JsonConvert.DeserializeObject<ZONA>(Response);
                    }
                }
                ViewBag.Title = "Edit Zona";
                ViewBag.Header = "Edit Zona";
                return View("Edit", ObjResponse);
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        // POST: Zonasi/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(int id, ZONA collection)
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
                    var response = await client.PutAsync("Svc/Zona/PutZona/" + id, httpContent);
                    if (response.IsSuccessStatusCode)
                    {
                        var Response = response.Content.ReadAsStringAsync().Result;
                    }
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Zonasi/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            using (var client = new HttpClient())
            {
                string token = Session["token"].ToString();
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.DeleteAsync("Svc/Zona/DeleteZona/" + id);
                if (response.IsSuccessStatusCode)
                {
                    var Response = response.Content.ReadAsStringAsync().Result;
                }
            }

            //return RedirectToAction("Index");

            result.DeleteSucceed();
            return Json(new { result }, JsonRequestBehavior.AllowGet);
        }

        // POST: Zonasi/Delete/5
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