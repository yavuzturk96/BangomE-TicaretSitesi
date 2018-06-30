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
    public class StoksController : Controller
    {
        private BangomEntities db = new BangomEntities();

        // GET: Admin/Stoks
        public ActionResult Index()
        {
            var stok = db.Stok.Include(s => s.Urun);
            return View(stok.ToList());
        }

        // GET: Admin/Stoks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stok stok = db.Stok.Find(id);
            if (stok == null)
            {
                return HttpNotFound();
            }
            return View(stok);
        }

        // GET: Admin/Stoks/Create
        public ActionResult Create()
        {
            ViewBag.UrunID = new SelectList(db.Urun, "UrunID", "UrunAdi");
            return View();
        }

        // POST: Admin/Stoks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StokID,UrunID,Adet")] Stok stok)
        {
            if (ModelState.IsValid)
            {
                db.Stok.Add(stok);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UrunID = new SelectList(db.Urun, "UrunID", "UrunAdi", stok.UrunID);
            return View(stok);
        }

        // GET: Admin/Stoks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stok stok = db.Stok.Find(id);
            if (stok == null)
            {
                return HttpNotFound();
            }
            ViewBag.UrunID = new SelectList(db.Urun, "UrunID", "UrunAdi", stok.UrunID);
            return View(stok);
        }

        // POST: Admin/Stoks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StokID,UrunID,Adet")] Stok stok)
        {
            if (ModelState.IsValid)
            {
                db.Entry(stok).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UrunID = new SelectList(db.Urun, "UrunID", "UrunAdi", stok.UrunID);
            return View(stok);
        }

        // GET: Admin/Stoks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stok stok = db.Stok.Find(id);
            if (stok == null)
            {
                return HttpNotFound();
            }
            return View(stok);
        }

        // POST: Admin/Stoks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Stok stok = db.Stok.Find(id);
            db.Stok.Remove(stok);
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
