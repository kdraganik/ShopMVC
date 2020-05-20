namespace ShopMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdminUser : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO [dbo].[AspNetUsers] ([Id], [Name], [Phone], [Address], [City], [PostalCode], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'206a5903-7769-41e5-a0ec-40a29e52e0b3', N'Karol Draganik', N'785322000', N'Ma³opanewska 22a, 2', N'Wroc³aw', N'54-212', N'admin@shopmvc.com', 0, N'AK9NQmd9cKRWOAimgSoBeB47NbUAdrqadXRoFhAkbeFNCEk6fvqXORbF+J5tov+k1w==', N'32ea9aaf-4e5f-4420-912a-44972b231a22', NULL, 0, 0, NULL, 1, 0, N'admin@shopmvc.com')");
            Sql("INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'126a5f96-5c1f-4dd6-9644-1aa86c372929', N'Admin')");
            Sql("INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'206a5903-7769-41e5-a0ec-40a29e52e0b3', N'126a5f96-5c1f-4dd6-9644-1aa86c372929')");
        }
        
        public override void Down()
        {
        }
    }
}
