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
    public class UyesController : Controller
    {
        private BangomEntities db = new BangomEntities();

        // GET: Admin/Uyes
        public ActionResult Index()
        {
            var uye = db.Uye.Include(u => u.Yetki);
            return View(uye.ToList());
        }

        // GET: Admin/Uyes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Uye uye = db.Uye.Find(id);
            if (uye == null)
            {
                return HttpNotFound();
            }
            return View(uye);
        }

        // GET: Admin/Uyes/Create
        public ActionResult Create()
        {
            ViewBag.YetkiID = new SelectList(db.Yetki, "YetkiID", "YetkiAdi");
            return View();
        }

        // POST: Admin/Uyes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UyeID,YetkiID,UyeAdi,UyeSoyadi,UyeResmi,Email,Sifre,Telefon")] Uye uye)
        {
            if (ModelState.IsValid)
            {
                db.Uye.Add(uye);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.YetkiID = new SelectList(db.Yetki, "YetkiID", "YetkiAdi", uye.YetkiID);
            return View(uye);
        }

        // GET: Admin/Uyes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Uye uye = db.Uye.Find(id);
            if (uye == null)
            {
                return HttpNotFound();
            }
            ViewBag.YetkiID = new SelectList(db.Yetki, "YetkiID", "YetkiAdi", uye.YetkiID);
            return View(uye);
        }

        // POST: Admin/Uyes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UyeID,YetkiID,UyeAdi,UyeSoyadi,UyeResmi,Email,Sifre,Telefon")] Uye uye)
        {
            if (ModelState.IsValid)
            {
                db.Entry(uye).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.YetkiID = new SelectList(db.Yetki, "YetkiID", "YetkiAdi", uye.YetkiID);
            return View(uye);
        }

        // GET: Admin/Uyes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Uye uye = db.Uye.Find(id);
            if (uye == null)
            {
                return HttpNotFound();
            }
            return View(uye);
        }

        // POST: Admin/Uyes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Uye uye = db.Uye.Find(id);
            db.Uye.Remove(uye);
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
