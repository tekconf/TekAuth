namespace Tekconf.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Schedules : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Schedules",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        ConferenceId = c.Int(nullable: false),
                        Created = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Conferences", t => t.ConferenceId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.ConferenceId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Schedules", "UserId", "dbo.Users");
            DropForeignKey("dbo.Schedules", "ConferenceId", "dbo.Conferences");
            DropIndex("dbo.Schedules", new[] { "ConferenceId" });
            DropIndex("dbo.Schedules", new[] { "UserId" });
            DropTable("dbo.Schedules");
        }
    }
}
