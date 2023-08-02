using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EZWalk.API.Context;

public class AuthDbContext : IdentityDbContext
{
    public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        string readerRoleId = "9a4be332-9f4e-4e76-96c3-7c3189bf2fb3";
        string writerRoleId = "2f17fd8c-6ca3-45ab-b5a9-abd97b72a933";
        string adminRoleId = "f16361a2-8b94-4692-b382-e1f1f1cc8a59";

        var roles = new List<IdentityRole>()
        {
            new IdentityRole()
            {
                Id = readerRoleId,
                NormalizedName = "Reader".ToUpper(),
                Name = "Reader",
                ConcurrencyStamp = readerRoleId
            },
            new IdentityRole()
            {
                Id = writerRoleId,
                Name = "Writer",
                NormalizedName = "Writer".ToUpper(),
                ConcurrencyStamp = writerRoleId
            },
            new IdentityRole()
            {
                Id = adminRoleId,
                Name = "Admin",
                NormalizedName = "Admin".ToUpper(),
                ConcurrencyStamp = adminRoleId
            }
        };


        builder.Entity<IdentityRole>().HasData(roles);
    }
}

