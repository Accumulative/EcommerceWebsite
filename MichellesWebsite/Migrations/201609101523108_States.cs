namespace MichellesWebsite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class States : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Addresses", "state");
            DropColumn("dbo.DeliveryModels", "state");
            CreateTable(
                "dbo.States",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(),
                        zhName = c.String(),
                        Country = c.Int(nullable: false),
                        ts = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            AddColumn("dbo.Addresses", "stateId", c => c.Int(nullable: false));
            AddColumn("dbo.DeliveryModels", "stateId", c => c.Int(nullable: false));
            CreateIndex("dbo.Addresses", "stateId");
            CreateIndex("dbo.DeliveryModels", "stateId");
            AddForeignKey("dbo.Addresses", "stateId", "dbo.States", "id", cascadeDelete: true);
            AddForeignKey("dbo.DeliveryModels", "stateId", "dbo.States", "id", cascadeDelete: true);
            
        }
        
        public override void Down()
        {
            AddColumn("dbo.DeliveryModels", "state", c => c.Int(nullable: false));
            AddColumn("dbo.Addresses", "state", c => c.Int(nullable: false));
            DropForeignKey("dbo.DeliveryModels", "stateId", "dbo.States");
            DropForeignKey("dbo.Addresses", "stateId", "dbo.States");
            DropIndex("dbo.DeliveryModels", new[] { "stateId" });
            DropIndex("dbo.Addresses", new[] { "stateId" });
            DropColumn("dbo.DeliveryModels", "stateId");
            DropColumn("dbo.Addresses", "stateId");
            DropTable("dbo.States");
        }
    }
}
