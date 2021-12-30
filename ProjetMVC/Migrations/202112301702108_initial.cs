namespace ProjetMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Ammoes",
                c => new
                    {
                        AmmoID = c.Int(nullable: false, identity: true),
                        Type = c.String(nullable: false),
                        Caliber = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AmmoID);
            
            CreateTable(
                "dbo.Weapons",
                c => new
                    {
                        WeaponID = c.Int(nullable: false, identity: true),
                        WeaponModel = c.String(nullable: false),
                        Ammo_AmmoID = c.Int(),
                    })
                .PrimaryKey(t => t.WeaponID)
                .ForeignKey("dbo.Ammoes", t => t.Ammo_AmmoID)
                .Index(t => t.Ammo_AmmoID);
            
            CreateTable(
                "dbo.Certificats",
                c => new
                    {
                        CertificatID = c.Int(nullable: false, identity: true),
                        DateBegin = c.DateTime(nullable: false),
                        DateEnd = c.DateTime(nullable: false),
                        User_UserID = c.Int(nullable: false),
                        Weapon_WeaponID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CertificatID)
                .ForeignKey("dbo.Users", t => t.User_UserID, cascadeDelete: true)
                .ForeignKey("dbo.Weapons", t => t.Weapon_WeaponID, cascadeDelete: true)
                .Index(t => t.User_UserID)
                .Index(t => t.Weapon_WeaponID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        Birthday = c.DateTime(nullable: false),
                        Address = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.UserID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Weapons", "Ammo_AmmoID", "dbo.Ammoes");
            DropForeignKey("dbo.Certificats", "Weapon_WeaponID", "dbo.Weapons");
            DropForeignKey("dbo.Certificats", "User_UserID", "dbo.Users");
            DropIndex("dbo.Certificats", new[] { "Weapon_WeaponID" });
            DropIndex("dbo.Certificats", new[] { "User_UserID" });
            DropIndex("dbo.Weapons", new[] { "Ammo_AmmoID" });
            DropTable("dbo.Users");
            DropTable("dbo.Certificats");
            DropTable("dbo.Weapons");
            DropTable("dbo.Ammoes");
        }
    }
}
