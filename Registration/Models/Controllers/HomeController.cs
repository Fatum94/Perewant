using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Configuration;
using Registration.Models;
using System.Data;
using System.Text.RegularExpressions;
using System.IO;
using System.Text;
using System.Security;


namespace System.Web.Security
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index(User user)
        {
            if (Request.Cookies["auth_test"] == null || Request.Cookies["auth_test"].Value == null)
            {
                return RedirectToAction("Register");
            }

            return View();
        }
        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase FileUpload)
        {
            if (FileUpload != null && FileUpload.ContentLength > 0)
            {
                try
                {
                    ProcessCSV(FileUpload);
                }
                catch (Exception ex)
                {
                    ViewData["Feedback"] = ex.Message;
                }
            }
            return RedirectToAction("Index");
        }

        public ActionResult Registration()
        {
            ViewData["Message"] = "Register Here!";
            var model = new User();
            return View("Registration", model);
        }

        public ActionResult WatchDB(User user)
        {
            try
            {
                ViewData = getFromTable(user);
                return RedirectToAction("Index");
            }
            catch (InvalidCastException e)
            {
                return RedirectToAction("Register");
            }
        }

        public ActionResult Register(User user)
        {
            return View();
        }
        public ActionResult LogOut()
        {
            HttpCookie myCookie = new HttpCookie("auth_test");
            myCookie.Expires = DateTime.Now.AddDays(-1d);
            Response.Cookies.Add(myCookie);
            return RedirectToAction("Register");
        }

        public ViewDataDictionary getFromTable(User user)
        {
            var database = new Database();
            var userLine = database.Users.Where(u => u.Name == user.Name).FirstOrDefault();
            if (userLine.Password == user.Password)
            {
                var hash = Convert.ToBase64String(
                      System.Security.Cryptography.MD5.Create()
                      .ComputeHash(Encoding.UTF8.GetBytes(userLine.Password))
                    );


                var AuthCookie = new HttpCookie("auth_test")
                {
                    Value = hash,
                    Expires = DateTime.Now.Add(FormsAuthentication.Timeout)
                };
                user.isAuth = true;
                HttpContext.Response.Cookies.Set(AuthCookie);
            }

            return null;
        }

        public ActionResult InsertCompressorCharacter(Kompressor compr)
        {
            if (ModelState.IsValid)
            {
                var database = new Database();
                database.Compressor.Add(new Kompressor { PressIn = compr.PressIn, PressOut = compr.PressOut, Performance = compr.Performance, Rodo = compr.Rodo });
                database.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("Index");
        }

        public ActionResult ConvertDataToCSV()
        {
            var database = new Database();
            var arr = database.Compressor.ToArray();
            var path = HttpContext.Server.MapPath("~/App_Data/uploads/history.csv");
            var sw = new StreamWriter(path);
            var j = 0;          

            foreach (var rows in arr) {
                sw.WriteLine(rows.PressIn + "," + rows.PressOut + "," + rows.Performance + "," + rows.Rodo);
            }
            sw.Dispose();
            return RedirectToAction("Index");
        }

        private void CreateCookie(string userName, bool isPersistent = false)
        {
            var ticket = new FormsAuthenticationTicket(
                  1,
                  userName,
                  DateTime.Now,
                  DateTime.Now.Add(FormsAuthentication.Timeout),
                  isPersistent,
                  string.Empty,
                  FormsAuthentication.FormsCookiePath);

            // Encrypt the ticket.
            var encTicket = FormsAuthentication.Encrypt(ticket);

            // Create the cookie.
            var AuthCookie = new HttpCookie("auth_test")
            {
                Value = encTicket,
                Expires = DateTime.Now.Add(FormsAuthentication.Timeout)
            };
            HttpContext.Response.Cookies.Set(AuthCookie);
        }

        private void ProcessCSV(HttpPostedFileBase FileUpload)
        {
            var database = new Database();

            //Set up our variables
            string Feedback = string.Empty;
            string line = string.Empty;
            string[] strArray = { "", "", "", "" };
            // work out where we should split on comma, but not in a sentence
            Regex r = new Regex(",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");
            //Set the filename in to our stream
            using (var sr = new StreamReader(FileUpload.InputStream))
                    {
                        while ((line = sr.ReadLine()) != null)
                        {

                            //add our current value to our data row
                            strArray = r.Split(line);

                            database.Compressor.Add(new Kompressor { PressIn = strArray[0], PressOut = strArray[1], Performance = strArray[2], Rodo = strArray[3] });
                            database.SaveChanges();
                        }
            }

        }
    }

}

