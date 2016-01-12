namespace Tekconf.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Indexes : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Conferences", "Slug", unique: true, name: "IX_ConferenceSlug");
            CreateIndex("dbo.Conferences", "Name");
            CreateIndex("dbo.Sessions", "Slug", unique: true, name: "IX_SessionSlug");
            CreateIndex("dbo.Sessions", "Title");
            CreateIndex("dbo.Users", "Name");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Users", new[] { "Name" });
            DropIndex("dbo.Sessions", new[] { "Title" });
            DropIndex("dbo.Sessions", "IX_SessionSlug");
            DropIndex("dbo.Conferences", new[] { "Name" });
            DropIndex("dbo.Conferences", "IX_ConferenceSlug");
        }
    }
}
