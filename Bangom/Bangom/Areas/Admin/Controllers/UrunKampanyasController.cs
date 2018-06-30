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
    public class UrunKampanyasController : Controller
    {
        private BangomEntities db = new BangomEntities();

        // GET: Admin/UrunKampanyas
        public ActionResult Index()
        {
            var urunKampanya = db.UrunKampanya.Include(u => u.Urun);
            return View(urunKampanya.ToList());
        }

        // GET: Admin/UrunKampanyas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UrunKampanya urunKampanya = db.UrunKampanya.Find(id);
            if (urunKampanya == null)
            {
                return HttpNotFound();
            }
            return View(urunKampanya);
        }

        // GET: Admin/UrunKampanyas/Create
        public ActionResult Create()
        {
            ViewBag.UrunID = new SelectList(db.Urun, "UrunID", "UrunAdi");
            return View();
        }

        // POST: Admin/UrunKampanyas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UrunKampanyaID,UrunID,UrunKampanyaAdi,YeniFiyat,UrunKampanyaBaslangic,UrunKampanyaBitis")] UrunKampanya urunKampanya)
        {
            if (ModelState.IsValid)
            {
                db.UrunKampanya.Add(urunKampanya);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UrunID = new SelectList(db.Urun, "UrunID", "UrunAdi", urunKampanya.UrunID);
            return View(urunKampanya);
        }

        // GET: Admin/UrunKampanyas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UrunKampanya urunKampanya = db.UrunKampanya.Find(id);
            if (urunKampanya == null)
            {
                return HttpNotFound();
            }
            ViewBag.UrunID = new SelectList(db.Urun, "UrunID", "UrunAdi", urunKampanya.UrunID);
            return View(urunKampanya);
        }

        // POST: Admin/UrunKampanyas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UrunKampanyaID,UrunID,UrunKampanyaAdi,YeniFiyat,UrunKampanyaBaslangic,UrunKampanyaBitis")] UrunKampanya urunKampanya)
        {
            if (ModelState.IsValid)
            {
                db.Entry(urunKampanya).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UrunID = new SelectList(db.Urun, "UrunID", "UrunAdi", urunKampanya.UrunID);
            return View(urunKampanya);
        }

        // GET: Admin/UrunKampanyas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UrunKampanya urunKampanya = db.UrunKampanya.Find(id);
            if (urunKampanya == null)
            {
                return HttpNotFound();
            }
            return View(urunKampanya);
        }

        // POST: Admin/UrunKampanyas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UrunKampanya urunKampanya = db.UrunKampanya.Find(id);
            db.UrunKampanya.Remove(urunKampanya);
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
