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
    public class UsersController : BaseController
    {
        string apiUrl = Properties.Settings.Default.ApiUrl;
        private ProcessResult result = new ProcessResult();
        // GET: Users
        public async Task<ActionResult> Index()
        {
            List<V_USER> ObjResponse = new List<V_USER>();
            using (var client = new HttpClient())
            {
                string token = Session["token"].ToString();
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Add("Authorization","Bearer " + token);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.GetAsync("Svc/Users/GetUsers");
                if (response.IsSuccessStatusCode)
                {
                    var Response = response.Content.ReadAsStringAsync().Result;
                    ObjResponse = JsonConvert.DeserializeObject<List<V_USER>>(Response);
                }
            }
            return View(ObjResponse);
        }

        // GET: Users/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Users/Create
        public async Task<ActionResult> Create()
        {
            List<ROLE> ObjResponse = new List<ROLE>();
            using (var client = new HttpClient())
            {
                string token = Session["token"].ToString();
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.GetAsync("Svc/Roles");
                if (response.IsSuccessStatusCode)
                {
                    var Response = response.Content.ReadAsStringAsync().Result;
                    ObjResponse = JsonConvert.DeserializeObject<List<ROLE>>(Response);
                }
            }

            List<SelectListItem> roles = new List<SelectListItem>();
            foreach (var item in ObjResponse)
            {
                roles.Add(new SelectListItem { Text = item.role_name, Value = item.role_id.ToString() });
            }

            ViewBag.Roles = roles;

            return View();
        }

        // POST: Users/Create
        [HttpPost]
        public async Task<ActionResult> Create(USER collection)
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
                    var response = await client.PostAsync("Svc/Users/PostUser", httpContent);
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

        // GET: Users/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            UserViewModel uvm = new UserViewModel();

            USER ObjResponse = new USER();
            using (var client = new HttpClient())
            {
                string token = Session["token"].ToString();
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.GetAsync("Svc/Users/GetUser/" + id);
                if (response.IsSuccessStatusCode)
                {
                    var Response = response.Content.ReadAsStringAsync().Result;
                    ObjResponse = JsonConvert.DeserializeObject<USER>(Response);
                }
            }

            MainFields mainFields = new MainFields();
            mainFields.id = ObjResponse.id;
            mainFields.user_id = ObjResponse.user_id;
            mainFields.username = ObjResponse.username;
            mainFields.email = ObjResponse.email;
            mainFields.telepon = ObjResponse.telepon;
            mainFields.avatar = ObjResponse.avatar;
            mainFields.role_id = ObjResponse.role_id;

            CustomFields customFields = new CustomFields();
            customFields.id = ObjResponse.id;
            customFields.user_id = ObjResponse.user_id;
            customFields.join_at = ObjResponse.join_at;
            customFields.expired_at = ObjResponse.expired_at;
            customFields.invalid_login = ObjResponse.invalid_login;
            customFields.status = ObjResponse.status;

            Security security = new Security();
            security.id = ObjResponse.id;
            security.user_id = ObjResponse.user_id;
            security.new_password = "";
            security.confirm_password = "";

            ORGANIZATION organization = new ORGANIZATION();
            organization.id = ObjResponse.id;
            organization.user_id = ObjResponse.user_id;
            //organization.dep_id = ObjResponse.dep_id;
            //organization.dev_id = ObjResponse.dev_id;
            //organization.position = ObjResponse.position;
            //organization.upline_id = ObjResponse.upline_id;

            uvm.main_fields = mainFields;
            uvm.custom_fields = customFields;
            uvm.security = security;

            List<ROLE> RolesResponse = new List<ROLE>();
            using (var client = new HttpClient())
            {
                string token = Session["token"].ToString();
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.GetAsync("Svc/Roles");
                if (response.IsSuccessStatusCode)
                {
                    var Response = response.Content.ReadAsStringAsync().Result;
                    RolesResponse = JsonConvert.DeserializeObject<List<ROLE>>(Response);
                }
            }

            List<SelectListItem> roles = new List<SelectListItem>();
            foreach (var item in RolesResponse)
            {
                roles.Add(new SelectListItem { Text = item.role_name, Value = item.role_id.ToString() });
            }

            List<ZONA> ZonasResponse = new List<ZONA>();
            using (var client = new HttpClient())
            {
                string token = Session["token"].ToString();
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.GetAsync("Svc/Zonasi/GetZonasis");
                if (response.IsSuccessStatusCode)
                {
                    var Response = response.Content.ReadAsStringAsync().Result;
                    ZonasResponse = JsonConvert.DeserializeObject<List<ZONA>>(Response);
                }
            }


            ViewBag.Roles = roles;
            ViewBag.Zona = ZonasResponse;

            return View(uvm);
        }

        // POST: Users/Edit/5
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

        // POST: Users/MainFields
        [HttpPost]
        public async Task<ActionResult> MainFields(MainFields collection)
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
                    var response = await client.PostAsync("Svc/Users/MainFields", httpContent);
                    if (response.IsSuccessStatusCode)
                    {
                        var Response = response.Content.ReadAsStringAsync().Result;
                    }
                }
                return RedirectToAction("Edit", new {id = collection.id});
            }
            catch
            {
                return View();
            }
        }

        // POST: Users/CustomFields
        [HttpPost]
        public async Task<ActionResult> CustomFields(CustomFields collection)
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
                    var response = await client.PostAsync("Svc/Users/CustomFields", httpContent);
                    if (response.IsSuccessStatusCode)
                    {
                        var Response = response.Content.ReadAsStringAsync().Result;
                    }
                }
                return RedirectToAction("Edit", new { id = collection.id });
            }
            catch
            {
                return View();
            }
        }

        // POST: Users/Security
        [HttpPost]
        public async Task<ActionResult> Security(Security collection)
        {
            if (collection.new_password != collection.confirm_password)
            {
                ModelState.AddModelError("Error", "New Password & Confirmation Password not match");
                //return RedirectToAction("Edit", new { id = collection.id });
            }

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
                    var response = await client.PostAsync("Svc/Users/ChangePassword", httpContent);
                    if (response.IsSuccessStatusCode)
                    {
                        var Response = response.Content.ReadAsStringAsync().Result;
                    }
                }
                return RedirectToAction("Edit", new { id = collection.id });
            }
            catch
            {
                return View();
            }
        }

        // POST: Users/Organisation
        [HttpPost]
        public async Task<ActionResult> Organization(ORGANIZATION collection)
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
                    var response = await client.PostAsync("Svc/Users/Organization/", httpContent);
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

        // GET: Users/CreateOrganization
        public async Task<ActionResult> CreateOrganization(int id)
        {
            UserEditViewModel ObjResponse = new UserEditViewModel();
            using (var client = new HttpClient())
            {
                string token = Session["token"].ToString();
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.GetAsync("Svc/Users/UsersEdit/" + id);
                if (response.IsSuccessStatusCode)
                {
                    var Response = response.Content.ReadAsStringAsync().Result;
                    ObjResponse = JsonConvert.DeserializeObject<UserEditViewModel>(Response);
                }
            }

            List<SelectListItem> departments = new List<SelectListItem>();
            foreach (var item in ObjResponse.departments)
            {
                departments.Add(new SelectListItem { Text = item.department_name, Value = item.department_id });
            }
            List<SelectListItem> devisions = new List<SelectListItem>();
            foreach (var item in ObjResponse.divisions)
            {
                devisions.Add(new SelectListItem { Text = item.division_name, Value = item.division_id });
            }
            List<SelectListItem> positions = new List<SelectListItem>();
            foreach (var item in ObjResponse.positions)
            {
                positions.Add(new SelectListItem { Text = item.field_name, Value = item.field_value });
            }
            List<SelectListItem> uplines = new List<SelectListItem>();
            foreach (var item in ObjResponse.uplines)
            {
                uplines.Add(new SelectListItem { Text = item.username, Value = item.user_id });
            }

            ViewBag.UserIndex = ObjResponse.user.id;
            ViewBag.Departments = departments;
            ViewBag.Devisions = devisions;
            ViewBag.Positions = positions;
            ViewBag.Uplines = uplines;

            ORGANIZATION obj = new ORGANIZATION();
            obj.id = ObjResponse.user.id;
            obj.user_id = ObjResponse.user.user_id;
            ViewBag.Title = "Add Organization";
            ViewBag.Header = "Add Organization";
            return PartialView("_Organization", obj);
        }

        // GET: Users/EditOrganization
        public async Task<ActionResult> EditOrganization(int id)
        {
            OrganizationViewModel ObjResponse = new OrganizationViewModel();
            using (var client = new HttpClient())
            {
                string token = Session["token"].ToString();
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.GetAsync("Svc/Users/EditOrganization/" + id);
                if (response.IsSuccessStatusCode)
                {
                    var Response = response.Content.ReadAsStringAsync().Result;
                    ObjResponse = JsonConvert.DeserializeObject<OrganizationViewModel>(Response);
                }
            }

            List<SelectListItem> departments = new List<SelectListItem>();
            foreach (var item in ObjResponse.departments)
            {
                departments.Add(new SelectListItem { Text = item.department_name, Value = item.department_id });
            }
            List<SelectListItem> devisions = new List<SelectListItem>();
            foreach (var item in ObjResponse.divisions)
            {
                devisions.Add(new SelectListItem { Text = item.division_name, Value = item.division_id });
            }
            List<SelectListItem> positions = new List<SelectListItem>();
            foreach (var item in ObjResponse.positions)
            {
                positions.Add(new SelectListItem { Text = item.field_name, Value = item.field_value });
            }
            List<SelectListItem> uplines = new List<SelectListItem>();
            foreach (var item in ObjResponse.uplines)
            {
                uplines.Add(new SelectListItem { Text = item.username, Value = item.user_id });
            }

            ViewBag.UserIndex = ObjResponse.user.id;

            ViewBag.Departments = departments;
            ViewBag.Devisions = devisions;
            ViewBag.Positions = positions;
            ViewBag.Uplines = uplines;

            ORGANIZATION obj = new ORGANIZATION();
            obj = ObjResponse.organization;
            ViewBag.Title = "Edit Organization";
            ViewBag.Header = "Edit Organization";
            return PartialView("_Organization", obj);
        }

        // GET: Users/DeleteOrganization/5
        public async Task<ActionResult> DeleteOrganization(int id)
        {
            using (var client = new HttpClient())
            {
                string token = Session["token"].ToString();
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.DeleteAsync("Svc/Users/DeleteOrganization/" + id);
                if (response.IsSuccessStatusCode)
                {
                    var Response = response.Content.ReadAsStringAsync().Result;
                }
            }

            result.DeleteSucceed();
            return Json(new { result }, JsonRequestBehavior.AllowGet);
        }
        // GET: Users/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Users/Delete/5
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

