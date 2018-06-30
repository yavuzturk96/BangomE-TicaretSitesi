using Bangom.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bangom.Controllers
{
    public class UyeController : Controller
    {
        private BangomEntities db = new BangomEntities();
        // GET: Uye
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Uye uye)
        {
            var login = db.Uye.Where(u => u.Email == uye.Email).SingleOrDefault();
            if (login.Email == uye.Email && login.Sifre == uye.Sifre)
            {
                Session["UyeID"] = login.UyeID;
                Session["Numara"] = login.Email;
                Session["YetkiID"] = login.YetkiID;
                Session["UyeAdi"] = login.UyeAdi;
                Session["UyeSoyadı"] = login.UyeSoyadi;
                Session["UyeEmail"] = login.Email;
                Session["Resim"] = login.UyeResmi;
                if (login.YetkiID == 1) { return Redirect("~/Admin/AdminHome/Index"); }
                else if (login.YetkiID == 2) { return Redirect("~/Home/Index"); /*RedirectToAction("Index", "Uye");*/ }
                else return Redirect("~/Home/Index"); /*RedirectToAction("Index", "Home");*/

            }
            else
            {
                ViewBag.Uyarı = "Numara ya da Şifrenizi kontrol ediniz!";
                return View();
            }

        }
        public ActionResult YeniUye()
        {
            return View();
        }
        [HttpPost]
        public ActionResult YeniUye(Uye model, HttpPostedFileBase file)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }


            Uye uye = new Uye();
            foreach (var item in db.Uye)
            {
                if (item.Email == model.Email)
                {
                    ViewBag.Uyarı = "Böyle bir E-Posta mevcut lütfen farklı bir E-Posta giriniz.";
                    return View();
                }
                else uye.Email = model.Email;
                uye.Sifre = model.Sifre;
                uye.UyeAdi = model.UyeAdi;
                uye.UyeSoyadi = model.UyeSoyadi;
                uye.Telefon = model.Telefon;
            }

            uye.YetkiID = 2;
            db.Uye.Add(uye);
            db.SaveChanges();

            //İşlemimiz başarıyla biterse, başarılı olduğuna dair bir sayfaya yönlendiriyoruz.
            return Redirect("~/Uye/Login");

        }
        public ActionResult Logout()
        {
            Session["UyeID"] = null;
            Session.Abandon();
            return RedirectToAction("Login", "Uye");
        }
    }
}