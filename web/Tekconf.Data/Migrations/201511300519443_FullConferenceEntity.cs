namespace Tekconf.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FullConferenceEntity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Conferences", "EndDate", c => c.DateTime(storeType: "date"));
            AddColumn("dbo.Conferences", "IsLive", c => c.Boolean(nullable: false));
            AddColumn("dbo.Conferences", "DatePublished", c => c.DateTime(nullable: false, storeType: "date"));
            AddColumn("dbo.Conferences", "DefaultSessionLength", c => c.Int());
            AddColumn("dbo.Conferences", "CallForSpeakersOpen", c => c.DateTime(storeType: "date"));
            AddColumn("dbo.Conferences", "CallForSpeakersCloses", c => c.DateTime(storeType: "date"));
            AddColumn("dbo.Conferences", "RegistrationOpens", c => c.DateTime(storeType: "date"));
            AddColumn("dbo.Conferences", "RegistrationCloses", c => c.DateTime(storeType: "date"));
            AddColumn("dbo.Conferences", "DateAdded", c => c.DateTime(nullable: false, storeType: "date"));
            AddColumn("dbo.Conferences", "LastUpdated", c => c.DateTime(nullable: false, storeType: "date"));
            AddColumn("dbo.Conferences", "IsOnlineConference", c => c.Boolean(nullable: false));
            AddColumn("dbo.Conferences", "ImageUrl", c => c.String());
            AddColumn("dbo.Conferences", "ImageSquareUrl", c => c.String());
            AddColumn("dbo.Conferences", "TagLine", c => c.String());
            AddColumn("dbo.Conferences", "FacebookUrl", c => c.String());
            AddColumn("dbo.Conferences", "HomepageUrl", c => c.String());
            AddColumn("dbo.Conferences", "LanyrdUrl", c => c.String());
            AddColumn("dbo.Conferences", "TwitterHashtag", c => c.String());
            AddColumn("dbo.Conferences", "TwitterName", c => c.String());
            AddColumn("dbo.Conferences", "MeetupUrl", c => c.String());
            AddColumn("dbo.Conferences", "GooglePlusUrl", c => c.String());
            AddColumn("dbo.Conferences", "VimeoUrl", c => c.String());
            AddColumn("dbo.Conferences", "YouTubeUrl", c => c.String());
            AddColumn("dbo.Conferences", "GithubUrl", c => c.String());
            AddColumn("dbo.Conferences", "LinkedInUrl", c => c.String());
            AlterColumn("dbo.Conferences", "StartDate", c => c.DateTime(storeType: "date"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Conferences", "StartDate", c => c.DateTime(nullable: false, storeType: "date"));
            DropColumn("dbo.Conferences", "LinkedInUrl");
            DropColumn("dbo.Conferences", "GithubUrl");
            DropColumn("dbo.Conferences", "YouTubeUrl");
            DropColumn("dbo.Conferences", "VimeoUrl");
            DropColumn("dbo.Conferences", "GooglePlusUrl");
            DropColumn("dbo.Conferences", "MeetupUrl");
            DropColumn("dbo.Conferences", "TwitterName");
            DropColumn("dbo.Conferences", "TwitterHashtag");
            DropColumn("dbo.Conferences", "LanyrdUrl");
            DropColumn("dbo.Conferences", "HomepageUrl");
            DropColumn("dbo.Conferences", "FacebookUrl");
            DropColumn("dbo.Conferences", "TagLine");
            DropColumn("dbo.Conferences", "ImageSquareUrl");
            DropColumn("dbo.Conferences", "ImageUrl");
            DropColumn("dbo.Conferences", "IsOnlineConference");
            DropColumn("dbo.Conferences", "LastUpdated");
            DropColumn("dbo.Conferences", "DateAdded");
            DropColumn("dbo.Conferences", "RegistrationCloses");
            DropColumn("dbo.Conferences", "RegistrationOpens");
            DropColumn("dbo.Conferences", "CallForSpeakersCloses");
            DropColumn("dbo.Conferences", "CallForSpeakersOpen");
            DropColumn("dbo.Conferences", "DefaultSessionLength");
            DropColumn("dbo.Conferences", "DatePublished");
            DropColumn("dbo.Conferences", "IsLive");
            DropColumn("dbo.Conferences", "EndDate");
        }
    }
}
