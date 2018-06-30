using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Bangom.Models;

namespace Bangom.Areas.Admin.Controllers
{
    public class SiparisUrunsController : Controller
    {
        private BangomEntities db = new BangomEntities();

        // GET: Admin/SiparisUruns
        public ActionResult Index()
        {
            var siparisUrun = db.SiparisUrun.Include(s => s.Siparis).Include(s => s.Urun).Include(s => s.Uye);
            return View(siparisUrun.ToList());
        }

        // GET: Admin/SiparisUruns/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SiparisUrun siparisUrun = db.SiparisUrun.Find(id);
            if (siparisUrun == null)
            {
                return HttpNotFound();
            }
            return View(siparisUrun);
        }

        // GET: Admin/SiparisUruns/Create
        public ActionResult Create()
        {
            ViewBag.SiparisID = new SelectList(db.Siparis, "SiparisID", "Durum");
            ViewBag.UrunID = new SelectList(db.Urun, "UrunID", "UrunAdi");
            ViewBag.UyeID = new SelectList(db.Uye, "UyeID", "UyeAdi");
            return View();
        }

        // POST: Admin/SiparisUruns/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SiparisUrunID,UrunID,SiparisID,UrunAdi,UrunFiyati,UyeID")] SiparisUrun siparisUrun)
        {
            if (ModelState.IsValid)
            {
                db.SiparisUrun.Add(siparisUrun);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SiparisID = new SelectList(db.Siparis, "SiparisID", "Durum", siparisUrun.SiparisID);
            ViewBag.UrunID = new SelectList(db.Urun, "UrunID", "UrunAdi", siparisUrun.UrunID);
            ViewBag.UyeID = new SelectList(db.Uye, "UyeID", "UyeAdi", siparisUrun.UyeID);
            return View(siparisUrun);
        }

        // GET: Admin/SiparisUruns/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SiparisUrun siparisUrun = db.SiparisUrun.Find(id);
            if (siparisUrun == null)
            {
                return HttpNotFound();
            }
            ViewBag.SiparisID = new SelectList(db.Siparis, "SiparisID", "Durum", siparisUrun.SiparisID);
            ViewBag.UrunID = new SelectList(db.Urun, "UrunID", "UrunAdi", siparisUrun.UrunID);
            ViewBag.UyeID = new SelectList(db.Uye, "UyeID", "UyeAdi", siparisUrun.UyeID);
            return View(siparisUrun);
        }

        // POST: Admin/SiparisUruns/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SiparisUrunID,UrunID,SiparisID,UrunAdi,UrunFiyati,UyeID")] SiparisUrun siparisUrun)
        {
            if (ModelState.IsValid)
            {
                db.Entry(siparisUrun).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SiparisID = new SelectList(db.Siparis, "SiparisID", "Durum", siparisUrun.SiparisID);
            ViewBag.UrunID = new SelectList(db.Urun, "UrunID", "UrunAdi", siparisUrun.UrunID);
            ViewBag.UyeID = new SelectList(db.Uye, "UyeID", "UyeAdi", siparisUrun.UyeID);
            return View(siparisUrun);
        }

        // GET: Admin/SiparisUruns/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SiparisUrun siparisUrun = db.SiparisUrun.Find(id);
            if (siparisUrun == null)
            {
                return HttpNotFound();
            }
            return View(siparisUrun);
        }

        // POST: Admin/SiparisUruns/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SiparisUrun siparisUrun = db.SiparisUrun.Find(id);
            db.SiparisUrun.Remove(siparisUrun);
            db.SaveChanges();
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
