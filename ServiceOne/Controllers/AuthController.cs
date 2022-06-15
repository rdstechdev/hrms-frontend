using Newtonsoft.Json;
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
    public class AuthController : Controller
    {
        // GET: Auth
        public ActionResult Index()
        {
            return View();
        }

        // POST: Auth/Login
        [HttpPost]
        public async Task<ActionResult> Login(LoginViewModel collection)
        {
            //http://117.54.234.54:8081/dev-service-one-api/api/
            string apiUrl = Properties.Settings.Default.ApiUrl;
            ResponseLoginViewModel ObjResponse = new ResponseLoginViewModel();
            try
            {
                //string email = collection["email"];
                //string password = collection["password"];

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(apiUrl);
                    var jsonString = JsonConvert.SerializeObject(collection);
                    HttpContent httpContent = new StringContent(jsonString);
                    httpContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                    var response = await client.PostAsync("Auth/Login", httpContent);
                    if (response.IsSuccessStatusCode)
                    {
                        var Response = response.Content.ReadAsStringAsync().Result;
                        ObjResponse = JsonConvert.DeserializeObject<ResponseLoginViewModel>(Response);

                    }
                    else
                    {
                        //ModelState.AddModelError(string.Empty, "Server error try after some time.");
                        return View("Index");
                    }
                }

                if (ObjResponse.code == 200)
                {
                    string menusJson;
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(apiUrl);

                        string token = ObjResponse.token;
                        client.BaseAddress = new Uri(apiUrl);
                        client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        var response = await client.GetAsync("svc/Menus?user_id="+ ObjResponse.user_id);
                        if (response.IsSuccessStatusCode)
                        {
                            var Response = response.Content.ReadAsStringAsync().Result;
                            menusJson = Response;
                        }
                        else
                        {
                            return View("Index");
                        }
                    }

                    Session["user_id"]= ObjResponse.user_id.ToString();
                    Session["username"] = ObjResponse.username;
                    Session["role_id"] = ObjResponse.role_id;
                    Session["role"] = ObjResponse.role;
                    Session["role_name"] = ObjResponse.role;
                    Session["email"] = collection.user_id;
                    Session["token"] = ObjResponse.token;
                    Session["menu"] = menusJson;

                    return Redirect("~/Home/Index");
                }
                else
                {
                    return View("Index");
                }

            }
            catch
            {
                return View();
            }
        }

        
    }
}
