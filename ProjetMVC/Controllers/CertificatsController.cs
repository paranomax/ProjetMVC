using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProjetMVC.Models;

namespace ProjetMVC.Controllers
{
    public class CertificatsController : Controller
    {
        private StoreContext db = new StoreContext();

        // GET: Certificats
        public ActionResult Index()
        {
            var Certificatss = db.Certificat;
            return View(Certificatss.ToList());
        }

        // GET: Certificats/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Certificat certificat = db.Certificat.Find(id);
            if (certificat == null)
            {
                return HttpNotFound();
            }
            return View(certificat);
        }

        // GET: Certificats/Create
        public ActionResult Create()
        {
            ViewBag.WeaponID = new SelectList(db.Weapon, "WeaponID", "WeaponID");
            ViewBag.UserID = new SelectList(db.User, "UserID", "UserID");
            return View();
        }

        // POST: Certificats/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CertificatID, UserID, WeaponID, DateBegin, DateEnd")] Certificat certificat)
        {
            if (ModelState.IsValid)
            {
                db.Certificat.Add(certificat);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.WeaponID = new SelectList(db.Weapon, "WeaponID", "WeaponID", certificat.WeaponID);
            ViewBag.UserID = new SelectList(db.User, "UserID", "UserID", certificat.UserID);
            return View(certificat);
        }

        // GET: Certificats/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Certificat certificat = db.Certificat.Find(id);
            if (certificat == null)
            {
                return HttpNotFound();
            }
            ViewBag.WeaponID = new SelectList(db.Weapon, "WeaponID", "WeaponID", certificat.WeaponID);
            ViewBag.UserID = new SelectList(db.User, "UserID", "UserID", certificat.UserID);
            return View(certificat);
        }

        // POST: Certificats/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CertificatID, WeaponID, UserID, DateBegin, DateEnd")] Certificat certificat)
        {
            if (ModelState.IsValid)
            {
                db.Entry(certificat).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.WeaponID = new SelectList(db.Weapon, "WeaponID", "WeaponID", certificat.WeaponID);
            ViewBag.UserID = new SelectList(db.User, "UserID", "UserID", certificat.UserID);
            return View(certificat);
        }

        // GET: Certificats/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Certificat certificat = db.Certificat.Find(id);
            if (certificat == null)
            {
                return HttpNotFound();
            }
            return View(certificat);
        }

        // POST: Certificats/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Certificat certificat = db.Certificat.Find(id);
            db.Certificat.Remove(certificat);
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
