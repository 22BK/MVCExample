using Personel.UI.Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Personel.UI.Controllers
{
    public class DepartmanController : Controller
    {
        PersonelDBEntities db = new PersonelDBEntities();
        // GET: Departman
        public ActionResult Index()
        {
            var model = db.Departman.ToList();
            return View(model);
        }

        [HttpGet]
        public ActionResult Yeni()
        {
            
            return View();
        }

        [HttpPost]
        public ActionResult Yeni(Departman departman)
        {
            db.Departman.Add(departman);
            db.SaveChanges();
            return RedirectToAction("Index","Departman");
        }
    }
}