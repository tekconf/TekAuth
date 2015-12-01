namespace Tekconf.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingUsers : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 500),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserConferences",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        ConferenceId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.ConferenceId })
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.Conferences", t => t.ConferenceId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.ConferenceId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserConferences", "ConferenceId", "dbo.Conferences");
            DropForeignKey("dbo.UserConferences", "UserId", "dbo.Users");
            DropIndex("dbo.UserConferences", new[] { "ConferenceId" });
            DropIndex("dbo.UserConferences", new[] { "UserId" });
            DropTable("dbo.UserConferences");
            DropTable("dbo.Users");
        }
    }
}
