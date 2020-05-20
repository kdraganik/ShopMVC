namespace ShopMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SwitchBackToListItem : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Orders", "ItemsList_Id", "dbo.ItemsLists");
            DropIndex("dbo.Orders", new[] { "ItemsList_Id" });
            AddColumn("dbo.Items", "Order_Id", c => c.Int());
            CreateIndex("dbo.Items", "Order_Id");
            AddForeignKey("dbo.Items", "Order_Id", "dbo.Orders", "Id");
            DropColumn("dbo.Orders", "ItemsList_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "ItemsList_Id", c => c.Int(nullable: false));
            DropForeignKey("dbo.Items", "Order_Id", "dbo.Orders");
            DropIndex("dbo.Items", new[] { "Order_Id" });
            DropColumn("dbo.Items", "Order_Id");
            CreateIndex("dbo.Orders", "ItemsList_Id");
            AddForeignKey("dbo.Orders", "ItemsList_Id", "dbo.ItemsLists", "Id", cascadeDelete: true);
        }
    }
}
