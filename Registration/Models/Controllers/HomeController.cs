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
using System.Web;
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
            if (Request.Cookies["auth_test"] != null && Request.Cookies["auth_test"].Value != null && Session["userName"] != null)
                return View();

            return RedirectToAction("Register");
        }
        public ActionResult History()
        {
            return View("History");
        }
        [HttpPost]
        public ActionResult Upload()
        {
            HttpPostedFileBase FileUpload = Request.Files["file"];
            if (FileUpload != null && FileUpload.ContentLength > 0)
            {
                try
                {
                    ProcessCSV(FileUpload);
                }
                catch (Exception ex)
                {
                    return Json(ex.Message, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new {success = true });
        }
        public ActionResult SelectCompressor(ViewModel model)
        {
            var database = new Database();
            var compressor = database.Compressor.Where(c => c.PressIn == model.First.PressIn);
            return Json(new {resp = compressor, success = true}, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Registration()
        {
            if (Request.Cookies["auth_test"] == null || Request.Cookies["auth_test"].Value == null)
                return RedirectToAction("Register");
            return View("Registration");
        }
        [HttpPost]
        public ActionResult WatchDB(User user)
        {
            try
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
                            Value = hash + "id" + userLine.Id,
                            Expires = DateTime.Now.Add(FormsAuthentication.Timeout)
                        };
                        user.isAuth = true;
                        HttpContext.Response.Cookies.Set(AuthCookie);
                        Session["userName"] = userLine.Name;
                    }
                    return Json(new { success = true, url = "/" });
                
            }
            catch (InvalidCastException e)
            {
                return RedirectToAction("Register");
            }
            return Json(new { success = true });
        }

        [HttpGet]
        public ActionResult Register () {
            return View();
        }
        [HttpPost]
        public ActionResult Register(User user)
        {
            try
            {
                var database = new Database();
                user.isAuth = true;
                database.Users.Add(user);
                database.SaveChanges();
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex) {
                return Json( new {error = ex.Message}, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult LogOut()
        {
            HttpCookie myCookie = new HttpCookie("auth_test");
            myCookie.Expires = DateTime.Now.AddDays(-1d);
            Response.Cookies.Add(myCookie);
            Session.Clear();
            return RedirectToAction("Register");
        }

        public ViewDataDictionary GetFromTable(User user)
        {
            return null;
        }

        [HttpGet]
        public ActionResult ConvertDataToCSV(ViewModel model)
        {
            var database = new Database();
            var arr = database.Compressor.ToArray();
            return Json(new {result = arr}, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult GetUserName(int id)
        {
            var name = (string) Session["userName"];
            return Json(new { response = name }, JsonRequestBehavior.AllowGet);
        }

        private void ProcessCSV(HttpPostedFileBase FileUpload)
        {
            var database = new Database();

            //Set up our variables
            string Feedback = string.Empty;
            string line = string.Empty;

            string [] strArray;
            // work out where we should split on comma, but not in a sentence
            Regex r = new Regex(",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");
            //Set the filename in to our stream
            using (var sr = new StreamReader(FileUpload.InputStream))
            {
                while ((line = sr.ReadLine()) != null)
                {

                    //add our current value to our data row
                    strArray = r.Split(line);
                    var PressIn = strArray[0];
                    var PressOut = strArray[1];
                    var Performance = strArray[2];
                    var Drive = strArray[3];
                    var Power = strArray[4];
                    var DegreesOfPressure = strArray[5];
                    var NumberOfCylinders = strArray[6];
                    var Bore = strArray[7];
                    var LengthOfStroke = strArray[8];
                    var SpeedOfRotation = strArray[9];
                    var fromDB =
                        database.Compressor.Where(c=>
                            c.PressIn ==
                            PressIn && c.PressOut ==
                                    PressOut && c.Performance ==
                                        Performance && c.Drive ==
                                            Drive && c.Power ==
                                                Power && c.DegreesOfPressure ==
                                                    DegreesOfPressure && c.NumberOfCylinders ==
                                                        NumberOfCylinders && c.Bore ==
                                                            Bore && c.LengthOfStroke ==
                                                                LengthOfStroke && c.SpeedOfRotation == SpeedOfRotation).FirstOrDefault();
                    if (fromDB == null)
                    {
                        database.Compressor.Add(new Kompressor
                        {
                            PressIn = strArray[0],
                            PressOut = strArray[1],
                            Performance = strArray[2],
                            Drive = strArray[3],
                            Power = strArray[4],
                            DegreesOfPressure = strArray[5],
                            NumberOfCylinders = strArray[6],
                            Bore = strArray[7],
                            LengthOfStroke = strArray[8],
                            SpeedOfRotation = strArray[9]
                        });
                        database.SaveChanges();
                    }
                    fromDB = null;

                }
            }

        }
    }

}

