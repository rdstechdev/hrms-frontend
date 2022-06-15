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
    public class RolesController : BaseController
    {
        string apiUrl = Properties.Settings.Default.ApiUrl;
        private ProcessResult result = new ProcessResult();
        // GET: Roles
        public async Task<ActionResult> Index()
        {
            List<ROLE> ObjResponse = new List<ROLE>();
            using (var client = new HttpClient())
            {
                string token = Session["token"].ToString();
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.GetAsync("Svc/Roles/GetRoles");
                if (response.IsSuccessStatusCode)
                {
                    var Response = response.Content.ReadAsStringAsync().Result;
                    ObjResponse = JsonConvert.DeserializeObject<List<ROLE>>(Response);
                }
            }
            return View(ObjResponse);
        }

        // GET: Roles/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Roles/Group/5
        public ActionResult Group(string role_id, string role_name)
        {
            List<SP_TREE_MENU> ObjResponse = new List<SP_TREE_MENU>();
            ViewBag.Title = "Group Access";
            ViewBag.Header = "Group Access";
            ViewBag.RoleId = role_id;
            ViewBag.RoleName= role_name;
            return PartialView("Group", ObjResponse);
        }

        public async Task<ActionResult> Menu(string role_id)
        {
            List<SP_GROUP_ACCES> ObjResponse = new List<SP_GROUP_ACCES>();
            using (var client = new HttpClient())
            {
                string token = Session["token"].ToString();
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.GetAsync("Svc/GroupAccess?role_id=" + role_id);
                if (response.IsSuccessStatusCode)
                {
                    var Response = response.Content.ReadAsStringAsync().Result;
                    ObjResponse = JsonConvert.DeserializeObject<List<SP_GROUP_ACCES>>(Response);
                }
            }

            List<JSTreeView> jstree = new List<JSTreeView>();
            foreach (var item in ObjResponse)
            {
                if (item.parent_id == 0)
                {
                    jstree.Add(new JSTreeView
                    {
                        id = item.menu_id.ToString(),
                        parent = "#",
                        text = item.menu_name,
                        type = "folder",
                        icon = "",
                        selected = item.selected
                    });
                }
                else
                {
                    jstree.Add(new JSTreeView
                    {
                        id = item.menu_id.ToString(),
                        parent = item.parent_id.ToString(),
                        text = item.menu_name,
                        type = "file",
                        icon = "",
                        selected = item.selected
                    });
                }
            }
            return Json(new { jstree }, JsonRequestBehavior.AllowGet);
        }

        // GET: Roles/Create
        public ActionResult Create()
        {
            ROLE obj = new ROLE();
            ViewBag.Title = "Create Role";
            ViewBag.Header = "Create Role";
            return PartialView("Create", obj);
        }

        // POST: Roles/Create
        [HttpPost]
        public async Task<ActionResult> Create(ROLE collection)
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
                        string user_id = Session["user_id"].ToString();
                        collection.created_by = user_id;

                        client.BaseAddress = new Uri(apiUrl);
                        client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        var jsonString = JsonConvert.SerializeObject(collection);
                        HttpContent httpContent = new StringContent(jsonString);
                        httpContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                        var response = await client.PostAsync("Svc/Roles/PostRole", httpContent);
                        if (response.IsSuccessStatusCode)
                        {
                            var Response = response.Content.ReadAsStringAsync().Result;
                        }
                    }
                    //return RedirectToAction("Index");
                    
                    result.InsertSucceed();
                    return Json(new { result }, JsonRequestBehavior.AllowGet);
                }
                catch
                {
                    return View();
                }
            }
        }


        // POST: Roles/Create
        [HttpPost]
        public async Task<ActionResult> SetGroupUser(string role_id, List<GROUP_ACCESS> collection)
        {
            try
            {
                // TODO: Add insert logic here
                using (var client = new HttpClient())
                {
                    string token = Session["token"].ToString();
                    string user_id = Session["user_id"].ToString();
                    //collection.created_by = user_id;

                    client.BaseAddress = new Uri(apiUrl);
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var jsonString = JsonConvert.SerializeObject(collection);
                    HttpContent httpContent = new StringContent(jsonString);
                    httpContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                    var response = await client.PostAsync("Svc/GroupAccess?role_id=" + role_id, httpContent);
                    if (response.IsSuccessStatusCode)
                    {
                        var Response = response.Content.ReadAsStringAsync().Result;
                    }
                }
                //return RedirectToAction("Index");

                result.InsertSucceed();
                return Json(new { result }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return View();
            }
        }

        // GET: Roles/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            try
            {
                // TODO: Add update logic here

                ROLE ObjResponse = new ROLE();
                using (var client = new HttpClient())
                {
                    string token = Session["token"].ToString();
                    client.BaseAddress = new Uri(apiUrl);
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var response = await client.GetAsync("Svc/Roles/GetRole/" + id);
                    if (response.IsSuccessStatusCode)
                    {
                        var Response = response.Content.ReadAsStringAsync().Result;
                        ObjResponse = JsonConvert.DeserializeObject<ROLE>(Response);
                    }
                }
                ViewBag.Title = "Edit Role";
                ViewBag.Header = "Edit Role";
                return PartialView("Edit", ObjResponse);
            }
            catch
            {
                return View();
            }
        }

        // POST: Roles/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(int id, ROLE collection)
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
                    var response = await client.PutAsync("Svc/Roles/PutRole/" + id, httpContent);
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

        // GET: Roles/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            using (var client = new HttpClient())
            {
                string token = Session["token"].ToString();
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.DeleteAsync("Svc/Roles/DeleteRole/" + id);
                if (response.IsSuccessStatusCode)
                {
                    var Response = response.Content.ReadAsStringAsync().Result;
                }
            }

            //return RedirectToAction("Index");
            
            result.DeleteSucceed();
            return Json(new { result }, JsonRequestBehavior.AllowGet);
        }

        // POST: Roles/Delete/5
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
