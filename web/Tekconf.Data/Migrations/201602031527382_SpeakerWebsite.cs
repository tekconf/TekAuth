namespace Tekconf.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SpeakerWebsite : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Speakers", "JobTitle", c => c.String(maxLength: 100));
            AddColumn("dbo.Speakers", "WebsiteUrl", c => c.String(maxLength: 2083));
            AlterColumn("dbo.Speakers", "ImageUrl", c => c.String(maxLength: 2083));
            AlterColumn("dbo.Speakers", "LinkedInUrl", c => c.String(maxLength: 2083));
            AlterColumn("dbo.Speakers", "GithubUrl", c => c.String(maxLength: 2083));
            AlterColumn("dbo.Speakers", "FacebookUrl", c => c.String(maxLength: 2083));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Speakers", "FacebookUrl", c => c.String());
            AlterColumn("dbo.Speakers", "GithubUrl", c => c.String());
            AlterColumn("dbo.Speakers", "LinkedInUrl", c => c.String());
            AlterColumn("dbo.Speakers", "ImageUrl", c => c.String());
            DropColumn("dbo.Speakers", "WebsiteUrl");
            DropColumn("dbo.Speakers", "JobTitle");
        }
    }
}
