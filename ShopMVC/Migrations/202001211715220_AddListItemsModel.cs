namespace ShopMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddListItemsModel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Items", "Order_Id", "dbo.Orders");
            DropIndex("dbo.Items", new[] { "Order_Id" });
            CreateTable(
                "dbo.ItemsLists",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Items", "ItemsList_Id", c => c.Int());
            AddColumn("dbo.Orders", "ItemsList_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.Items", "ItemsList_Id");
            CreateIndex("dbo.Orders", "ItemsList_Id");
            AddForeignKey("dbo.Items", "ItemsList_Id", "dbo.ItemsLists", "Id");
            AddForeignKey("dbo.Orders", "ItemsList_Id", "dbo.ItemsLists", "Id", cascadeDelete: true);
            DropColumn("dbo.Items", "Order_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Items", "Order_Id", c => c.Int());
            DropForeignKey("dbo.Orders", "ItemsList_Id", "dbo.ItemsLists");
            DropForeignKey("dbo.Items", "ItemsList_Id", "dbo.ItemsLists");
            DropIndex("dbo.Orders", new[] { "ItemsList_Id" });
            DropIndex("dbo.Items", new[] { "ItemsList_Id" });
            DropColumn("dbo.Orders", "ItemsList_Id");
            DropColumn("dbo.Items", "ItemsList_Id");
            DropTable("dbo.ItemsLists");
            CreateIndex("dbo.Items", "Order_Id");
            AddForeignKey("dbo.Items", "Order_Id", "dbo.Orders", "Id");
        }
    }
}
