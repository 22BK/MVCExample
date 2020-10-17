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

            return View("DepartmanForm", new Departman());
        }

        [HttpPost]
        public ActionResult Kaydet(Departman departman)
        {
            if (!ModelState.IsValid)
            {
                return View("DepartmanForm");
            }
            if (departman.Id==0)
            {
                db.Departman.Add(departman);
            }
            else
            {
                var guncellenecekDepartman = db.Departman.Find(departman.Id);
                if (guncellenecekDepartman==null)
                {
                    return HttpNotFound();
                }
                guncellenecekDepartman.Ad = departman.Ad;
            }
            db.SaveChanges();
            return RedirectToAction("Index","Departman");
        }

        public ActionResult Guncelle(int id)
        {
            var model = db.Departman.Find(id);
            if (model!=null)
            {
                return View("DepartmanForm", model);
            }
            return HttpNotFound(); 
        }

        public ActionResult Sil(int id)
        {
            var silinecekDepartman = db.Departman.Find(id);
            if (silinecekDepartman==null)
            {
                return HttpNotFound();
            }
            db.Departman.Remove(silinecekDepartman);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}