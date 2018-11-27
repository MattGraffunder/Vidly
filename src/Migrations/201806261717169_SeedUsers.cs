namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedUsers : DbMigration
    {
        public override void Up()
        {
            Sql(@"INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'c1e6a56f-f225-42d6-9efc-66977cf31229', N'guest@vidly.com', 0, N'AAd3HtdyGhR99qb+fif7ij3383kJq6NKAyDcBVv5iUnotTdyHMv3YGEC59veCKhHIg==', N'7a3463b1-8ae1-4a73-9106-3b2093fd9f9d', NULL, 0, 0, NULL, 1, 0, N'guest@vidly.com')
                  INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'dc2034ed-3874-4951-a748-fdcedcf59c23', N'admin@vidly.com', 0, N'AEsE67gi5AlUZzaFEgspN0E0u9K8jvtoAztolnxgNE0mhotp4l7gTFjSA3sUlzsRXw==', N'48ca0820-54b6-49b0-9636-d62c2bd77795', NULL, 0, 0, NULL, 1, 0, N'admin@vidly.com')

                  INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'3f3bd6ed-4e12-4bd7-b65c-834593d10274', N'CanManageMovies')

                  INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'dc2034ed-3874-4951-a748-fdcedcf59c23', N'3f3bd6ed-4e12-4bd7-b65c-834593d10274')");
        }
        
        public override void Down()
        {

        }
    }
}
