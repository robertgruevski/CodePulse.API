using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CodePulse.API.Data
{
	public class AuthDbContext : IdentityDbContext
	{
		public AuthDbContext(DbContextOptions options) : base(options)
		{

		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			var readerRoleId = "a885180a-ecb6-4bea-82f1-40711793d403";
			var writerRoleId = "8d87a100-3381-428f-aaeb-bd09efbd4bc4";

			// Create Reader and Writer Roles
			var roles = new List<IdentityRole>
			{
				new IdentityRole()
				{
					Id = readerRoleId,
					Name = "Reader",
					NormalizedName = "Reader".ToUpper(),
					ConcurrencyStamp = readerRoleId
				},
				new IdentityRole()
				{
					Id = writerRoleId,
					Name = "Writer",
					NormalizedName = "Writer".ToUpper(),
					ConcurrencyStamp = writerRoleId
				}
			};

			// Seed the roles
			builder.Entity<IdentityRole>().HasData(roles);

			// Create an Admin User
			var adminUserId = "72dda976-b56c-4c68-8e07-951da0f15fec";
			var admin = new IdentityUser()
			{
				Id = adminUserId,
				UserName = "admin@codepulse.com",
				Email = "admin@codepulse.com",
				NormalizedEmail = "admin@codepulse.com".ToUpper(),
				NormalizedUserName = "admin@codepulse.com".ToUpper()
			};

			admin.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(admin, "Admin@123");

			builder.Entity<IdentityUser>().HasData(admin);

			// Give Roles to Admin
			var adminRoles = new List<IdentityUserRole<string>>()
			{
				new()
				{
					UserId = adminUserId,
					RoleId = readerRoleId
				},
				new()
				{
					UserId = adminUserId,
					RoleId = writerRoleId
				}
			};

			builder.Entity<IdentityUserRole<string>>().HasData(adminRoles);
		}
	}
}
