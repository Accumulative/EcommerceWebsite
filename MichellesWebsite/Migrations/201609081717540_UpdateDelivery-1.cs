namespace MichellesWebsite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateDelivery1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DeliveryModels",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        state = c.Int(nullable: false),
                        costWithin500 = c.Decimal(nullable: false, precision: 18, scale: 2),
                        costWithin1000 = c.Decimal(nullable: false, precision: 18, scale: 2),
                        costPer1000 = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.DeliveryModels");
        }
    }
}
