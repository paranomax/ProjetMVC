namespace ProjetMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialRelations : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Ammoes", "Type", c => c.String(nullable: false));
            AlterColumn("dbo.Weapons", "WeaponModel", c => c.String(nullable: false));
            AlterColumn("dbo.Users", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Users", "LastName", c => c.String(nullable: false));
            AlterColumn("dbo.Users", "Address", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "Address", c => c.String());
            AlterColumn("dbo.Users", "LastName", c => c.String());
            AlterColumn("dbo.Users", "Name", c => c.String());
            AlterColumn("dbo.Weapons", "WeaponModel", c => c.String());
            AlterColumn("dbo.Ammoes", "Type", c => c.String());
        }
    }
}
