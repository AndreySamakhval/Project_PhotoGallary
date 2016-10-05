using PhotoGallary.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PhotoGallary.Web.Controllers
{
    public class HomeController : Controller
    {
        PhotoService _service = new PhotoService();

        public ActionResult Index()
        {

            return View(_service.GetPhotos());
        }

        public JsonResult Genres()
        {
            
           return Json( _service.GetGenres(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult Photos(int id = 0)
        {
            return Json(_service.GetPhotos(id), JsonRequestBehavior.AllowGet);
        }
    }
}