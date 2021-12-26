using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace ProjetMVC.Models
{
    public class User
    {
        public int UserID { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }
        public string Address { get; set; }

        public virtual ICollection<Certificat> Certificats { get; set; }

    }

    public class Certificat
    {
        public int CertificatID { get; set; }
       /* public int UserID { get; set; }
        public int WeaponID { get; set; }*/
        public DateTime DateBegin { get; set; }
        public DateTime DateEnd { get; set; }

        public virtual User User { get; set; }
        public virtual Weapon Weapon { get; set; }
    }

    public class Weapon
    {
        public int WeaponID { get; set; }
        public string WeaponModel { get; set; }
        public string AmmoID { get; set; }

        public virtual Ammo Ammo { get; set; }
        public virtual ICollection<Certificat> Certificats { get; set; }

    }

    public class Ammo
    {
        public int AmmoID { get; set; }
        public string Type { get; set; }
        public int Caliber { get; set; }

        public virtual ICollection<Weapon> Weapons { get; set; }


    }
    public class Model : DbContext
    {   
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<Certificat> Certificat { get; set; }
        public virtual DbSet<Weapon> Weapon { get; set; }
        public virtual DbSet<Ammo> Ammo { get; set; }

        /*
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set;} */
    }
}