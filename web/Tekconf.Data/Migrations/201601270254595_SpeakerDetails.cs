namespace Tekconf.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SpeakerDetails : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Speakers", "ImageUrl", c => c.String());
            AddColumn("dbo.Speakers", "EmailAddress", c => c.String());
            AddColumn("dbo.Speakers", "TwitterHandle", c => c.String());
            AddColumn("dbo.Speakers", "CompanyName", c => c.String());
            AddColumn("dbo.Speakers", "LinkedInUrl", c => c.String());
            AddColumn("dbo.Speakers", "GithubUrl", c => c.String());
            AddColumn("dbo.Speakers", "FacebookUrl", c => c.String());
            AddColumn("dbo.Speakers", "PhoneNumber", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Speakers", "PhoneNumber");
            DropColumn("dbo.Speakers", "FacebookUrl");
            DropColumn("dbo.Speakers", "GithubUrl");
            DropColumn("dbo.Speakers", "LinkedInUrl");
            DropColumn("dbo.Speakers", "CompanyName");
            DropColumn("dbo.Speakers", "TwitterHandle");
            DropColumn("dbo.Speakers", "EmailAddress");
            DropColumn("dbo.Speakers", "ImageUrl");
        }
    }
}
