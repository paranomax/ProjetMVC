namespace ProjetMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Ammoes",
                c => new
                    {
                        AmmoID = c.Int(nullable: false, identity: true),
                        Type = c.String(),
                        Caliber = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AmmoID);
            
            CreateTable(
                "dbo.Weapons",
                c => new
                    {
                        WeaponID = c.Int(nullable: false, identity: true),
                        WeaponModel = c.String(),
                        AmmoID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.WeaponID)
                .ForeignKey("dbo.Ammoes", t => t.AmmoID, cascadeDelete: true)
                .Index(t => t.AmmoID);
            
            CreateTable(
                "dbo.Certificats",
                c => new
                    {
                        CertificatID = c.Int(nullable: false, identity: true),
                        UserID = c.Int(nullable: false),
                        WeaponID = c.Int(nullable: false),
                        DateBegin = c.DateTime(nullable: false),
                        DateEnd = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.CertificatID)
                .ForeignKey("dbo.Users", t => t.UserID, cascadeDelete: true)
                .ForeignKey("dbo.Weapons", t => t.WeaponID, cascadeDelete: true)
                .Index(t => t.UserID)
                .Index(t => t.WeaponID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        LastName = c.String(),
                        Birthday = c.DateTime(nullable: false),
                        Address = c.String(),
                    })
                .PrimaryKey(t => t.UserID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Certificats", "WeaponID", "dbo.Weapons");
            DropForeignKey("dbo.Certificats", "UserID", "dbo.Users");
            DropForeignKey("dbo.Weapons", "AmmoID", "dbo.Ammoes");
            DropIndex("dbo.Certificats", new[] { "WeaponID" });
            DropIndex("dbo.Certificats", new[] { "UserID" });
            DropIndex("dbo.Weapons", new[] { "AmmoID" });
            DropTable("dbo.Users");
            DropTable("dbo.Certificats");
            DropTable("dbo.Weapons");
            DropTable("dbo.Ammoes");
        }
    }
}
