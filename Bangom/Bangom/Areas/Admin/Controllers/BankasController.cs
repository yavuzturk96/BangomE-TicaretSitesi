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
    public class BankasController : Controller
    {
        private BangomEntities db = new BangomEntities();

        // GET: Admin/Bankas
        public ActionResult Index()
        {
            return View(db.Banka.ToList());
        }

        // GET: Admin/Bankas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Banka banka = db.Banka.Find(id);
            if (banka == null)
            {
                return HttpNotFound();
            }
            return View(banka);
        }

        // GET: Admin/Bankas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Bankas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BankaID,BankaAdi,BankaResmi")] Banka banka)
        {
            if (ModelState.IsValid)
            {
                db.Banka.Add(banka);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(banka);
        }

        // GET: Admin/Bankas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Banka banka = db.Banka.Find(id);
            if (banka == null)
            {
                return HttpNotFound();
            }
            return View(banka);
        }

        // POST: Admin/Bankas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BankaID,BankaAdi,BankaResmi")] Banka banka)
        {
            if (ModelState.IsValid)
            {
                db.Entry(banka).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(banka);
        }

        // GET: Admin/Bankas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Banka banka = db.Banka.Find(id);
            if (banka == null)
            {
                return HttpNotFound();
            }
            return View(banka);
        }

        // POST: Admin/Bankas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Banka banka = db.Banka.Find(id);
            db.Banka.Remove(banka);
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
