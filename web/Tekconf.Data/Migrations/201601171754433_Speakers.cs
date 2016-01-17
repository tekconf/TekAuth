namespace Tekconf.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Speakers : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Speakers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false),
                        MiddleName = c.String(),
                        LastName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SessionSpeakers",
                c => new
                    {
                        SpeakerId = c.Int(nullable: false),
                        SessionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.SpeakerId, t.SessionId })
                .ForeignKey("dbo.Speakers", t => t.SpeakerId, cascadeDelete: true)
                .ForeignKey("dbo.Sessions", t => t.SessionId, cascadeDelete: true)
                .Index(t => t.SpeakerId)
                .Index(t => t.SessionId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SessionSpeakers", "SessionId", "dbo.Sessions");
            DropForeignKey("dbo.SessionSpeakers", "SpeakerId", "dbo.Speakers");
            DropIndex("dbo.SessionSpeakers", new[] { "SessionId" });
            DropIndex("dbo.SessionSpeakers", new[] { "SpeakerId" });
            DropTable("dbo.SessionSpeakers");
            DropTable("dbo.Speakers");
        }
    }
}
