using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UserSupportManagement.Models;

namespace UserSupportManagement.Data
{
    public class ApplicationDbContext : IdentityDbContext <ApplicationUser>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ApplicationDbContext(DbContextOptions options, IHttpContextAccessor httpContextAccessor)
            : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        // [01] Concern
        public DbSet<Concern> Concerns { get; set; }

        // [02] Vendor
        public DbSet<Vendor> Vendors { get; set; }

        // [03] ProblemType
        public DbSet<ProblemType> ProblemTypes { get; set; }

        // [04] StatusType
        public DbSet<StatusType> StatusTypes { get; set; }

        // [05] Problem
        public DbSet<Problem> Problems { get; set; }

        // [06] Problem
        public DbSet<Solution> Solutions { get; set; }

        // [07] Order
        public DbSet<Order> Orders { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.HasDefaultSchema("Identity");
            builder.Entity<ApplicationUser>(entity =>
            {
                entity.ToTable(name: "User");
            });
            builder.Entity<IdentityRole>(entity =>
            {
                entity.ToTable(name: "Role");
            });
            builder.Entity<IdentityUserRole<string>>(entity =>
            {
                entity.ToTable("UserRoles");
            });
            builder.Entity<IdentityUserClaim<string>>(entity =>
            {
                entity.ToTable("UserClaims");
            });
            builder.Entity<IdentityUserLogin<string>>(entity =>
            {
                entity.ToTable("UserLogins");
            });
            builder.Entity<IdentityRoleClaim<string>>(entity =>
            {
                entity.ToTable("RoleClaims");
            });
            builder.Entity<IdentityUserToken<string>>(entity =>
            {
                entity.ToTable("UserTokens");
            });
        }


        public override Task<int> SaveChangesAsync(
                                            bool acceptAllChangesOnSuccess,
                                            CancellationToken token = default)
        {
            AddTimestamps();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, token);
        }

        public override int SaveChanges()
        {
            AddTimestamps();
            return base.SaveChanges();
        }

        private void AddTimestamps()
        {
            var entities = ChangeTracker.Entries().Where(x => x.Entity is CreatedUpdated && (x.State == EntityState.Added || x.State == EntityState.Modified));

            //var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            //var currentUsername = !string.IsNullOrEmpty(userId)
            //   ? userId
            //    : "Anonymous";

            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            //var userName = _httpContextAccessor.HttpContext.User.Identity.Name;

            var currentUserId = !string.IsNullOrEmpty(userId) ? userId : "Anonymous";

            foreach (var entity in entities)
            {
                if (entity.State == EntityState.Added)
                {
                    ((CreatedUpdated)entity.Entity).CreatedDate = DateTime.Now;
                    ((CreatedUpdated)entity.Entity).CreatedBy = currentUserId;
                }
                else
                {
                    entity.Property("CreatedDate").IsModified = false;
                    entity.Property("CreatedBy").IsModified = false;
                }
             ((CreatedUpdated)entity.Entity).ModifiedDate = DateTime.Now;
                ((CreatedUpdated)entity.Entity).ModifiedBy = currentUserId;
            }
        }
    }
}
