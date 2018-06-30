using Bangom.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Bangom.Controllers
{
    public class HomeController : Controller
    {
        private BangomEntities db = new BangomEntities();
        public ActionResult Index()
        {
            return View();          
        }
        public ActionResult Siparisler()
        {
            var uyeID = Convert.ToInt32(Session["UyeID"]);
            var res = db.Siparis.Where(m => m.UyeID == uyeID).ToList();
            if (res == null)
            {
                return HttpNotFound();
            }
            return View(res);
            
            //var model = db.SiparisUrun.ToList();
            
            //return View(db.Siparis.ToList());
        }
        public ActionResult Sepet()
        {
            return View(db.SiparisUrun.ToList());
        }
        public ActionResult Urunler()
        {
            ViewBag.KategoriID = new SelectList(db.Kategori, "KategoriID", "KategoriAdi");
            return View(db.Urun.ToList());
        }
        public ActionResult Urun(int id)
        {
           
            var res = db.Urun.Where(m => m.UrunID == id).SingleOrDefault();
            if (res == null)
            {
                return HttpNotFound();
            }
            return View(res);

        }
        public ActionResult Hakkımızda()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Iletisim()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public JsonResult SepeteEkle(int urunID, int UyeID,string urunAdi,string fiyat)
        {
            var urunFiyati = Convert.ToDecimal(fiyat);
            var uyeid = Session["UyeId"];
            if (urunID == null)
            {
                return Json(true, JsonRequestBehavior.AllowGet);

            }
            db.SiparisUrun.Add(new SiparisUrun { UrunID = urunID, SiparisID = null, UrunAdi = urunAdi, UrunFiyati = urunFiyati, UyeID=UyeID });
            db.SaveChanges();
            return Json(false, JsonRequestBehavior.AllowGet);
        }
        public JsonResult SepettenKaldır(int siparisUrunID)
        {
           
            if (siparisUrunID == null)
            {
                return Json(true, JsonRequestBehavior.AllowGet);

            }
            SiparisUrun siparisUrun = db.SiparisUrun.Find(siparisUrunID);
            db.SiparisUrun.Remove(siparisUrun);
            db.SaveChanges();
            return Json(false, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Sepettekiler()
        {
            var uyeID = Convert.ToInt32(Session["UyeID"]);
            //var model = db.SiparisUrun.ToList();
            var model = db.SiparisUrun.Where(m => m.UyeID == uyeID).ToList();
            int siparistekiurunSayısı = model.Count();
            int urunSayısı = siparistekiurunSayısı;
            return Json(
            new
            {
                Result = (from obj in model
                          select new
                          {
                              SiparisUrunID = obj.SiparisUrunID,
                              UrunID = obj.UrunID,
                              SiparisID = obj.SiparisID,
                              UrunAdi = obj.UrunAdi,
                              UrunFiyati = obj.UrunFiyati,
                              UyeID = obj.UyeID,
                              urunSayısı = siparistekiurunSayısı
                          }),
                         
            }, JsonRequestBehavior.AllowGet);
        }

        
        [HttpPost]
        public JsonResult Sil(int[] id)
        {
            SiparisUrun model;
            foreach (var veri in id)
            {
                model = db.SiparisUrun.FirstOrDefault(x => x.SiparisUrunID == veri);
                db.SiparisUrun.Remove(model);
            }
            db.SaveChanges();
            return Json("1");
        }

        [HttpPost]
        public JsonResult SiparisEt(int[] id,int sayac)
        {
            var uyeID = Convert.ToInt32(Session["UyeID"]);
            SiparisUrun siparisUrunModel;
            int urunSayac = 0;
            int[] urunID = new int[sayac];
            int tutar = 0;


            foreach (var veri in id)
            {
                siparisUrunModel = db.SiparisUrun.FirstOrDefault(x => x.SiparisUrunID == veri);
                urunID[urunSayac] = Convert.ToInt32(siparisUrunModel.UrunID);
                tutar = tutar+Convert.ToInt32(siparisUrunModel.UrunFiyati);
                urunSayac++;
            }
            db.Siparis.Add(new Siparis { UyeID = uyeID, SiparisTutar = tutar, SiparisTarihi = DateTime.Now, Durum = "Onaylanmadı", BankaID = 1 });
            db.SaveChanges();
            return Json("1");
            
        }
    }
}