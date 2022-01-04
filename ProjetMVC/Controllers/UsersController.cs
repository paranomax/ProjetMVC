﻿using System;
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
    public class UsersController : Controller
    {
        private StoreContext db = new StoreContext();
        // GET: Users
        public ActionResult Index()
        {
            var user = db.User;
            return View(user.ToList());
        }

        //GET: OrderbyName
        public ActionResult OrderbyName()
        {
            var user = from u in db.User orderby u.Name ascending select u;
            return View(user);
        }

        public ActionResult Details(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.User.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        [Route("InsriptionUser")]
        public ActionResult Create()
        {
            //ViewBag.UserID = new SelectList(db.User, "UserID", "Name", "LastName", "Birthday", "Address");
            //ViewBag. = new SelectList(db.Users, "CategoryID", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("InsriptionUser")]
        public ActionResult Create([Bind(Include = "UserID, Name, LastName, Birthday, Address")] User user)
        {
            if (ModelState.IsValid)
            {
                db.User.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            //ViewBag.User = new SelectList(db.User, "CategoryID", "Name", user.UserID);
            return View(user);
        }

        [Route("ModificationUser")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.User.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            //ViewBag.User = new SelectList(db.User, "UserID", "Name", "LastName", "Birthday", "Address");
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("ModificationUser")]
        public ActionResult Edit([Bind(Include = "UserID, Name, LastName, Birthday, Address")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
           // ViewBag.User = new SelectList(db.User, "CategoryID", "Name", user.UserID);
            return View(user);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.User.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.User.Find(id);
            db.User.Remove(user);
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