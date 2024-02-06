using BookBoowingApp.Domain.Entities;
using BookBoowingApp.Domain.Enums;
using BookBorrowingApp.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookBoowingApp.Infrastructure.DB;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<Book> Books { get; set; }
    public DbSet<Bike> Bikes { get; set; }
    public DbSet<BikeRating> BikeRatings { get; set; }
    public DbSet<Agreement> Agreements { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        var adminRoleId = Guid.NewGuid().ToString();
        var superUserId = Guid.NewGuid().ToString();

        SeedRoles(builder, adminRoleId);
        SeedSuperUserData(builder, adminRoleId, superUserId);
    }

    private static void SeedRoles(ModelBuilder builder, string adminRoleId)
    {
        builder.Entity<IdentityRole>().HasData
        (
            // Seed Roles to Database
            new IdentityRole() { Id = adminRoleId, Name = UserRole.Admin.ToString(), NormalizedName = "ADMIN" },
            new IdentityRole() { Id = Guid.NewGuid().ToString(), Name = UserRole.User.ToString(), NormalizedName = "USER" },
            new IdentityRole() { Id = Guid.NewGuid().ToString(), Name = UserRole.Test.ToString(), NormalizedName = "TEST" }
        );
    }

    private static void SeedSuperUserData(ModelBuilder builder, string adminRoleId, string superUserId)
    {
        //a hasher to hash the password before seeding the user to the db
        var hasher = new PasswordHasher<IdentityUser>();

        // Seed Super Admin User Data to Database.
        builder.Entity<ApplicationUser>().HasData
        (
            new ApplicationUser()
            {
                Id = superUserId,
                Name = "Super Admin",
                UserName = "super_admin",
                NormalizedUserName = "SUPER_ADMIN",
                Email = "super_admin@admin.com",
                NormalizedEmail = "SUPER_ADMIN@ADMIN.COM",
                EmailConfirmed = true,
                PhoneNumber = "1111111111",
                PhoneNumberConfirmed = true,
                Tokens_Available = 1000000,
                Books_Borrowed = [],
                Books_Lent = [],
                PasswordHash = hasher.HashPassword(null!, "Super_Admin@1234")
            }
        );

        // Seed Super Admin Role To Database
        builder.Entity<IdentityUserRole<string>>().HasData
        (
            new IdentityUserRole<string>
            {
                RoleId = adminRoleId,
                UserId = superUserId,
            }
        );
    }
}
