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
    public class ZonasiController : BaseController
    {
        string apiUrl = Properties.Settings.Default.ApiUrl;
        private ProcessResult result = new ProcessResult();
        // GET: Zonasi
        public async Task<ActionResult> Index()
        {
            List<SP_TREE_EMPLOYEE> ObjResponse = new List<SP_TREE_EMPLOYEE>();
            using (var client = new HttpClient())
            {
                string token = Session["token"].ToString();
                string user_id = Session["user_id"].ToString();
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.GetAsync("Svc/Zonasi/GetEmployees?user_id=" + user_id);
                if (response.IsSuccessStatusCode)
                {
                    var Response = response.Content.ReadAsStringAsync().Result;
                    ObjResponse = JsonConvert.DeserializeObject<List<SP_TREE_EMPLOYEE>>(Response);
                }
            }
            return View(ObjResponse);
        }

        // GET: Zonasi/Details/5
        public async Task<ActionResult> Details(int id, string user_id)
        {
            try
            {
                // TODO: Add update logic here

                DetailsZonasiViewModel ObjResponse = new DetailsZonasiViewModel();
                using (var client = new HttpClient())
                {
                    string token = Session["token"].ToString();
                    client.BaseAddress = new Uri(apiUrl);
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var response = await client.GetAsync("Svc/Zonasi/GetGetZonasis?user_id=" + user_id);
                    if (response.IsSuccessStatusCode)
                    {
                        var Response = response.Content.ReadAsStringAsync().Result;
                        ObjResponse = JsonConvert.DeserializeObject<DetailsZonasiViewModel>(Response);
                    }
                }

                ViewBag.Zona = ObjResponse.list_zona;
                return View(ObjResponse);
            }
            catch
            {
                return View();
            }
        }

        // GET: Zonasi/Create
        public async Task<ActionResult> Create(int id, string user_id)
        {
            List<SP_SELECT_ZONASI> ObjResponse = new List<SP_SELECT_ZONASI>();
            using (var client = new HttpClient())
            {
                string token = Session["token"].ToString();
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.GetAsync("Svc/Zonasi/GetSelectedZona?user_id="+user_id);
                if (response.IsSuccessStatusCode)
                {
                    var Response = response.Content.ReadAsStringAsync().Result;
                    ObjResponse = JsonConvert.DeserializeObject<List<SP_SELECT_ZONASI>>(Response);
                }
            }

            ViewBag.Zona = ObjResponse;
            ViewBag.Id = id;
            ViewBag.UserId = user_id;
            ZONASI obj = new ZONASI();
            ViewBag.Title = "Create Zonasi";
            ViewBag.Header = "Create Zonasi";
            return PartialView("Create", obj);
        }

        // POST: Zonasi/Create
        [HttpPost]
        public async Task<ActionResult> Create(int id,FormCollection collection)
        {
            if (!ModelState.IsValid)
            {
                //ViewBag.Message = "NOTE : Proses save gagal, silahkan lengkapi kembali data anda.";
                //ViewBag.Header = collection.id == 0 ? "Create Company" : "Edit Company";
                //result.ProcessFailed("ValidationError");
                return Json(new { result, partialView = RenderPartialViewToString("CreateEdit", collection) }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                try
                {
                    var user_id = collection["user_id"];
                    var zonas = collection["zona_id"];
                    string[] zonaArr = zonas.Split(',');

                    string created_by = Session["user_id"].ToString();

                    List<ZONASI> list_zona = new List<ZONASI>();
                    for (int i = 0; i < zonaArr.Length; i++)
                    {
                        list_zona.Add(new ZONASI { id = 0, user_id = user_id, zona_id = Convert.ToInt32(zonaArr[i]), created_by = created_by });
                    }

                    //TODO: Add insert logic here
                    using (var client = new HttpClient())
                    {
                        string token = Session["token"].ToString();
                        client.BaseAddress = new Uri(apiUrl);
                        client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        var jsonString = JsonConvert.SerializeObject(list_zona);
                        HttpContent httpContent = new StringContent(jsonString);
                        httpContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                        var response = await client.PostAsync("Svc/Zonasi/PostListZonasi?user_id=" + user_id, httpContent);
                        if (response.IsSuccessStatusCode)
                        {
                            var Response = response.Content.ReadAsStringAsync().Result;
                        }
                    }

                    result.InsertSucceed();
                    result.id = id;
                    result.user_id = user_id;
                    return Json(new { result }, JsonRequestBehavior.AllowGet);
                }
                catch
                {
                    return View();
                }
            }
        }
    }
}