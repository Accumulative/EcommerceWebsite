namespace MichellesWebsite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Addresses",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        userId = c.String(),
                        firstLine = c.String(nullable: false),
                        secondLine = c.String(),
                        postcode = c.String(nullable: false),
                        city = c.String(nullable: false),
                        country = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Contacts",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        EnquiryId = c.Int(nullable: false),
                        message = c.String(nullable: false),
                        ts = c.DateTime(nullable: false),
                        FromCustomer = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Enquiries", t => t.EnquiryId, cascadeDelete: true)
                .Index(t => t.EnquiryId);
            
            CreateTable(
                "dbo.Enquiries",
                c => new
                    {
                        EnquiryId = c.Int(nullable: false, identity: true),
                        CustomerId = c.String(maxLength: 128),
                        queryType = c.Int(nullable: false),
                        ts = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.EnquiryId);
            
            CreateTable(
                "dbo.News",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        title = c.String(),
                        description = c.String(),
                        zhTitle = c.String(),
                        zhDescription = c.String(),
                        ts = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.PayPalTransactions",
                c => new
                    {
                        TransactionId = c.Guid(nullable: false),
                        SaleID = c.String(maxLength: 128),
                        RequestId = c.String(),
                        TrackingReference = c.String(),
                        RequestTime = c.DateTime(nullable: false),
                        RequestStatus = c.String(),
                        TimeStamp = c.String(),
                        RequestError = c.String(),
                        Token = c.String(),
                        PayerId = c.String(),
                        RequestData = c.String(),
                        PaymentTransactionId = c.String(),
                        PaymentError = c.String(),
                    })
                .PrimaryKey(t => t.TransactionId);
            
            CreateTable(
                "dbo.ProductModels",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false),
                        description = c.String(),
                        zhName = c.String(),
                        zhDescription = c.String(),
                        ts = c.DateTime(nullable: false),
                        picture = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ProductPrices",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        productID = c.Int(nullable: false),
                        dateFrom = c.DateTime(nullable: false),
                        dateTo = c.DateTime(precision: 7, storeType: "datetime2"),
                        price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.SaleModels",
                c => new
                    {
                        SaleId = c.Guid(nullable: false),
                        CustomerId = c.String(maxLength: 128),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AddressId = c.Int(nullable: false),
                        status = c.Int(nullable: false),
                        ts = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.SaleId);
            
            CreateTable(
                "dbo.SaleProductModels",
                c => new
                    {
                        SaleProductId = c.Int(nullable: false, identity: true),
                        SaleId = c.Guid(nullable: false),
                        PriceId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SaleProductId)
                .ForeignKey("dbo.ProductModels", t => t.ProductId, cascadeDelete: true)
                .ForeignKey("dbo.SaleModels", t => t.SaleId, cascadeDelete: true)
                .Index(t => t.SaleId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FullName = c.String(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.SaleProductModels", "SaleId", "dbo.SaleModels");
            DropForeignKey("dbo.SaleProductModels", "ProductId", "dbo.ProductModels");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Contacts", "EnquiryId", "dbo.Enquiries");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.SaleProductModels", new[] { "ProductId" });
            DropIndex("dbo.SaleProductModels", new[] { "SaleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Contacts", new[] { "EnquiryId" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.SaleProductModels");
            DropTable("dbo.SaleModels");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.ProductPrices");
            DropTable("dbo.ProductModels");
            DropTable("dbo.PayPalTransactions");
            DropTable("dbo.News");
            DropTable("dbo.Enquiries");
            DropTable("dbo.Contacts");
            DropTable("dbo.Addresses");
        }
    }
}
