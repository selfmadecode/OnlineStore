namespace SmartStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAdminAndStoreManagerRole : DbMigration
    {
        public override void Up()
        {
            Sql(@"
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'2c6e21e7-d553-498c-b999-d46f54be029b', N'user@smartstore.com', 0, N'APDypcWV+6gobQT6NCVPpXUDAL631NzPJp4rHlzYvnv5oxoHSy3suEfNh7D7ldd0qA==', N'4e74f4c8-98c2-4bf1-9cd4-81ada8a58f15', NULL, 0, 0, NULL, 1, 0, N'user@smartstore.com')
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'496fdded-155a-4e1e-a5e3-31838cde951c', N'storemanager@smartstore.com', 0, N'AAuRZdsT8XlcGZih4l/udrJ5uld6DvfLXHyXTgvbx+OT5z5mOS69X6QCPveeJxG7HQ==', N'c59279cb-6e34-43e9-80ee-417945c42d02', NULL, 0, 0, NULL, 1, 0, N'storemanager@smartstore.com')
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'b7994c35-a66c-4473-b693-8a8cba0b9a2c', N'admin@smartstore.com', 0, N'ABIvVwaNNUwPQXw6n18MLSOd/byc4E7TBFhCpxWH4CWzmRElWEeeT3oj7RHuo1SWtQ==', N'7327bf80-facb-4b7b-a4ed-53d0db23c1dd', NULL, 0, 0, NULL, 1, 0, N'admin@smartstore.com')

INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'ccab7888-c46c-4c90-9fee-8d9b3cc6db5c', N'Admin')
INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'97ba95a4-f6ca-4836-80fc-de00b09fd9b5', N'StoreManager')

INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'496fdded-155a-4e1e-a5e3-31838cde951c', N'97ba95a4-f6ca-4836-80fc-de00b09fd9b5')
INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'b7994c35-a66c-4473-b693-8a8cba0b9a2c', N'ccab7888-c46c-4c90-9fee-8d9b3cc6db5c')


");
        }
        
        public override void Down()
        {
        }
    }
}
