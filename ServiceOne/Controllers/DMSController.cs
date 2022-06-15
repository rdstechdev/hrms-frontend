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
    public class DMSController : BaseController
    {
        string apiUrl = Properties.Settings.Default.ApiUrl;
        private ProcessResult result = new ProcessResult();

        // GET: DMS
        public async Task<ActionResult> Index()
        {
            List<SP_DMS> ObjResponse = new List<SP_DMS>();
            using (var client = new HttpClient())
            {
                string token = Session["token"].ToString();
                string user_id = Session["user_id"].ToString();
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.GetAsync("DMS");
                if (response.IsSuccessStatusCode)
                {
                    var Response = response.Content.ReadAsStringAsync().Result;
                    ObjResponse = JsonConvert.DeserializeObject<List<SP_DMS>>(Response);
                }
            }
            return View(ObjResponse);
        }

        // GET: DMS/Details/5
        public async Task<ActionResult> Details(string batch_no)
        {
            List<LAMPIRAN> ObjResponse = new List<LAMPIRAN>();
            using (var client = new HttpClient())
            {
                string token = Session["token"].ToString();
                string user_id = Session["user_id"].ToString();
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.GetAsync("DMS/batch_no/"+ batch_no);
                if (response.IsSuccessStatusCode)
                {
                    var Response = response.Content.ReadAsStringAsync().Result;
                    ObjResponse = JsonConvert.DeserializeObject<List<LAMPIRAN>>(Response);
                }
            }
            ViewBag.BatchNo = batch_no;
            return View(ObjResponse);
        }
    }
}