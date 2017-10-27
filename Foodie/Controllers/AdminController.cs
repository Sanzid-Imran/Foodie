using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using Foodie.Models;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Web.Security;

namespace Foodie.Controllers
{
    public class AdminController : Controller
    {
        //
        // GET: /Admin/
        ResContext db = new ResContext();

        public ActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogIn(string userName, string userPassword)
        {
            string dbUserName = userName;
            string dbUserPassoword = userPassword;

            var userAuth =
                db.adminLists.SingleOrDefault(vari => vari.userName == dbUserName && vari.userPassword == dbUserPassoword);
            if (userAuth != null)
            {
                 ViewBag.Message = "Login Successful";

                return RedirectToAction("AdminPage");
            }
            else
            {
                ViewBag.Message = "Login Failed";
                
                return RedirectToAction("LogIn");
            }
            
        }

        public ActionResult logInTemp()
        {
            return View();
        }

        public ActionResult AdminPage()
        {
            return View();
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult AdminPage(string title, string description, string selector, HttpPostedFileBase file)
        {
            var path = "";
            var photoPath = "";

            if (file != null)
            {
                if (file.ContentLength > 0)
                {
                    if (Path.GetExtension(file.FileName).ToLower() == ".jpg"
                        || Path.GetExtension(file.FileName).ToLower() == ".png"
                              || Path.GetExtension(file.FileName).ToLower() == ".gif"
                                    || Path.GetExtension(file.FileName).ToLower() == ".jpeg")
                    {
                        path = Path.Combine(Server.MapPath("~/Content/images"), file.FileName);
                        file.SaveAs(path);
                        //photoPath = "~/Images/" + file.FileName; 
                        photoPath = file.FileName;
                    }
                }
            }

            var model = (from p in db.FoodDetails where (p.selector == selector) select p).FirstOrDefault();

            model.foodTitle = title;
            model.foodDescription = description;
            model.foodPhoto = photoPath;

            db.Entry(model).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("AdminPage");

        }
        [HttpPost]
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }

      

    }
}