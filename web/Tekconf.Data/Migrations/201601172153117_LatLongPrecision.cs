namespace Tekconf.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LatLongPrecision : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Conferences", "Latitude", c => c.Decimal(precision: 12, scale: 10));
            AlterColumn("dbo.Conferences", "Longitude", c => c.Decimal(precision: 12, scale: 10));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Conferences", "Longitude", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.Conferences", "Latitude", c => c.Decimal(precision: 18, scale: 2));
        }
    }
}
