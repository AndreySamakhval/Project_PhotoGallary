using PhotoGallary.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace PhotoGallary.Web.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        // GET: Admin
        //[Authorize(Roles = "admin")]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Users()
        {
            var users = new List<UserViewModel>();

            var _provider = Membership.Providers["SqlProvider"];
            int total;
            var data = _provider.GetAllUsers(0, 50, out total);

            foreach(MembershipUser user in data)
            {
                users.Add(new UserViewModel
                {
                    Name = user.UserName,
                    Roles = Roles.GetRolesForUser(user.UserName)
                });
            }

            return View(users);
        }
        public JsonResult GetRoles()
        {
            return Json(Roles.GetAllRoles(), JsonRequestBehavior.AllowGet);
        }

        public bool CreateUser(NewUser user)
        {
            if (ModelState.IsValid)
            {
                Membership.CreateUser(user.Name, user.Password);
                Roles.AddUserToRole(user.Name, user.Role);
                return true;
            }
            else return false;
        }
    }
}