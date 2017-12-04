namespace DAL.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RecreateDatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                    "dbo.AccountOwners",
                    c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                    })
                .PrimaryKey(t => t.Id);

            CreateTable(
                    "dbo.Accounts",
                    c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AccountNumber = c.String(),
                        Balance = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Bonus = c.Int(nullable: false),
                        AccountOwnerId = c.Int(nullable: false),
                        AccountTypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AccountOwners", t => t.AccountOwnerId, cascadeDelete: true)
                .ForeignKey("dbo.AccountTypes", t => t.AccountTypeId, cascadeDelete: true)
                .Index(t => t.AccountOwnerId)
                .Index(t => t.AccountTypeId);

            CreateTable(
                    "dbo.AccountTypes",
                    c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Accounts", "AccountTypeId", "dbo.AccountTypes");
            DropForeignKey("dbo.Accounts", "AccountOwnerId", "dbo.AccountOwners");
            DropIndex("dbo.Accounts", new[] { "AccountTypeId" });
            DropIndex("dbo.Accounts", new[] { "AccountOwnerId" });
            DropTable("dbo.AccountTypes");
            DropTable("dbo.Accounts");
            DropTable("dbo.AccountOwners");
        }
    }
}
