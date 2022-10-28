namespace SimpleSignupSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tblActiveItem",
                c => new
                    {
                        cItemID = c.Int(nullable: false, identity: true),
                        cItemName = c.String(maxLength: 4000),
                        cActiveDt = c.String(unicode: false, storeType: "text"),
                    })
                .PrimaryKey(t => t.cItemID);
            
            CreateTable(
                "dbo.tblSignupItem",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        cMobile = c.String(nullable: false, maxLength: 10, unicode: false),
                        cItemID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ID, t.cMobile, t.cItemID })
                .ForeignKey("dbo.tblActiveItem", t => t.cItemID, cascadeDelete: true)
                .ForeignKey("dbo.tblSignup", t => t.cMobile, cascadeDelete: true)
                .Index(t => t.cMobile)
                .Index(t => t.cItemID);
            
            CreateTable(
                "dbo.tblSignup",
                c => new
                    {
                        cMobile = c.String(nullable: false, maxLength: 10, unicode: false),
                        cName = c.String(maxLength: 20),
                        cEmail = c.String(maxLength: 50),
                        cCreateDT = c.DateTime(nullable: false, storeType: "date"),
                    })
                .PrimaryKey(t => t.cMobile);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tblSignupItem", "cMobile", "dbo.tblSignup");
            DropForeignKey("dbo.tblSignupItem", "cItemID", "dbo.tblActiveItem");
            DropIndex("dbo.tblSignupItem", new[] { "cItemID" });
            DropIndex("dbo.tblSignupItem", new[] { "cMobile" });
            DropTable("dbo.tblSignup");
            DropTable("dbo.tblSignupItem");
            DropTable("dbo.tblActiveItem");
        }
    }
}
