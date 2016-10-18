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

        public ActionResult Photos()
        {

            return View();
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
        public void DeleteUser(string Name)
        {
            Membership.DeleteUser(Name);
        }
        //public UserViewModel EditUser(NewUser user)
        //{
        //    var editUser = Membership.GetUser(user.Name);
        //    editUser.ChangePassword(user)
        //    Membership.UpdateUser();
        //    MembershipUser.
        //}
        public void EditRole(string Name, string Role)
        {
            var role = Roles.GetRolesForUser(Name);
            Roles.RemoveUserFromRoles(Name,role);
            Roles.AddUserToRole(Name, Role);
        }

        public JsonResult AddRole(string RoleName)
        {
            Roles.CreateRole(RoleName);

            return Json(Roles.GetAllRoles(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult RemoveRole(string RoleName)
        {
            Roles.DeleteRole(RoleName);

            return Json(Roles.GetAllRoles(), JsonRequestBehavior.AllowGet);
        }
    }
}