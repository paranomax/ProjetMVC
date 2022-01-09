using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProjetMVC.Models;
using ProjetMVC.ViewModels;

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

        //view avec requete linq pour afficher toutes les armes aux nom de cette utilisateur.
        public ActionResult Arme(int? id)
        {
            UsersAmeViewModel userarmes = new UsersAmeViewModel()
            {
                user = new User(),
                weapon = new List<Weapon> { }
            };
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            userarmes.user = db.User.Find(id);
            if (userarmes.user == null)
            {
                return HttpNotFound();
            }
            var certificats = from c in db.Certificat where c.UserID == userarmes.user.UserID select c;
            foreach(Certificat c in certificats)
            {
                var weapon = db.Weapon.Find(c.WeaponID);
                userarmes.weapon.Add(weapon);
            }
            return View(userarmes);
            //var user = from u in db.User orderby u.Name ascending select u;
        }

        public ActionResult Dictionnaire1(string LastName)
        {
            User userchoisi = new User();
            /*Dictionary<int, string> myDictionnary = new Dictionary<int, string>()
            {
                {1, "one" },
                {2, "two" },
                {3, "Three" }
            };*/

            Dictionary<string, User> UserDictionnary = new Dictionary<string, User>();
            var listUser = db.User.ToList();
            foreach (User u in listUser)
            {
                UserDictionnary.Add(u.LastName, u);
            }

            if (UserDictionnary.ContainsKey(LastName))
            {
                userchoisi = UserDictionnary[LastName];
            }
            /*
            User result = null;
            if(UserDictionnary.TryGetValue(LastName, out result))
            {
                userchoisi = result;
            }*/

            if (userchoisi == null)
            {
                return HttpNotFound();
            }
            return View(userchoisi);
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