using Personel.UI.Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Personel.UI.ViewModels;

namespace Personel.UI.Controllers
{
    public class PersonelController : Controller
    {
        PersonelDBEntities db = new PersonelDBEntities();
        // GET: Personel
        public ActionResult Index()
        {
            var model = db.Personel.Include(x=>x.Departman).ToList();
            return View(model);
        }

        public ActionResult Yeni()
        {
            var model = new PersonelFormViewModel()
            {
                Departmanlar = db.Departman.ToList()
            };
            return View("PersonelForm",model
                );
        }

        public ActionResult Kaydet(Personel.UI.Models.EntityFramework.Personel personel)
        {
            if (personel.Id==0) // Kaydetme
            {
                db.Personel.Add(personel);
            }
            else //Güncelleme
            {
                   
            }
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}