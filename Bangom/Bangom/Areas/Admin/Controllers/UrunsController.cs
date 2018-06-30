using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Bangom.Models;

namespace Bangom.Areas.Admin.Controllers
{
    public class UrunsController : Controller
    {
        private BangomEntities db = new BangomEntities();

        // GET: Admin/Uruns
        public ActionResult Index()
        {
            var urun = db.Urun.Include(u => u.Kategori).Include(u => u.Marka);
            return View(urun.ToList());
        }

        // GET: Admin/Uruns/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Urun urun = db.Urun.Find(id);
            if (urun == null)
            {
                return HttpNotFound();
            }
            return View(urun);
        }

        // GET: Admin/Uruns/Create
        public ActionResult Create()
        {
            
            ViewBag.KategoriID = new SelectList(db.Kategori, "KategoriID", "KategoriAdi");
            ViewBag.MarkaID = new SelectList(db.Marka, "MarkaID", "MarkaAdi");
            return View();
        }

        // POST: Admin/Uruns/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UrunID,UrunAdi,UrunDetay,UrunFiyat,Durum,KategoriID,MarkaID,UrunFoto")] Urun urun,HttpPostedFileBase file)
        {
            //[Bind(Include = "UrunID,UrunAdi,UrunDetay,UrunFiyat,Durum,KategoriID,MarkaID,UrunFoto")]
            if (ModelState.IsValid)
            {
                //HttpPostedFileBase file = Request.Files["file"];
                if (file != null && file.FileName != null && file.FileName != "")
                {
                    FileInfo fi = new FileInfo(file.FileName);
                        string pic = System.IO.Path.GetFileName(file.FileName);
                        string path = System.IO.Path.Combine(Server.MapPath("~/images/urunler"), pic);
                        file.SaveAs(path);
                        byte[] img = null;
                        FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
                        BinaryReader br = new BinaryReader(fs);
                        img = br.ReadBytes((int)fs.Length);   //Byte değeri img ye atadı.
                        urun.UrunFoto = img;  //img nin içinde bulunan binary değeri veritabanına kaydediyor.
                    
                }
                db.Urun.Add(urun);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            
            //ViewBag.KategoriID = new SelectList(db.Kategori, "KategoriID", "KategoriAdi", urun.KategoriID);
            //ViewBag.MarkaID = new SelectList(db.Marka, "MarkaID", "MarkaAdi", urun.MarkaID);
            return View(urun);
        }

        // GET: Admin/Uruns/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Urun urun = db.Urun.Find(id);
            if (urun == null)
            {
                return HttpNotFound();
            }
            ViewBag.KategoriID = new SelectList(db.Kategori, "KategoriID", "KategoriAdi", urun.KategoriID);
            ViewBag.MarkaID = new SelectList(db.Marka, "MarkaID", "MarkaAdi", urun.MarkaID);
            return View(urun);
        }

        // POST: Admin/Uruns/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UrunID,UrunAdi,UrunDetay,UrunFiyat,Durum,KategoriID,MarkaID")] Urun urun)
        {
            if (ModelState.IsValid)
            {
                db.Entry(urun).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.KategoriID = new SelectList(db.Kategori, "KategoriID", "KategoriAdi", urun.KategoriID);
            ViewBag.MarkaID = new SelectList(db.Marka, "MarkaID", "MarkaAdi", urun.MarkaID);
            return View(urun);
        }

        // GET: Admin/Uruns/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Urun urun = db.Urun.Find(id);
            if (urun == null)
            {
                return HttpNotFound();
            }
            return View(urun);
        }

        // POST: Admin/Uruns/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Urun urun = db.Urun.Find(id);

            db.Urun.Remove(urun);

            var d =
                    from details in db.UrunResim
                    where details.UrunID == id
                    select details;

            foreach (var detail in d)
            {
                db.UrunResim.Remove(detail);
            }

            db.SaveChanges();
            //Urun urun = db.Urun.Find(id);
            //db.Urun.Remove(urun);
            //db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
