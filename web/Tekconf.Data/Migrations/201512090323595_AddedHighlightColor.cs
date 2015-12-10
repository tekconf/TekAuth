namespace Tekconf.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedHighlightColor : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Conferences", "HighlightColor", c => c.String(maxLength: 10));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Conferences", "HighlightColor");
        }
    }
}
