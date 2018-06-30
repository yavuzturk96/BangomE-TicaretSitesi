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
    public class YetkisController : Controller
    {
        private BangomEntities db = new BangomEntities();

        // GET: Admin/Yetkis
        public ActionResult Index()
        {
            return View(db.Yetki.ToList());
        }

        // GET: Admin/Yetkis/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Yetki yetki = db.Yetki.Find(id);
            if (yetki == null)
            {
                return HttpNotFound();
            }
            return View(yetki);
        }

        // GET: Admin/Yetkis/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Yetkis/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "YetkiID,YetkiAdi")] Yetki yetki)
        {
            if (ModelState.IsValid)
            {
                db.Yetki.Add(yetki);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(yetki);
        }

        // GET: Admin/Yetkis/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Yetki yetki = db.Yetki.Find(id);
            if (yetki == null)
            {
                return HttpNotFound();
            }
            return View(yetki);
        }

        // POST: Admin/Yetkis/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "YetkiID,YetkiAdi")] Yetki yetki)
        {
            if (ModelState.IsValid)
            {
                db.Entry(yetki).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(yetki);
        }

        // GET: Admin/Yetkis/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Yetki yetki = db.Yetki.Find(id);
            if (yetki == null)
            {
                return HttpNotFound();
            }
            return View(yetki);
        }

        // POST: Admin/Yetkis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Yetki yetki = db.Yetki.Find(id);
            db.Yetki.Remove(yetki);
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
