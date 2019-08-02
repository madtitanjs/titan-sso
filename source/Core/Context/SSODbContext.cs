using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SSO.Core.Identity.Models;
using System;

namespace SSO.Core.Context
{
    public class SSODbContext : IdentityDbContext<User, Role, Guid, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>
    {
        public SSODbContext(DbContextOptions<SSODbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ClaimType>(b =>
            {
                b.ToTable("ClaimTypes");
                b.HasIndex(u => u.Name).IsUnique();
            });

            modelBuilder.Entity<User>(b =>
            {
                // Map to User Tables
                b.ToTable("IdentityUsers");

                // Each User can have many UserClaims
                b.HasMany(e => e.Claims)
                    .WithOne(e => e.User)
                    .HasForeignKey(uc => uc.UserId)
                    .IsRequired();

                // Each User can have many UserLogins
                b.HasMany(e => e.Logins)
                    .WithOne(e => e.User)
                    .HasForeignKey(ul => ul.UserId)
                    .IsRequired();

                // Each User can have many UserTokens
                b.HasMany(e => e.Tokens)
                    .WithOne(e => e.User)
                    .HasForeignKey(ut => ut.UserId)
                    .IsRequired();

                // Each User can have many entries in the UserRole join table
                b.HasMany(e => e.UserRoles)
                    .WithOne(e => e.User)
                    .HasForeignKey(ur => ur.UserId)

                    .IsRequired();
            });

            modelBuilder.Entity<Role>(b =>
            {

                // Map to Role Table
                b.ToTable("IdentityRoles");
                // Each Role can have many entries in the UserRole join table
                b.HasMany(e => e.UserRoles)
                    .WithOne(e => e.Role)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();

                // Each Role can have many associated RoleClaims
                b.HasMany(e => e.RoleClaims)
                    .WithOne(e => e.Role)
                    .HasForeignKey(rc => rc.RoleId)
                    .IsRequired();
            });

            modelBuilder.Entity<UserClaim>(b =>
            {
                b.HasKey(r => r.Id);
                b.ToTable("IdentityUserClaims");
            });

            modelBuilder.Entity<UserLogin>(b =>
            {
                b.ToTable("IdentityUserLogins");
            });

            modelBuilder.Entity<UserToken>(b =>
            {
                b.ToTable("IdentityUserTokens");
            });

            modelBuilder.Entity<RoleClaim>(b =>
            {
                b.ToTable("IdentityRoleClaims");
            });

            modelBuilder.Entity<UserRole>(b =>
            {
                b.ToTable("IdentityUserRoles");
            });
        }
    }

}
