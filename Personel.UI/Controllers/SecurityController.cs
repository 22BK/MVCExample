using Personel.UI.Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Personel.UI.Controllers
{
   
    public class SecurityController : Controller
    {
        PersonelDBEntities db = new PersonelDBEntities();

        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]    
        [HttpPost]
        public ActionResult Login(Kullanici kullanici)
        {
            var kullaniciKontrol = db.Kullanici.FirstOrDefault(x=>x.Ad==kullanici.Ad && x.Sifre==kullanici.Sifre);
            if (kullaniciKontrol!=null)
            {
                FormsAuthentication.SetAuthCookie(kullaniciKontrol.Ad, false);
                return RedirectToAction("Index","Departman");
            }
            else
            {
                ViewBag.Mesaj = "GEçersiz kullanıcı adı ya da şifre";
                return View();
            }
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}