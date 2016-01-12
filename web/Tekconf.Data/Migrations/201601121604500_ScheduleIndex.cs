namespace Tekconf.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ScheduleIndex : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Schedules", new[] { "UserId" });
            DropIndex("dbo.Schedules", new[] { "ConferenceId" });
            CreateIndex("dbo.Schedules", new[] { "UserId", "ConferenceId" }, unique: true, name: "IX_UserIdConferenceId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Schedules", "IX_UserIdConferenceId");
            CreateIndex("dbo.Schedules", "ConferenceId");
            CreateIndex("dbo.Schedules", "UserId");
        }
    }
}
