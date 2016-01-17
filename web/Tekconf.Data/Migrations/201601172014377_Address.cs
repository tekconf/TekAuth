namespace Tekconf.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Address : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Conferences", "AddressLine1", c => c.String());
            AddColumn("dbo.Conferences", "AddressLine2", c => c.String());
            AddColumn("dbo.Conferences", "AddressLine3", c => c.String());
            AddColumn("dbo.Conferences", "City", c => c.String());
            AddColumn("dbo.Conferences", "StateOrProvince", c => c.String());
            AddColumn("dbo.Conferences", "PostalCode", c => c.String());
            AddColumn("dbo.Conferences", "Country", c => c.String());
            AddColumn("dbo.Conferences", "Latitude", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.Conferences", "Longitude", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.Conferences", "VenuePhoneNumber", c => c.String());
            AddColumn("dbo.Conferences", "OrganizerPhoneNumber", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Conferences", "OrganizerPhoneNumber");
            DropColumn("dbo.Conferences", "VenuePhoneNumber");
            DropColumn("dbo.Conferences", "Longitude");
            DropColumn("dbo.Conferences", "Latitude");
            DropColumn("dbo.Conferences", "Country");
            DropColumn("dbo.Conferences", "PostalCode");
            DropColumn("dbo.Conferences", "StateOrProvince");
            DropColumn("dbo.Conferences", "City");
            DropColumn("dbo.Conferences", "AddressLine3");
            DropColumn("dbo.Conferences", "AddressLine2");
            DropColumn("dbo.Conferences", "AddressLine1");
        }
    }
}
