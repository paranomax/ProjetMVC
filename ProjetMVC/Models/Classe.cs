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

    }

    public class Certificat
    {
        public int CertificatID { get; set; }
        public int UserID { get; set; }
        public int WeaponID { get; set; }
        public DateTime DateBegin { get; set; }
        public DateTime DateEnd { get; set; }
    }

    public class Weapon
    {
        public int WeaponID { get; set; }
        public string WeaponModel { get; set; }
        public string AmmoID { get; set; }

    }

    public class Ammo
    {
        public int AmmoID { get; set; }
        public string Type { get; set; }
        public int Caliber { get; set; }

    }
    public class Model : DbContext
    {/*
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set;} */
    }
}