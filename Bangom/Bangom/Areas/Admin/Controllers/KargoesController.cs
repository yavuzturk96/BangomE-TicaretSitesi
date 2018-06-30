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
    public class KargoesController : Controller
    {
        private BangomEntities db = new BangomEntities();

        // GET: Admin/Kargoes
        public ActionResult Index()
        {
            return View(db.Kargo.ToList());
        }

        // GET: Admin/Kargoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kargo kargo = db.Kargo.Find(id);
            if (kargo == null)
            {
                return HttpNotFound();
            }
            return View(kargo);
        }

        // GET: Admin/Kargoes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Kargoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "KargoID,KargoAdi,KargoResmi")] Kargo kargo)
        {
            if (ModelState.IsValid)
            {
                db.Kargo.Add(kargo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(kargo);
        }

        // GET: Admin/Kargoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kargo kargo = db.Kargo.Find(id);
            if (kargo == null)
            {
                return HttpNotFound();
            }
            return View(kargo);
        }

        // POST: Admin/Kargoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "KargoID,KargoAdi,KargoResmi")] Kargo kargo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(kargo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(kargo);
        }

        // GET: Admin/Kargoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kargo kargo = db.Kargo.Find(id);
            if (kargo == null)
            {
                return HttpNotFound();
            }
            return View(kargo);
        }

        // POST: Admin/Kargoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Kargo kargo = db.Kargo.Find(id);
            db.Kargo.Remove(kargo);
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
