namespace Tekconf.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Sessions : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Sessions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Slug = c.String(nullable: false, maxLength: 200),
                        Title = c.String(nullable: false, maxLength: 400),
                        StartDate = c.DateTime(storeType: "date"),
                        EndDate = c.DateTime(storeType: "date"),
                        SpeakerName = c.String(),
                        Description = c.String(),
                        Room = c.String(maxLength: 100),
                        ConferenceId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Conferences", t => t.ConferenceId, cascadeDelete: true)
                .Index(t => t.ConferenceId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Sessions", "ConferenceId", "dbo.Conferences");
            DropIndex("dbo.Sessions", new[] { "ConferenceId" });
            DropTable("dbo.Sessions");
        }
    }
}
