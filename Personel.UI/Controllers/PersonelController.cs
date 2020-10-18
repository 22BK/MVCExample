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
    [Authorize(Roles = "A,U")]
    public class PersonelController : Controller
    {
        PersonelDBEntities db = new PersonelDBEntities();
        
        [OutputCache(Duration =30)] // aşağıdaki db sorgusunu cache e atıp 30saniyede bir yenilemesini sağlıyor büyü ksorgularda performans arttırıcı olarak kullanılabilir fakat yeni eklenen personel ya da güncellenen personel varsa 30sn dolduktan sonra listede görüntülenebilir
        public ActionResult Index()
        {
            var model = db.Personel.Include(x=>x.Departman).ToList();
            return View(model);
        }
        
        public ActionResult Yeni()
        {
            var model = new PersonelFormViewModel()
            {
                Departmanlar = db.Departman.ToList(),
                Personel = new Personel.UI.Models.EntityFramework.Personel()
            };
            return View("PersonelForm",model
                );
        }

        [ValidateAntiForgeryToken]
        public ActionResult Kaydet(Personel.UI.Models.EntityFramework.Personel personel)
        {
            if (!ModelState.IsValid)
            {
                var model = new PersonelFormViewModel()
                {
                    Departmanlar = db.Departman.ToList(),
                    Personel = personel
                };
                return View("PersonelForm", model);
            }
            if (personel.Id==0) // Kaydetme
            {
                db.Personel.Add(personel);
            }
            else //Güncelleme
            {
                db.Entry(personel).State = System.Data.Entity.EntityState.Modified;   
            }
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Guncelle(int id)
        {
            var model = new PersonelFormViewModel()
            {
                Departmanlar=db.Departman.ToList(),
                Personel=db.Personel.Find(id)
            };
            return View("PersonelForm",model);
        }

        public ActionResult Sil(int id)
        {
            var silinecekPersonel = db.Personel.Find(id);
            if (silinecekPersonel==null)
            {
                return HttpNotFound();
            }
            db.Personel.Remove(silinecekPersonel);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult PersonelleriListele(int id)
        {
            var model = db.Personel.Where(x=>x.Departman.Id==id).ToList();
            return PartialView(model);
        }

        public ActionResult ToplamMaas()
        {
            ViewBag.Maas = db.Personel.Sum(x=>x.Maas);
            return PartialView();
        }
    }
}