namespace Tekconf.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Constraints : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Speakers", "Bio", c => c.String());
            AlterColumn("dbo.Conferences", "AddressLine1", c => c.String(maxLength: 100));
            AlterColumn("dbo.Conferences", "AddressLine2", c => c.String(maxLength: 100));
            AlterColumn("dbo.Conferences", "AddressLine3", c => c.String(maxLength: 100));
            AlterColumn("dbo.Conferences", "City", c => c.String(maxLength: 100));
            AlterColumn("dbo.Conferences", "StateOrProvince", c => c.String(maxLength: 50));
            AlterColumn("dbo.Conferences", "PostalCode", c => c.String(maxLength: 20));
            AlterColumn("dbo.Conferences", "VenuePhoneNumber", c => c.String(maxLength: 20));
            AlterColumn("dbo.Conferences", "OrganizerPhoneNumber", c => c.String(maxLength: 20));
            AlterColumn("dbo.Conferences", "TwitterHashtag", c => c.String(maxLength: 20));
            AlterColumn("dbo.Conferences", "TwitterName", c => c.String(maxLength: 50));
            AlterColumn("dbo.Speakers", "FirstName", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Speakers", "MiddleName", c => c.String(maxLength: 50));
            AlterColumn("dbo.Speakers", "LastName", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Speakers", "EmailAddress", c => c.String(maxLength: 100));
            AlterColumn("dbo.Speakers", "TwitterHandle", c => c.String(maxLength: 50));
            AlterColumn("dbo.Speakers", "CompanyName", c => c.String(maxLength: 100));
            AlterColumn("dbo.Speakers", "PhoneNumber", c => c.String(maxLength: 20));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Speakers", "PhoneNumber", c => c.String());
            AlterColumn("dbo.Speakers", "CompanyName", c => c.String());
            AlterColumn("dbo.Speakers", "TwitterHandle", c => c.String());
            AlterColumn("dbo.Speakers", "EmailAddress", c => c.String());
            AlterColumn("dbo.Speakers", "LastName", c => c.String(nullable: false));
            AlterColumn("dbo.Speakers", "MiddleName", c => c.String());
            AlterColumn("dbo.Speakers", "FirstName", c => c.String(nullable: false));
            AlterColumn("dbo.Conferences", "TwitterName", c => c.String());
            AlterColumn("dbo.Conferences", "TwitterHashtag", c => c.String());
            AlterColumn("dbo.Conferences", "OrganizerPhoneNumber", c => c.String());
            AlterColumn("dbo.Conferences", "VenuePhoneNumber", c => c.String());
            AlterColumn("dbo.Conferences", "PostalCode", c => c.String());
            AlterColumn("dbo.Conferences", "StateOrProvince", c => c.String());
            AlterColumn("dbo.Conferences", "City", c => c.String());
            AlterColumn("dbo.Conferences", "AddressLine3", c => c.String());
            AlterColumn("dbo.Conferences", "AddressLine2", c => c.String());
            AlterColumn("dbo.Conferences", "AddressLine1", c => c.String());
            DropColumn("dbo.Speakers", "Bio");
        }
    }
}
