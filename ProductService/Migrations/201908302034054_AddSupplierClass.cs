namespace ProductService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSupplierClass : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Suppliers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.Products", "SupplierID", c => c.Int());
            CreateIndex("dbo.Products", "SupplierID");
            AddForeignKey("dbo.Products", "SupplierID", "dbo.Suppliers", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "SupplierID", "dbo.Suppliers");
            DropIndex("dbo.Products", new[] { "SupplierID" });
            DropColumn("dbo.Products", "SupplierID");
            DropTable("dbo.Suppliers");
        }
    }
}
