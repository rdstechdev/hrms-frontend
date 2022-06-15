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
using Rotativa;
using System.IO;

namespace ServiceOne.Controllers
{
    [AuthorizeSession]
    public class SuratKeluarController : BaseController
    {

        // https://askarasoft.com/digital-sign/?utm_source=googleadsdms&utm_content=cid|15998899573|gid|132041322185|kwid|kwd-1642658709531&gclid=CjwKCAjwsJ6TBhAIEiwAfl4TWIhL-DTk61XDggV_F_hp6f0fDOnzSSrYA8mrnxG4tQMRYnC2I2t83BoC3l8QAvD_BwE
        // https://www.bitrix24.com/




        string apiUrl = Properties.Settings.Default.ApiUrl;
        private ProcessResult result = new ProcessResult();
        // GET: SuratKeluar
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
                var response = await client.GetAsync("Surat/Keluar/" + user_id);
                if (response.IsSuccessStatusCode)
                {
                    var Response = response.Content.ReadAsStringAsync().Result;
                    ObjResponse = JsonConvert.DeserializeObject<List<V_SURAT>>(Response);
                }
            }
            return View(ObjResponse);
        }

        // GET: SuratKeluar/Create
        public async Task<ActionResult> Create()
        {
            SusratViewModel ObjResponse = new SusratViewModel();
            using (var client = new HttpClient())
            {
                string token = Session["token"].ToString();
                string user_id = Session["user_id"].ToString();
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.GetAsync("Surat/Create/" + user_id);
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

            List<SelectListItem> pengirim = new List<SelectListItem>();
            foreach (var item in ObjResponse.pengirim)
            {
                pengirim.Add(new SelectListItem { Text = item.upline_name, Value = item.upline_email });
            }

            List<SelectListItem> penerima = new List<SelectListItem>();
            foreach (var item in ObjResponse.penerima)
            {
                penerima.Add(new SelectListItem { Text = item.username + "(" + item.dev_id + ")", Value = item.email });
            }


            ViewBag.Templates = Templates;
            ViewBag.Tipe = Tipe;
            ViewBag.Pengirim = pengirim;
            ViewBag.Penerima = penerima;
            ViewBag.Lampiran = ObjResponse.lampiran;

            SURAT surat = new SURAT();
            return View(surat);
        }

        // POST: Users/Create
        [HttpPost]
        public async Task<ActionResult> Create(SURAT collection, HttpPostedFileBase[] files)
        {
            try
            {
                // TODO: Add insert logic here
                string pengirim = string.Empty;
                if (collection.list_pengirim != null)
                {
                    for (int i = 0; i < collection.list_pengirim.Count; i++)
                    {
                        pengirim = pengirim + collection.list_pengirim[i] + ",";
                    }
                    collection.pengirim = pengirim;
                }
                else
                {
                    collection.pengirim = null;
                }

                string penerima = string.Empty;
                if (collection.list_penerima != null)
                {
                    for (int i = 0; i < collection.list_penerima.Count; i++)
                    {
                        penerima = penerima + collection.list_penerima[i] + ",";
                    }
                    collection.penerima = penerima;
                }
                else
                {
                    collection.penerima = null;
                }

                
                List<FileViewModel> newFiles = new List<FileViewModel>();
                //iterating through multiple file collection   
                int no = 1;
                foreach (HttpPostedFileBase file in files)
                {
                    //Checking file is available to save.  
                    if (file != null)
                    {
                        var InputFileName = Path.GetFileName(file.FileName);
                        var ServerSavePath = Path.Combine(Server.MapPath("~/UploadedFiles/") + InputFileName);
                        string theFileName = Path.GetFileName(file.FileName);
                        byte[] thePictureAsBytes = new byte[file.ContentLength];
                        long file_size = new System.IO.FileInfo(ServerSavePath).Length;
                        using (BinaryReader theReader = new BinaryReader(file.InputStream))
                        {
                            thePictureAsBytes = theReader.ReadBytes(file.ContentLength);
                        }
                        string thePictureDataAsString = Convert.ToBase64String(thePictureAsBytes);
                        newFiles.Add(new FileViewModel { id = 0, batch_no = "", no_surat = "", nama_file = file.FileName, content_type = file.ContentType, binary_file = thePictureDataAsString , file_size = file_size});
                        no++;
                    }
                }

                CreateSuratViewModel csv = new CreateSuratViewModel();
                csv.surat = collection;
                csv.files = newFiles;

                using (var client = new HttpClient())
                {
                    string token = Session["token"].ToString();
                    string user_id = Session["user_id"].ToString();
                    collection.created_by = user_id;
                    client.BaseAddress = new Uri(apiUrl);
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var jsonString = JsonConvert.SerializeObject(csv);
                    HttpContent httpContent = new StringContent(jsonString);
                    httpContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                    var response = await client.PostAsync("Surat/", httpContent);
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

        // POST: SuratKeluar/Edit
        [HttpPost]
        public async Task<ActionResult> Edit(int id,SURAT collection, HttpPostedFileBase[] files)
        {
            try
            {
                string token = Session["token"].ToString();
                string user_id = Session["user_id"].ToString();

                string pengirim = string.Empty;
                if (collection.list_pengirim != null)
                {
                    for (int i = 0; i < collection.list_pengirim.Count; i++)
                    {
                        pengirim = pengirim + collection.list_pengirim[i] + ",";
                    }
                    collection.pengirim = pengirim;
                }
                else
                {
                    collection.mengetahui = null;
                }

                string penerima = string.Empty;
                if(collection.list_penerima != null)
                {
                    for (int i = 0; i < collection.list_penerima.Count; i++)
                    {
                        penerima = penerima + collection.list_penerima[i] + ",";
                    }
                    collection.penerima = penerima;
                }
                else
                {
                    collection.penerima = null;
                }

                List<FileViewModel> newFiles = new List<FileViewModel>();
                //iterating through multiple file collection   
                int no = 1;
                foreach (HttpPostedFileBase file in files)
                {
                    //Checking file is available to save.  
                    if (file != null)
                    {
                        var InputFileName = Path.GetFileName(file.FileName);
                        var ServerSavePath = Path.Combine(Server.MapPath("~/UploadedFiles/") + InputFileName);
                        string theFileName = Path.GetFileName(file.FileName);
                        byte[] thePictureAsBytes = new byte[file.ContentLength];
                        long file_size = thePictureAsBytes.Length;
                        using (BinaryReader theReader = new BinaryReader(file.InputStream))
                        {
                            thePictureAsBytes = theReader.ReadBytes(file.ContentLength);
                        }
                        string thePictureDataAsString = Convert.ToBase64String(thePictureAsBytes);
                        newFiles.Add(new FileViewModel { 
                            id = no, batch_no = collection.batch_no, 
                            no_surat = collection.no_surat,
                            nama_file = file.FileName, 
                            content_type = file.ContentType, 
                            binary_file = thePictureDataAsString, 
                            file_size = file_size,
                            created_by = user_id,
                        });
                        
                        //Save file to server folder  
                        //file.SaveAs(ServerSavePath);
                        //assigning file uploaded status to ViewBag for showing message to user.  
                        //ViewBag.UploadStatus = files.Count().ToString() + " files uploaded successfully.";
                        no ++;
                    }
                }

                using (var client = new HttpClient())
                {
                    //collection.created_by = user_id;
                    client.BaseAddress = new Uri(apiUrl);
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var jsonString = JsonConvert.SerializeObject(newFiles);
                    HttpContent httpContent = new StringContent(jsonString);
                    httpContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                    var response = await client.PostAsync("Surat/PostFile/" + collection.batch_no, httpContent);
                    if (response.IsSuccessStatusCode)
                    {
                        var Response = response.Content.ReadAsStringAsync().Result;
                    }
                }

                // TODO: Add insert logic here
                using (var client = new HttpClient())
                {
                    collection.created_by = user_id;
                    client.BaseAddress = new Uri(apiUrl);
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var jsonString = JsonConvert.SerializeObject(collection);
                    HttpContent httpContent = new StringContent(jsonString);
                    httpContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                    var response = await client.PutAsync("Surat/"+ id, httpContent);
                    if (response.IsSuccessStatusCode)
                    {
                        var Response = response.Content.ReadAsStringAsync().Result;
                    }
                }
                return RedirectToAction("Edit", new {id = id});
            }
            catch
            {
                return View();
            }
        }

        // GET: SuratKeluar/DeleteFile/5
        public async Task<ActionResult> DeleteFile(int id)
        {
            using (var client = new HttpClient())
            {
                string token = Session["token"].ToString();
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.DeleteAsync("Surat/DeleteFile/" + id);
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

        // GET: SuratKeluar/Sent/5
        public async Task<ActionResult> Sent(int id)
        {
            using (var client = new HttpClient())
            {
                string token = Session["token"].ToString();
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.GetAsync("Surat/Sent/" + id);
                if (response.IsSuccessStatusCode)
                {
                    var Response = response.Content.ReadAsStringAsync().Result;
                }
            }

            //return RedirectToAction("Index");
            result.IsSucceed = true;
            result.Message = "Data has been Sent";
            result.InsertSucceed();
            return Json(new { result }, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> PrintPDF(int id)
        {
            // https://www.c-sharpcorner.com/article/export-pdf-from-html-in-mvc-net4/

            PrintPdfViewModel ppdf = new PrintPdfViewModel();
            using (var client = new HttpClient())
            {
                string token = Session["token"].ToString();
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.GetAsync("Surat/PrintPdf/" + id);
                if (response.IsSuccessStatusCode)
                {
                    var Response = response.Content.ReadAsStringAsync().Result;
                    ppdf = JsonConvert.DeserializeObject<PrintPdfViewModel>(Response);
                }
            }

            return new PartialViewAsPdf("_Preview", ppdf)
            {
                FileName = ppdf.surat.no_surat + ".pdf"
            };
        }

        public async Task<ActionResult> Preview(int id)
        {
            PrintPdfViewModel printPdfViewModel = new PrintPdfViewModel();
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

            var template = ObjResponse.templates.Where(m=> m.kode_surat==ObjResponse.surat.kode_surat).FirstOrDefault();

            printPdfViewModel.surat = ObjResponse.surat;
            printPdfViewModel.template = template;
            return PartialView("_Preview", printPdfViewModel);
        }

        public async Task<ActionResult> Print(int id)
        {
            PrintPdfViewModel printPdfViewModel = new PrintPdfViewModel();
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

            var template = ObjResponse.templates.Where(m => m.kode_surat == ObjResponse.surat.kode_surat).FirstOrDefault();

            printPdfViewModel.surat = ObjResponse.surat;
            printPdfViewModel.template = template;
            printPdfViewModel.lampiran = ObjResponse.lampiran.Count();
            printPdfViewModel.pengirim = ObjResponse.pengirim.FirstOrDefault();
            return PartialView("_Print", printPdfViewModel);
        }

        // POST: SuratMasuk/Edit/5
        [HttpPost]
        public async Task<ActionResult> CreateChat(int id, FormCollection collection)
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
    }
}