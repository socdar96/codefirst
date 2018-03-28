using SampleCodeFirstIn.Context;
using SampleCodeFirstIn.Models;
using SampleCodeFirstIn.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SampleCodeFirstIn.Controllers
{
    public class AccountController : Controller
    {
        InviContext db = new InviContext();
        // GET: Account
        public ActionResult Login()
        {
            return View();
        }
        //Login
        //returns  0 - invalid user or pass / 1 - valid user or pass / 2 - lock user status
        public int validateUser(String _UserName, String _Password) 
        {
            try
            {
                User user = db.Users.Single(p => p.userName == _UserName);
                string truePassword = getTruePassword(user.Pwd);
                if (truePassword == _Password && user.disabled == false)
                {
                    int empID = user.userId;
                    Session["userid"] = empID;
                    Session["username"] = _UserName;
                    Session.Timeout = 15;
                    return 1;
                }
                else if (getTruePassword(user.Pwd) == _Password && user.disabled == true)
                {
                    return 2;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return 0;
            }
            return 0; // return 0 to notify invalid input
        }
        public ActionResult LogOff()
        {
            Session["userid"] = null;
            Session["username"] = null;
            HttpContext.Session.Clear();
            HttpContext.Session.Abandon();
            HttpContext.Session.RemoveAll();
            Response.Cookies.Add(new HttpCookie("ASP.NET_SessionId", ""));
            return RedirectToAction("Login", "Account");
        }
        public ActionResult CheckLogin(String _UserName, String _Password)
        {
            int Log = validateUser(_UserName, _Password);

            if (Log == 1)
                return Redirect("~/Home/Main");
            else if (Log == 2)
                return Redirect("home/Lock");

            return Redirect("Account/login");

        }
        public JsonResult IsUserNameExist(string userName)
        {
            bool isExist = db.Users.Where(m => m.userName.ToLower().Equals(userName.ToLower())).FirstOrDefault() != null;
            return Json(isExist, JsonRequestBehavior.AllowGet);
        }
        public String decryptPassword(int userid)
        {
            String recoveredpassword = "";
            User user = db.Users.Single(p => p.userId == userid);
            string oldpass = user.Pwd;
            try
            {
                recoveredpassword = MD5Crypt.Decrypt(oldpass, "admin", true);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                user.Pwd = MD5Crypt.Encrypt(user.Pwd, "admin", true);
                try
                {
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return recoveredpassword;
        }
        public int saveNewPassword(int userid, String newPassword)
        {
            int success = 0;
            User user = db.Users.Single(p => p.userId == userid);
            user.Pwd = MD5Crypt.Encrypt(newPassword, "admin", true);

            //string recipient = "soc.alcantara.delonix.com.au";
            try
            {
                success = db.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            //new HRIS_WEB.Controllers.TimecardController().sendMail(recipient, null, "Password Reset.", mailbody.ToString());
            return success;
        }
        public String getTruePassword(String password)
        {
            return MD5Crypt.Decrypt(@password, "admin", true);
        }
    }
}