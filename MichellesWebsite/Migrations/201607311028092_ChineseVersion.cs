namespace MichellesWebsite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChineseVersion : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CultureCountries",
                c => new
                    {
                        CultureCountryId = c.String(nullable: false, maxLength: 128),
                        country = c.Int(nullable: false),
                        culture = c.String(),
                    })
                .PrimaryKey(t => t.CultureCountryId);
            
            AddColumn("dbo.ProductPrices", "country", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ProductPrices", "country");
            DropTable("dbo.CultureCountries");
        }
    }
}
