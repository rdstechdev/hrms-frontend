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
    public class MenusController : BaseController
    {
        string apiUrl = Properties.Settings.Default.ApiUrl;
        private ProcessResult result = new ProcessResult();
        // GET: Menus
        public async Task<ActionResult> Index()
        {
            List<SP_TREE_MENU> ObjResponse = new List<SP_TREE_MENU>();
            using (var client = new HttpClient())
            {
                string token = Session["token"].ToString();
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.GetAsync("Svc/Menus/");
                if (response.IsSuccessStatusCode)
                {
                    var Response = response.Content.ReadAsStringAsync().Result;
                    ObjResponse = JsonConvert.DeserializeObject<List<SP_TREE_MENU>>(Response);
                }
            }
            return View(ObjResponse);
        }

        // GET: Menus/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Menus/Create
        public async Task<ActionResult> Create()
        {
            MENU obj = new MENU();
            List<SP_TREE_MENU> ObjResponse = new List<SP_TREE_MENU>();
            using (var client = new HttpClient())
            {
                string token = Session["token"].ToString();
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.GetAsync("Svc/Menus");
                if (response.IsSuccessStatusCode)
                {
                    var Response = response.Content.ReadAsStringAsync().Result;
                    ObjResponse = JsonConvert.DeserializeObject<List<SP_TREE_MENU>>(Response);
                }
            }

            List<SelectListItem> parents = new List<SelectListItem>();
            parents.Add(new SelectListItem { Text = "- Select -", Value = "0" });
            var parentList = ObjResponse.Where(m=> m.parent == true).ToList();
            foreach (var item in parentList)
            {
                parents.Add(new SelectListItem { Text = item.menu_name, Value = item.menu_id.ToString() });
            }

            ViewBag.ParentList = parents;
            ViewBag.Title = "Create Menu";
            ViewBag.Header = "Create Menu";
            return PartialView("Create", obj);
        }

        // POST: Menus/Create
        [HttpPost]
        public async Task<ActionResult> Create(MENU collection)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Message = "NOTE : Proses save gagal, silahkan lengkapi kembali data anda.";
                ViewBag.Header = collection.id == 0 ? "Create User" : "Edit User";
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
                        client.BaseAddress = new Uri(apiUrl);
                        client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        var jsonString = JsonConvert.SerializeObject(collection);
                        HttpContent httpContent = new StringContent(jsonString);
                        httpContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                        var response = await client.PostAsync("Svc/Menus/", httpContent);
                        if (response.IsSuccessStatusCode)
                        {
                            var Response = response.Content.ReadAsStringAsync().Result;
                        }
                    }
                    //return RedirectToAction("Index");
                    result.IsSucceed = true;
                    result.Message = "Data has been saved";
                    result.InsertSucceed();
                    return Json(new { result }, JsonRequestBehavior.AllowGet);
                }
                catch
                {
                    return View();
                }
            }
            
        }

        // GET: Menus/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            MENU ObjResponse = new MENU();
            using (var client = new HttpClient())
            {
                string token = Session["token"].ToString();
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.GetAsync("Svc/Menus/GetMenu/" + id);
                if (response.IsSuccessStatusCode)
                {
                    var Response = response.Content.ReadAsStringAsync().Result;
                    ObjResponse = JsonConvert.DeserializeObject<MENU>(Response);
                }
            }

            List<SP_TREE_MENU> ObjMenu = new List<SP_TREE_MENU>();
            using (var client = new HttpClient())
            {
                string token = Session["token"].ToString();
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.GetAsync("Svc/Menus/GetMenus");
                if (response.IsSuccessStatusCode)
                {
                    var Response = response.Content.ReadAsStringAsync().Result;
                    ObjMenu = JsonConvert.DeserializeObject<List<SP_TREE_MENU>>(Response);
                }
            }

            List<SelectListItem> parents = new List<SelectListItem>();
            var parentList = ObjMenu.Where(m => m.parent == true).ToList();
            parents.Add(new SelectListItem { Text = "- Select -", Value = "0" });
            foreach (var item in parentList)
            {
                parents.Add(new SelectListItem { Text = item.menu_name, Value = item.menu_id.ToString() });
            }

            ViewBag.ParentList = parents;

            ViewBag.Title = "Edit Menus";
            ViewBag.Header = "Edit Menus";
            return PartialView("Edit", ObjResponse);
        }

        // POST: Menus/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(int id, MENU collection)
        {
            try
            {
                // TODO: Add update logic here

                using (var client = new HttpClient())
                {
                    string token = Session["token"].ToString();
                    client.BaseAddress = new Uri(apiUrl);
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var jsonString = JsonConvert.SerializeObject(collection);
                    HttpContent httpContent = new StringContent(jsonString);
                    httpContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                    var response = await client.PutAsync("Svc/Menus/PutMenu/" + id, httpContent);
                    if (response.IsSuccessStatusCode)
                    {
                        var Response = response.Content.ReadAsStringAsync().Result;
                    }
                }

                //return RedirectToAction("Index");
                result.IsSucceed = true;
                result.Message = "Data has been update";
                result.UpdateSucceed();
                return Json(new { result }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return View();
            }
        }

        // GET: Menus/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            using (var client = new HttpClient())
            {
                string token = Session["token"].ToString();
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                
                var response = await client.DeleteAsync("Svc/Menus/" + id);
                if (response.IsSuccessStatusCode)
                {
                    var Response = response.Content.ReadAsStringAsync().Result;
                }
            }

            //return RedirectToAction("Index");
            result.IsSucceed = true;
            result.Message = "Data has been remove";
            result.DeleteSucceed();
            return Json(new { result }, JsonRequestBehavior.AllowGet);
        }

        // POST: Menus/Delete/5
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
