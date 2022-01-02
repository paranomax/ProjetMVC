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
        public int UserID { get; set; }
        public int WeaponID { get; set; }
        public DateTime DateBegin { get; set; }
        public DateTime DateEnd { get; set; }
        public virtual User User { get; set; }
        public virtual Weapon Weapon { get; set; }
    }

    public class Weapon
    {
        public int WeaponID { get; set; }
        public string WeaponModel { get; set; }
        public int AmmoID { get; set; }

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
    public class StoreContext : DbContext
    {   

        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<Certificat> Certificat { get; set; }
        public virtual DbSet<Weapon> Weapon { get; set; }
        public virtual DbSet<Ammo> Ammo { get; set; }

        /*
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set;} */


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var weapons = modelBuilder.Entity<Weapon>();
            weapons.HasKey(b => b.WeaponID);
            weapons.Property(b => b.WeaponModel).IsRequired();
            weapons.HasRequired(w => w.Ammo).WithMany(a => a.Weapons).HasForeignKey(w => w.AmmoID);
            
            //weapons.HasMany(b => b.Certificats).WithOptional(a => a.Weapon).HasForeignKey(e => e.Weapon.WeaponID);


            var users = modelBuilder.Entity<User>();
            users.HasKey(b => b.UserID);
            users.Property(b => b.Name).IsRequired();
            users.Property(b => b.LastName).IsRequired();
            users.Property(b => b.Birthday).IsRequired();
            users.Property(b => b.Address).IsRequired();
            
            
            var certificats = modelBuilder.Entity<Certificat>();
            certificats.HasKey(b => b.CertificatID);
            certificats.Property(b => b.DateBegin).IsRequired();
            certificats.Property(b => b.DateEnd).IsRequired();
            certificats.HasRequired(c => c.User).WithMany(u => u.Certificats).HasForeignKey(p => p.UserID);
            certificats.HasRequired(c => c.Weapon).WithMany(w => w.Certificats).HasForeignKey(p => p.WeaponID);

            var ammos = modelBuilder.Entity<Ammo>();
            ammos.HasKey(b => b.AmmoID);
            ammos.Property(b => b.Caliber).IsRequired();
            ammos.Property(b => b.Type).IsRequired();
            //ammos.HasMany<Weapon>(b => b.Weapons).WithRequired(p => p.Ammo).HasForeignKey(p => p.AmmoID);
        }
       /* public StoreContext() : base("WeaponContext-20211229113635_Annotation")
        {
            Database.SetInitializer<StoreContext>(new DropCreateDatabaseIfModelChanges<StoreContext>());
        }*/
    }
}