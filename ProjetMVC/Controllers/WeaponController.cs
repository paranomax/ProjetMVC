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
    public class WeaponController : Controller
    {
        private StoreContext db = new StoreContext();

        // GET: Weapon
        public ActionResult Index()
        {
            var weapons = db.Weapon;
            return View(weapons.ToList());
        }

        // GET: Weapon/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Weapon weapon = db.Weapon.Find(id);
            if (weapon == null)
            {
                return HttpNotFound();
            }
            return View(weapon);
        }

        // GET: Weapon/Create
        public ActionResult Create()
        {
            ViewBag.AmmoID = new SelectList(db.Ammo, "AmmoID","Type", "Caliber" );
            return View();
        }

        // POST: Weapon/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "WeaponID, WeaponModel, AmmoID")] Weapon weapon)
        {
            if (ModelState.IsValid)
            {
                db.Weapon.Add(weapon);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AmmoID = new SelectList(db.Ammo, "AmmoID", "Type", "Caliber");
            return View(weapon);
        }

        // GET: Weapon/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Weapon weapon = db.Weapon.Find(id);
            if (weapon == null)
            {
                return HttpNotFound();
            }
            ViewBag.AmmoID = new SelectList(db.Ammo, "AmmoID", "Type", "Caliber");
            return View(weapon);
        }

        // POST: Weapon/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "WeaponID, WeaponModel, AmmoID")] Weapon weapon)
        {
            if (ModelState.IsValid)
            {
                db.Entry(weapon).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AmmoID = new SelectList(db.Ammo, "AmmoID", "Type", "Caliber", weapon.Ammo.AmmoID);
            return View(weapon);
        }

        // GET: Weapon/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Weapon weapon = db.Weapon.Find(id);
            if (weapon == null)
            {
                return HttpNotFound();
            }
            return View(weapon);
        }

        // POST: Weapon/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Weapon weapon = db.Weapon.Find(id);
            db.Weapon.Remove(weapon);
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
