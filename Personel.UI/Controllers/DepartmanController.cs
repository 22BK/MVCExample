using Personel.UI.Models.EntityFramework;
using Personel.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Personel.UI.Controllers
{
    [Authorize(Roles = "A,U")]
    public class DepartmanController : Controller
    {
        PersonelDBEntities db = new PersonelDBEntities();
        
        
        public ActionResult Index()
        {
            var model = db.Departman.ToList();
            //int a = 0, b = 10;
            //int c = b / a; // runtime error oluşturmak için
            return View(model);
        }

        [HttpGet]
        public ActionResult Yeni()
        {

            return View("DepartmanForm", new Departman());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Kaydet(Departman departman)
        {
            if (!ModelState.IsValid)
            {
                return View("DepartmanForm");
            }

            MesajViewModel model = new MesajViewModel();
            if (departman.Id==0)
            {
                db.Departman.Add(departman);
                model.Mesaj =departman.Ad+ " departmanı eklendi";
            }
            else
            {
                var guncellenecekDepartman = db.Departman.Find(departman.Id);
                if (guncellenecekDepartman==null)
                {
                    return HttpNotFound();
                }
                guncellenecekDepartman.Ad = departman.Ad;
                model.Mesaj = departman.Ad + " departmanı güncellendi";
            }
            db.SaveChanges();
            model.Status = true;
            model.LinkText = "Departman Listesi";
            model.Url = "/Departman";
            return View("_Mesaj",model);
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