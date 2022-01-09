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
    public class AmmoesController : Controller
    {
        private StoreContext db = new StoreContext();
        // GET: Ammoes
        public ActionResult Index()
        {
            var ammo = db.Ammo;
            return View(ammo.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ammo ammo = db.Ammo.Find(id);
            if (ammo == null)
            {
                return HttpNotFound();
            }
            return View(ammo);
        }
        public ActionResult Create()
        {
            //ViewBag.UserID = new SelectList(db.User, "AmmoID", "Type", "Caliber");
            //ViewBag. = new SelectList(db.Users, "CategoryID", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Create([Bind(Include = "AmmoID, Type, Caliber")] Ammo ammo)
        {
            if (ModelState.IsValid)
            {
                db.Ammo.Add(ammo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            //ViewBag.CategoryID = new SelectList(db.User, "AmmoID", "Type", "Caliber");
            return View(ammo);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ammo ammo = db.Ammo.Find(id);
            if (ammo == null)
            {
                return HttpNotFound();
            }
            //ViewBag.User = new SelectList(db.Ammo, "AmmoID", "Type", "Caliber");
            return View(ammo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AmmoID, Type, Caliber")] Ammo ammo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ammo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            //ViewBag.Ammo = new SelectList(db.Ammo, "AmmoID", "Type", "Caliber");
            return View(ammo);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ammo ammo = db.Ammo.Find(id);
            if (ammo == null)
            {
                return HttpNotFound();
            }
            return View(ammo);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ammo ammo = db.Ammo.Find(id);
            db.Ammo.Remove(ammo);
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

        public ActionResult Recherche(string MotClef)
        {
            
            if (MotClef == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var ListeAmmo = db.Ammo.ToList().Where(c => c.Type == MotClef);
            if (ListeAmmo.Count() > 0)
                return View(ListeAmmo);

            int i = Int32.Parse(MotClef);

            ListeAmmo = db.Ammo.ToList().Where(c => c.AmmoID == i);
            if (ListeAmmo.Count() > 0)
                return View(ListeAmmo);

            ListeAmmo = db.Ammo.ToList().Where(c => c.Caliber == i);
            if (ListeAmmo.Count() > 0)
                return View(ListeAmmo);

            if (ListeAmmo.Count() > 0)
            {
                return HttpNotFound();
            }

            return View();
        }

    }
}