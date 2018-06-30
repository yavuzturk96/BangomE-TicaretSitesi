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
    public class UrunResimsController : Controller
    {
        private BangomEntities db = new BangomEntities();

        // GET: Admin/UrunResims
        public ActionResult Index()
        {
            var urunResim = db.UrunResim.Include(u => u.Urun);
            return View(urunResim.ToList());
        }

        // GET: Admin/UrunResims/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UrunResim urunResim = db.UrunResim.Find(id);
            if (urunResim == null)
            {
                return HttpNotFound();
            }
            return View(urunResim);
        }

        // GET: Admin/UrunResims/Create
        public ActionResult Create()
        {
            ViewBag.UrunID = new SelectList(db.Urun, "UrunID", "UrunAdi");
            return View();
        }

        // POST: Admin/UrunResims/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UrunResimID,UrunID,UrunResmi")] UrunResim urunResim)
        {
            HttpPostedFileBase file = Request.Files["ImageUpload"];
            if (file != null && file.FileName != null && file.FileName != "")
            {
                FileInfo fi = new FileInfo(file.FileName);
                if (fi.Extension != ".jpeg" && fi.Extension != ".jpg" && fi.Extension != ".JPG" && fi.Extension != ".JPEG")
                {
                    TempData["Errormsg"] = "Image File Extension is Not valid";
                    return View(urunResim);
                }
                else
                {
                    string pic = System.IO.Path.GetFileName(file.FileName);
                    string path = System.IO.Path.Combine(Server.MapPath("~/images"), pic);
                    file.SaveAs(path);
                    byte[] img = null;
                    FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
                    BinaryReader br = new BinaryReader(fs);
                    img = br.ReadBytes((int)fs.Length);   //Byte değeri img ye atadı.
                    urunResim.UrunResmi = img;  //img nin içinde bulunan binary değeri veritabanına kaydediyor.
                }
            }
            if (ModelState.IsValid)
            {
                db.UrunResim.Add(urunResim);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UrunID = new SelectList(db.Urun, "UrunID", "UrunAdi", urunResim.UrunID);
            return View(urunResim);
        }

        // GET: Admin/UrunResims/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UrunResim urunResim = db.UrunResim.Find(id);
            if (urunResim == null)
            {
                return HttpNotFound();
            }
            ViewBag.UrunID = new SelectList(db.Urun, "UrunID", "UrunAdi", urunResim.UrunID);
            return View(urunResim);
        }

        // POST: Admin/UrunResims/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UrunResimID,UrunID,UrunResmi")] UrunResim urunResim)
        {
            if (ModelState.IsValid)
            {
                db.Entry(urunResim).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UrunID = new SelectList(db.Urun, "UrunID", "UrunAdi", urunResim.UrunID);
            return View(urunResim);
        }

        // GET: Admin/UrunResims/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UrunResim urunResim = db.UrunResim.Find(id);
            if (urunResim == null)
            {
                return HttpNotFound();
            }
            return View(urunResim);
        }

        // POST: Admin/UrunResims/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UrunResim urunResim = db.UrunResim.Find(id);
            db.UrunResim.Remove(urunResim);
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
