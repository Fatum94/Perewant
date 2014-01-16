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
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace System.Web.Security
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index(User user)
        {
            //if (Request.Cookies["auth_test"] == null || Request.Cookies["auth_test"].Value == null)
            //{
            //    return RedirectToAction("Register");
            //}

            return View();
        }
        public ActionResult History()
        {
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
        public ActionResult SelectCompressor(ViewModel model)
        {
            var database = new Entities();
            var compressor = database.Compressor.Where(c => c.PressIn == model.First.PressIn);
            return Json(new {resp = compressor, success = true}, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Registration()
        {
            ViewData["Message"] = "Register Here!";
            var model = new User();
            return View("Registration", model);
        }
        [HttpPost]
        public ActionResult WatchDB(User user)
        {
            try
            {
                var database = new Entities();
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
                return Json(new { success = true });
            }
            catch (InvalidCastException e)
            {
                return RedirectToAction("Register");
            }
            return Json(new { success = true });
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
            

            return null;
        }

        [HttpPost]
        public ActionResult ConvertDataToCSV(ViewModel model)
        {
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }


        private void ProcessCSV(HttpPostedFileBase FileUpload)
        {
            var database = new Entities();

            //Set up our variables
            string Feedback = string.Empty;
            string line = string.Empty;

            var strArray = new string[11];
            // work out where we should split on comma, but not in a sentence
            Regex r = new Regex(",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");
            //Set the filename in to our stream
            using (var sr = new StreamReader(FileUpload.InputStream))
            {
                while ((line = sr.ReadLine()) != null)
                {

                    //add our current value to our data row
                    strArray = r.Split(line);

                    database.Compressor.Add(new Kompressor { PressIn = strArray[0], PressOut = strArray[1], Performance = strArray[2], Drive = strArray[3], Power = strArray[4], DegreesOfPressure = strArray[5], NumberOfCylinders = strArray[6], Bore = strArray[7], LengthOfStroke = strArray[8], SpeedOfRotation = strArray[9]});
                    database.SaveChanges();
                }
            }

        }
    }

}

