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
    public class HomeController : Controller
    {
        StoreContext db = new StoreContext();
        // GET: Home
        public ActionResult Index()
        {
            var certificat = db.Certificat.ToList();
            var ListeCertificats = db.Certificat.ToList().Where(c => c.DateEnd > DateTime.Today);
            var certificatsvalide = from c in db.Certificat where c.DateEnd > DateTime.Today select c;
          

            return View(ListeCertificats);
        }

        // GET: Certificats/Create
        public ActionResult Location()
        {
            ViewBag.WeaponID = new SelectList(db.Weapon, "WeaponID", "WeaponID");
            ViewBag.UserID = new SelectList(db.User, "UserID", "UserID");
            return View();
        }

        // POST: Certificats/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Location([Bind(Include = "CertificatID, UserID, WeaponID, DateBegin, DateEnd")] Certificat certificat)
        {
            certificat.DateBegin = DateTime.Today;
            certificat.DateEnd = DateTime.Today.AddMonths(6);
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

        public ActionResult Dictionnaire1(string LastName)
        {
            Dictionary<int, string> myDictionnary = new Dictionary<int, string>()
            {
                {1, "one" },
                {2, "two" },
                {3, "Three" }
            };

            Dictionary<string, User> UserDictionnary = new Dictionary<string, User>();
            var listUser = db.User.ToList();
            foreach(User u in listUser)
            {
                UserDictionnary.Add(u.LastName, u);
            }

            User userchoisi = UserDictionnary[LastName];
            if (userchoisi == null)
            {
                return HttpNotFound();
            }

            return View(userchoisi);
        }
    }
}