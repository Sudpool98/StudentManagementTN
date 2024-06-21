namespace StudentManagementTN.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateStuMgmtTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ClassDivisions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Classno = c.Int(nullable: false),
                        Division = c.String(nullable: false, maxLength: 1, fixedLength: true, unicode: false),
                    })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.EduStatus",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Rank = c.Int(nullable: false),
                    Status = c.String(nullable: false, maxLength: 50),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.Students",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Address = c.String(nullable: false),
                        Edustatusid = c.Int(nullable: false),
                        Classid = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ClassDivisions", t => t.Classid, cascadeDelete: true)
                .ForeignKey("dbo.EduStatus", t => t.Edustatusid, cascadeDelete: true)
                .Index(t => t.Edustatusid)
                .Index(t => t.Classid);
            
            CreateTable(
                "dbo.Teachers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Contact = c.String(nullable: false, maxLength: 10),
                        Address = c.String(nullable: false),
                        Classid = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ClassDivisions", t => t.Classid, cascadeDelete: true)
                .Index(t => t.Contact, unique: true, name: "IX_ContactUniqueKey")
                .Index(t => t.Classid);
            
            CreateTable(
                "dbo.Principals",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Username = c.String(nullable: false, maxLength: 50),
                        Password = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Username, unique: true, name: "IX_UsernameUniqueKey");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Teachers", "Classid", "dbo.ClassDivisions");
            DropForeignKey("dbo.Students", "Edustatusid", "dbo.EduStatus");
            DropForeignKey("dbo.Students", "Classid", "dbo.ClassDivisions");
            DropIndex("dbo.Principals", "IX_UsernameUniqueKey");
            DropIndex("dbo.Teachers", new[] { "Classid" });
            DropIndex("dbo.Teachers", "IX_ContactUniqueKey");
            DropIndex("dbo.Students", new[] { "Classid" });
            DropIndex("dbo.Students", new[] { "Edustatusid" });
            DropTable("dbo.Principals");
            DropTable("dbo.Teachers");
            DropTable("dbo.EduStatus");
            DropTable("dbo.Students");
            DropTable("dbo.ClassDivisions");
        }
    }
}
