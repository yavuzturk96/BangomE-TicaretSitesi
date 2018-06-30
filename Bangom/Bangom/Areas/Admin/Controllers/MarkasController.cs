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
    public class MarkasController : Controller
    {
        private BangomEntities db = new BangomEntities();

        // GET: Admin/Markas
        public ActionResult Index()
        {
            return View(db.Marka.ToList());
        }

        // GET: Admin/Markas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Marka marka = db.Marka.Find(id);
            if (marka == null)
            {
                return HttpNotFound();
            }
            return View(marka);
        }

        // GET: Admin/Markas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Markas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MarkaID,MarkaAdi,MarkaResmi")] Marka marka)
        {
            if (ModelState.IsValid)
            {
                db.Marka.Add(marka);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(marka);
        }

        // GET: Admin/Markas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Marka marka = db.Marka.Find(id);
            if (marka == null)
            {
                return HttpNotFound();
            }
            return View(marka);
        }

        // POST: Admin/Markas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MarkaID,MarkaAdi,MarkaResmi")] Marka marka)
        {
            if (ModelState.IsValid)
            {
                db.Entry(marka).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(marka);
        }

        // GET: Admin/Markas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Marka marka = db.Marka.Find(id);
            if (marka == null)
            {
                return HttpNotFound();
            }
            return View(marka);
        }

        // POST: Admin/Markas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Marka marka = db.Marka.Find(id);
            db.Marka.Remove(marka);
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
