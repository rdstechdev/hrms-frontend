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
    public class SuratMasukController : BaseController
    {
        string apiUrl = Properties.Settings.Default.ApiUrl;
        private ProcessResult result = new ProcessResult();
        // GET: SuratMasuk
        public async Task<ActionResult> Index()
        {
            List<V_SURAT> ObjResponse = new List<V_SURAT>();
            using (var client = new HttpClient())
            {
                string token = Session["token"].ToString();
                string user_id = Session["user_id"].ToString();
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.GetAsync("Surat/Masuk/" + user_id);
                if (response.IsSuccessStatusCode)
                {
                    var Response = response.Content.ReadAsStringAsync().Result;
                    ObjResponse = JsonConvert.DeserializeObject<List<V_SURAT>>(Response);
                }
            }
            return View(ObjResponse);
        }

        // GET: SuratMasuk/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: SuratMasuk/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SuratMasuk/Create
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

        // GET: SuratMasuk/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            SusratViewModel ObjResponse = new SusratViewModel();
            using (var client = new HttpClient())
            {
                string token = Session["token"].ToString();
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.GetAsync("Surat/" + id);
                if (response.IsSuccessStatusCode)
                {
                    var Response = response.Content.ReadAsStringAsync().Result;
                    ObjResponse = JsonConvert.DeserializeObject<SusratViewModel>(Response);
                }
            }

            List<SelectListItem> Templates = new List<SelectListItem>();
            foreach (var item in ObjResponse.templates)
            {
                Templates.Add(new SelectListItem { Text = item.jenis_surat, Value = item.kode_surat });
            }

            List<SelectListItem> Tipe = new List<SelectListItem>();
            foreach (var item in ObjResponse.tipe)
            {
                Tipe.Add(new SelectListItem { Text = item.field_name, Value = item.field_value });
            }

            var template = ObjResponse.templates.Where(m => m.kode_surat == ObjResponse.surat.kode_surat).FirstOrDefault();

            List<SelectListItem> list_pengirim = new List<SelectListItem>();
            if (ObjResponse.surat.pengirim != null)
            {
                List<string> listPengirim = ObjResponse.surat.pengirim.Split(',').ToList();
                //for (int i = 0; i < listMengetahui.Count - 1; i++)
                //{
                //    var ct = ObjResponse.contacts.Where(c => c.email == listMengetahui[i]).FirstOrDefault(); ;
                //    list_mengetahui.Add(new SelectListItem { Text = ct.username, Value = ct.email });
                //}
                foreach (var item in ObjResponse.pengirim)
                {
                    bool selected = false;
                    for (int i = 0; i < listPengirim.Count - 1; i++)
                    {
                        if (item.upline_email == listPengirim[i])
                        {
                            selected = true;
                            break;
                        }
                    }
                    list_pengirim.Add(new SelectListItem { Text = item.upline_name, Value = item.upline_email, Selected = selected });
                }
            }


            //List<SelectListItem> list_penerima = new List<SelectListItem>();
            //List<string> listPenerima = ObjResponse.surat.penerima.Split(',').ToList();
            //for (int i = 0; i < listPenerima.Count - 1; i++)
            //{
            //    var ct = ObjResponse.penerima.Where(c => c.email == listPenerima[i]).FirstOrDefault(); ;
            //    list_penerima.Add(new SelectListItem { Text = ct.username, Value = ct.email });
            //}

            List<SelectListItem> list_penerima = new List<SelectListItem>();
            if (ObjResponse.surat.penerima != null)
            {
                List<string> listPenerima = ObjResponse.surat.penerima.Split(',').ToList();


                foreach (var item in ObjResponse.penerima)
                {
                    bool selected = false;
                    for (int i = 0; i < listPenerima.Count - 1; i++)
                    {
                        if (item.email == listPenerima[i])
                        {
                            selected = true;
                            break;
                        }
                    }
                    if (selected == true)
                    {
                        list_penerima.Add(new SelectListItem { Text = item.username, Value = item.email, Selected = selected });
                    }
                    else
                    {
                        list_penerima.Add(new SelectListItem { Text = item.username, Value = item.email });
                    }
                }

            }

            List<SelectListItem> ContactPenerima = new List<SelectListItem>();
            foreach (var item in ObjResponse.penerima)
            {
                ContactPenerima.Add(new SelectListItem { Text = item.username + "(" + item.dev_id + ")", Value = item.email });
            }


            ViewBag.Templates = Templates;
            ViewBag.Tipe = Tipe;
            ViewBag.Lampiran = ObjResponse.lampiran;
            ViewBag.Chats = ObjResponse.chats.OrderBy(i => i.created_at);
            ViewBag.ListPengirim = list_pengirim;
            ViewBag.ListPenerima = list_penerima;
            ViewBag.ContactPenerima = ContactPenerima;
            ViewBag.Flow = ObjResponse.flow;

            PrintPdfViewModel ppdf = new PrintPdfViewModel();
            ppdf.surat = ObjResponse.surat;
            ppdf.template = template;

            ViewBag.Preview = ppdf;

            SURAT surat = new SURAT();
            surat = ObjResponse.surat;

            return View(surat);
        }

        // POST: SuratMasuk/Edit/5
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

        // POST: SuratMasuk/Edit/5
        [HttpPost]
        public async  Task<ActionResult> CreateChat(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
                string token = Session["token"].ToString();
                string user_id = Session["user_id"].ToString();
                string message = collection["message"];
                string batch_no = collection["batch_no"];
                CHAT chat = new CHAT();
                chat.batch_no = batch_no;
                chat.message = message;
                chat.created_by = user_id;

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(apiUrl);
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var jsonString = JsonConvert.SerializeObject(chat);
                    HttpContent httpContent = new StringContent(jsonString);
                    httpContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                    var response = await client.PostAsync("Surat/CreateChat/", httpContent);
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

        // GET: SuratKeluar/Sent/5
        public async Task<ActionResult> Approve(int id)
        {
            using (var client = new HttpClient())
            {
                string token = Session["token"].ToString();
                string user_id = Session["user_id"].ToString();
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.GetAsync("Surat/Approve/" + id + "/" + user_id);
                if (response.IsSuccessStatusCode)
                {
                    var Response = response.Content.ReadAsStringAsync().Result;
                }
            }

            //return RedirectToAction("Index");
            result.IsSucceed = true;
            result.Message = "Data has been Approved";
            result.InsertSucceed();
            return Json(new { result }, JsonRequestBehavior.AllowGet);
        }
    }
}
