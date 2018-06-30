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
    public class SiparisController : Controller
    {
        private BangomEntities db = new BangomEntities();

        // GET: Admin/Siparis
        public ActionResult Index()
        {
            var siparis = db.Siparis.Include(s => s.Banka).Include(s => s.Kargo).Include(s => s.Uye);
            return View(siparis.ToList());
        }

        // GET: Admin/Siparis/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Siparis siparis = db.Siparis.Find(id);
            if (siparis == null)
            {
                return HttpNotFound();
            }
            return View(siparis);
        }

        // GET: Admin/Siparis/Create
        public ActionResult Create()
        {
            ViewBag.BankaID = new SelectList(db.Banka, "BankaID", "BankaAdi");
            ViewBag.KargoID = new SelectList(db.Kargo, "KargoID", "KargoAdi");
            ViewBag.UyeID = new SelectList(db.Uye, "UyeID", "UyeAdi");
            return View();
        }

        // POST: Admin/Siparis/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SiparisID,UyeID,SiparisTutar,Durum,SiparisTarihi,BankaID,KargoID")] Siparis siparis)
        {
            if (ModelState.IsValid)
            {
                db.Siparis.Add(siparis);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BankaID = new SelectList(db.Banka, "BankaID", "BankaAdi", siparis.BankaID);
            ViewBag.KargoID = new SelectList(db.Kargo, "KargoID", "KargoAdi", siparis.KargoID);
            ViewBag.UyeID = new SelectList(db.Uye, "UyeID", "UyeAdi", siparis.UyeID);
            return View(siparis);
        }

        // GET: Admin/Siparis/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Siparis siparis = db.Siparis.Find(id);
            if (siparis == null)
            {
                return HttpNotFound();
            }
            ViewBag.BankaID = new SelectList(db.Banka, "BankaID", "BankaAdi", siparis.BankaID);
            ViewBag.KargoID = new SelectList(db.Kargo, "KargoID", "KargoAdi", siparis.KargoID);
            ViewBag.UyeID = new SelectList(db.Uye, "UyeID", "UyeAdi", siparis.UyeID);
            return View(siparis);
        }

        // POST: Admin/Siparis/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SiparisID,UyeID,SiparisTutar,Durum,SiparisTarihi,BankaID,KargoID")] Siparis siparis)
        {
            if (ModelState.IsValid)
            {
                db.Entry(siparis).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BankaID = new SelectList(db.Banka, "BankaID", "BankaAdi", siparis.BankaID);
            ViewBag.KargoID = new SelectList(db.Kargo, "KargoID", "KargoAdi", siparis.KargoID);
            ViewBag.UyeID = new SelectList(db.Uye, "UyeID", "UyeAdi", siparis.UyeID);
            return View(siparis);
        }

        // GET: Admin/Siparis/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Siparis siparis = db.Siparis.Find(id);
            if (siparis == null)
            {
                return HttpNotFound();
            }
            return View(siparis);
        }

        // POST: Admin/Siparis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Siparis siparis = db.Siparis.Find(id);
            db.Siparis.Remove(siparis);
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
