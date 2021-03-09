namespace SchoolLibrary.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class COommit : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Authors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IBooks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        Descriptions = c.String(nullable: false),
                        ISBN = c.String(nullable: false),
                        Published = c.DateTime(nullable: false),
                        PageCount = c.Int(nullable: false),
                        AuthorId = c.Int(nullable: false),
                        CategoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Authors", t => t.AuthorId, cascadeDelete: true)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.AuthorId)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.IBooks", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.IBooks", "AuthorId", "dbo.Authors");
            DropIndex("dbo.IBooks", new[] { "CategoryId" });
            DropIndex("dbo.IBooks", new[] { "AuthorId" });
            DropTable("dbo.Categories");
            DropTable("dbo.IBooks");
            DropTable("dbo.Authors");
        }
    }
}
