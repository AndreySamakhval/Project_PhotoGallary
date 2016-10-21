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
       // PhotoService _service = new PhotoService();
        IPhotoService _service;
        public HomeController(IPhotoService Service)
        {
            _service = Service;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Genre(int id)
        {
            var genre = _service.GetGenre(id);
            return View(genre);
        }


        public JsonResult Genres()
        {
            
           return Json( _service.GetGenres(), JsonRequestBehavior.AllowGet);
        }

        public int AddGenre(string Name)
        {
            return _service.AddGenre(Name);
        }
        public bool RemoveGenre(int id)
        {
            return _service.RemoveGenre(id);
        }

        public JsonResult Photos(int id)
        {
            return Json(_service.GetPhotos(id), JsonRequestBehavior.AllowGet);
        }

        public JsonResult LastPhotos(int id)
        {
            return Json(_service.GetLastPhotos(id), JsonRequestBehavior.AllowGet);
        }
    }
}