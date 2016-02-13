namespace Tekconf.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SpeakerSlug : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Speakers", "Slug", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Speakers", "Slug");
        }
    }
}
