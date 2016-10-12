using PhotoGallary.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace PhotoGallary.Web.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel login)
        {
            if (ModelState.IsValid)
            {
                if (Membership.ValidateUser(login.Name, login.Password))
                {
                    FormsAuthentication.SetAuthCookie(login.Name, true);
                    //FormsAuthentication.RedirectFromLoginPage();
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError("", "Invalid login/password");
            }

            return View(login);
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }


        public void Init()
        {
            Membership.CreateUser("Andrey", "123456");
            Roles.CreateRole("admin");
            Roles.CreateRole("superadmin");
            Roles.AddUserToRole("Andrey", "admin");
        }

    }
}