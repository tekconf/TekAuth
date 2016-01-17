namespace Tekconf.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveSpeakerNameFromSession : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Sessions", "SpeakerName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Sessions", "SpeakerName", c => c.String());
        }
    }
}
