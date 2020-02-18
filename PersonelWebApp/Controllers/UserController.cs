using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WebService;
using WebService.Controllers;
using WebService.Models;
using PersonelWebApp.Models;
using Microsoft.AspNetCore.Http;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PersonelWebApp.Controllers
{
    
    public class UserController : Controller
    {


        // GET: /<controller>/
        
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginModel lmodel)
        {
            if (ModelState.IsValid)
            {
                if ((lmodel.UserName == Admin.username) && (lmodel.UserPassword == Admin.password))
                {
                    HttpContext.Session.SetInt32("loggedin", 1);
                    return RedirectToAction("WelcomePageAdmin");
                }
                using (var client = new HttpClient())
                {
                    List<UserInfo> users = new List<UserInfo>();
                    using (var httpClient = new HttpClient())
                    {
                        using (var response = await httpClient.GetAsync("http://localhost:17169/api/user"))
                        {
                            string apiResponse = await response.Content.ReadAsStringAsync();
                            users = JsonConvert.DeserializeObject<List<UserInfo>>(apiResponse);
                            
                        }

                    }

                    var user = users.FindAll(c => c.UserName == lmodel.UserName);
                    if (user.FirstOrDefault() != null)
                    {
                        
                        if (user.FirstOrDefault().UserPassword == lmodel.UserPassword)
                        {
                            HttpContext.Session.SetInt32("loggedin",1);
                            HttpContext.Session.SetString("loggedinUser", lmodel.UserName);
                           
                            return RedirectToAction("WelcomePage", new { @username = lmodel.UserName });

                        }

                    }
                    else
                    {
                        HttpContext.Session.SetInt32("loggedin", 0);
                        ModelState.AddModelError("", "Invalid login credentials.");
                    }
                }
            }

            return View(lmodel);
        }
        
        public async Task<ActionResult> Delete(int id)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response1 = await httpClient.DeleteAsync("http://localhost:17169/api/user/" + id))
                {

                    if (response1.IsSuccessStatusCode)
                    {
                        HttpContext.Session.SetInt32("loggedin", 1);

                        return RedirectToAction("WelcomePageAdmin");
                    }
                }
            }
            return RedirectToAction("WelcomePage");
        }
        public async Task<IActionResult> Edit(string username)
        {
           
            if (HttpContext.Session.GetInt32("loggedin") == 1)
            {
                if (HttpContext.Session.GetString("loggedinUser") == username)
                {

                
                    using (var client = new HttpClient())
                    {


                        using (var httpClient = new HttpClient())
                        {
                            using (var response = await httpClient.GetAsync("http://localhost:17169/api/user"))
                            {
                                string apiResponse = await response.Content.ReadAsStringAsync();
                                var users = JsonConvert.DeserializeObject<List<UserInfo>>(apiResponse);
                                var user = users.Find(c => c.UserName == username);

                                if (user != null)
                                {
                                    return View(user);
                                }
                            }

                        }


                    }
                }
                else
                {
                    HttpContext.Session.SetInt32("loggedin",0);
                    return Redirect("/user/login");
                }
            }
            HttpContext.Session.SetInt32("loggedin", 0);

            return Redirect("/user/login");
        }
        public ActionResult Logout()
        {
            HttpContext.Session.SetInt32("loggedin", 0);
            return Redirect("/user/login");
        }
        public async Task<ActionResult> WelcomePageAdmin()
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:17169/api/user"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    var users = JsonConvert.DeserializeObject<List<UserInfo>>(apiResponse);
                    return View(users);
                }

            }
           
        }

        [HttpPost("{username}")]
       public async Task<IActionResult> Edit(string username, [Bind("UserInfoId,PersonelAd,PersonelSoyad,PersonelTcNo,UserName,UserPassword")] UserInfo user)
        {
            
            if (HttpContext.Session.GetInt32("loggedin") == 1)
            {
                if (ModelState.IsValid)
                {
                    using (var httpClient = new HttpClient())
                    {
                        using (var response = await httpClient.GetAsync("http://localhost:17169/api/user"))
                        {
                            string apiResponse = await response.Content.ReadAsStringAsync();
                            var users = JsonConvert.DeserializeObject<List<UserInfo>>(apiResponse);
                            var luser = users.Find(c => c.UserName == username);
                            using (var response1 = await httpClient.PutAsJsonAsync("http://localhost:17169/api/user/"+luser.UserInfoId , user))
                            {

                                if (response1.IsSuccessStatusCode)
                                {
                                    HttpContext.Session.SetInt32("loggedin", 0);

                                    return Redirect("/user/login");
                                }
                            }
                        }

                    }
                    

                }
                ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

                return View();
            }

            return View("/user/login");
        }

        public async Task<IActionResult> WelcomePage()
        {
            if (HttpContext.Session.GetInt32("loggedin") == 1)
            {
                return View();
            }
            return Redirect("/user/login");
        }
        [HttpGet("{username}")]
        public async Task<IActionResult> WelcomePage(string username)
        {

            if (HttpContext.Session.GetInt32("loggedin")==1)
            {
                using (var client = new HttpClient())
                {

                    List<UserInfo> users = new List<UserInfo>();

                    using (var httpClient = new HttpClient())
                    {
                        using (var response = await httpClient.GetAsync("http://localhost:17169/api/user"))
                        {
                            string apiResponse = await response.Content.ReadAsStringAsync();
                            users = JsonConvert.DeserializeObject<List<UserInfo>>(apiResponse);
                            var user = users.Find(c=> c.UserName==username);
                       
                        if (user!=null)
                        {
                            return View(user);
                        }
                        }
                    }
                }
            }
            return Redirect("/user/login");

        }
        public  ActionResult toCreate()
        {

            return Redirect("/user/create");
        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("UserInfoId,PersonelAd,PersonelSoyad,PersonelTcNo,UserName,UserPassword")] UserInfo user)
        {
            
            
            if (ModelState.IsValid)
            {
                List<UserInfo> users = new List<UserInfo>();
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync("http://localhost:17169/api/user"))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        users = JsonConvert.DeserializeObject<List<UserInfo>>(apiResponse);
                    }

                }
                if (users.FindAll(c=> c.UserName==user.UserName).Count==0)
                {
                    
                        using (var httpClient = new HttpClient())
                        {
                            using (var response = await httpClient.PostAsJsonAsync("http://localhost:17169/api/user", user))
                            {

                                if (response.IsSuccessStatusCode)
                                {
                                    return Redirect("/user/login");
                                }
                            }
                        

                    }
                    ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

                    return View();
                }
                ViewBag.ErrorMessage = "Kullanıcı adı çoktan alınmış. Başka bir kullanıcı adı deneyin.";
                
            }
            return View();
        }




       
    }
}
