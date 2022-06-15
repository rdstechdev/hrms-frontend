using Newtonsoft.Json;
using ServiceOne.Models;
using ServiceOne.Services;
using ServiceOne.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ServiceOne.Controllers
{
    [AuthorizeSession]
    public class HomeSlidersController : BaseController
    {
        string apiUrl = Properties.Settings.Default.ApiUrl;
        private ProcessResult result = new ProcessResult();
        // GET: HomeSliders
        public async Task<ActionResult> Index()
        {
            List<HOME_SLIDERS> ObjResponse = new List<HOME_SLIDERS>();
            using (var client = new HttpClient())
            {
                string token = Session["token"].ToString();
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.GetAsync("svc/HomeSliders/GetHomeSliders");
                if (response.IsSuccessStatusCode)
                {
                    var Response = response.Content.ReadAsStringAsync().Result;
                    ObjResponse = JsonConvert.DeserializeObject<List<HOME_SLIDERS>>(Response);
                }
            }
            return View(ObjResponse);
        }

        // GET: HomeSliders/Create
        public ActionResult Create()
        {
            HOME_SLIDERS obj = new HOME_SLIDERS();
            ViewBag.Title = "Create Sliders";
            ViewBag.Header = "Create Sliders";
            return PartialView("Create", obj);
        }

        // POST: HomeSliders/Create
        [HttpPost]
        public async Task<ActionResult> Create(HttpPostedFileBase file)
        {
            try
            {
                HOME_SLIDERS homeSliders = new HOME_SLIDERS();
                if (file != null)
                {
                    var InputFileName = Path.GetFileName(file.FileName);
                    //var ServerSavePath = Path.Combine(Server.MapPath("~/UploadedFiles/") + InputFileName);
                    string theFileName = Path.GetFileName(file.FileName);
                    byte[] thePictureAsBytes = new byte[file.ContentLength];
                    using (BinaryReader theReader = new BinaryReader(file.InputStream))
                    {
                        thePictureAsBytes = theReader.ReadBytes(file.ContentLength);
                    }
                    //string thePictureDataAsString = Convert.ToBase64String(thePictureAsBytes);
                    homeSliders.file_name = Path.GetFileName(file.FileName);
                    homeSliders.content_type = file.ContentType;
                    homeSliders.file_ext = Path.GetExtension(file.FileName);
                    homeSliders.file_binary = thePictureAsBytes;
                }

                // TODO: Add insert logic here
                using (var client = new HttpClient())
                {
                    string token = Session["token"].ToString();
                    string user_id = Session["user_id"].ToString();
                    homeSliders.created_by = user_id;
                    client.BaseAddress = new Uri(apiUrl);
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var jsonString = JsonConvert.SerializeObject(homeSliders);
                    HttpContent httpContent = new StringContent(jsonString);
                    httpContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                    var response = await client.PostAsync("svc/HomeSliders/PostHomeSlider", httpContent);
                    if (response.IsSuccessStatusCode)
                    {
                        var Response = response.Content.ReadAsStringAsync().Result;
                    }
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return View();
            }
        }

        // GET: HomeSliders/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            try
            {
                // TODO: Add update logic here

                HOME_SLIDERS ObjResponse = new HOME_SLIDERS();
                using (var client = new HttpClient())
                {
                    string token = Session["token"].ToString();
                    client.BaseAddress = new Uri(apiUrl);
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var response = await client.GetAsync("svc/HomeSliders/GetHomeSlider/" + id);
                    if (response.IsSuccessStatusCode)
                    {
                        var Response = response.Content.ReadAsStringAsync().Result;
                        ObjResponse = JsonConvert.DeserializeObject<HOME_SLIDERS>(Response);
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

        // POST: HomeSliders/Edit/5
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
                    var response = await client.PutAsync("svc/HomeSliders/PutHomeSlider/" + id, httpContent);
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

        // GET: HomeSliders/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            using (var client = new HttpClient())
            {
                string token = Session["token"].ToString();
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.DeleteAsync("svc/HomeSliders/DeleteHomeSlider/" + id);
                if (response.IsSuccessStatusCode)
                {
                    var Response = response.Content.ReadAsStringAsync().Result;
                }
            }

            //return RedirectToAction("Index");

            result.DeleteSucceed();
            return Json(new { result }, JsonRequestBehavior.AllowGet);
        }

        // POST: HomeSliders/Delete/5
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