using Application.Contexts;
using Domain.Attributes;
using Domain.Entities.Blogs;
using Domain.Entities.Captchas;
using Domain.Entities.Contents;
using Domain.Entities.Portfolios;
using Domain.Entities.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Persistence.Configurations.Users;
using System.Data;

namespace Persistence.Context
{
    public class DataBaseContext(DbContextOptions<DataBaseContext> options) : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>(options), IDataBaseContext
    {
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<BlogCategory> BlogCategories { get; set; }
        public DbSet<Captcha> Captchas { get; set; }
        public DbSet<Content> Contents { get; set; }
        public DbSet<ApplicationRole> ApplicationRoles { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Portfolio> Portfolios { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            foreach (var entityType in builder.Model.GetEntityTypes())
            {
                if (entityType.ClrType.GetCustomAttributes(typeof(AuditableAttribute), true).Length > 0)
                {
                    builder.Entity(entityType.Name).Property<DateTime>("InsertTime").HasDefaultValue(DateTime.Now);
                    builder.Entity(entityType.Name).Property<DateTime>("UpdateTime").HasDefaultValue(DateTime.Now);
                    builder.Entity(entityType.Name).Property<DateTime?>("RemoveTime");
                    builder.Entity(entityType.Name).Property<bool>("IsRemove").HasDefaultValue(false);
                }
            }

            var assembly = typeof(UserConfigurations).Assembly;
            builder.ApplyConfigurationsFromAssembly(assembly);
            base.OnModelCreating(builder);
        }

        public override int SaveChanges()
        {
            var modifiedEntity = ChangeTracker.Entries()
                  .Where(x => x.State == EntityState.Modified ||
                  x.State == EntityState.Added ||
                  x.State == EntityState.Deleted);
            foreach (var item in modifiedEntity)
            {
                var entityType = item.Context.Model.FindEntityType(item.Entity.GetType());
                if (entityType != null)
                {
                    var inserted = entityType!.FindProperty("InsertTime");
                    var Updated = entityType.FindProperty("UpdateTime");
                    var Removed = entityType.FindProperty("RemoveTime");
                    var IsRemoved = entityType.FindProperty("IsRemove");

                    if (item.State == EntityState.Added && inserted != null)
                    {
                        item.Property("InsertTime").CurrentValue = DateTime.Now;
                    }

                    if (item.State == EntityState.Modified && Updated != null)
                    {
                        item.Property("UpdateTime").CurrentValue = DateTime.Now;
                    }

                    if (item.State == EntityState.Deleted && Removed != null && IsRemoved != null)
                    {
                        item.Property("RemoveTime").CurrentValue = DateTime.Now;
                        item.Property("IsRemove").CurrentValue = true;
                        item.State = EntityState.Modified;
                    }
                }
            }
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            var modifiedEntity = ChangeTracker.Entries()
               .Where(x => x.State == EntityState.Modified ||
               x.State == EntityState.Added ||
               x.State == EntityState.Deleted);
            foreach (var item in modifiedEntity)
            {
                var entityType = item.Context.Model.FindEntityType(item.Entity.GetType());
                if (entityType != null)
                {
                    var inserted = entityType.FindProperty("InsertTime");
                    var Updated = entityType.FindProperty("UpdateTime");
                    var Removed = entityType.FindProperty("RemoveTime");
                    var IsRemoved = entityType.FindProperty("IsRemove");

                    if (item.State == EntityState.Added && inserted != null)
                    {
                        item.Property("InsertTime").CurrentValue = DateTime.Now;
                    }

                    if (item.State == EntityState.Modified && Updated != null)
                    {
                        item.Property("UpdateTime").CurrentValue = DateTime.Now;
                    }

                    if (item.State == EntityState.Deleted && Removed != null && IsRemoved != null)
                    {
                        item.Property("RemoveTime").CurrentValue = DateTime.Now;
                        item.Property("IsRemove").CurrentValue = true;
                        item.State = EntityState.Modified;
                    }
                }
            }

            return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);

        }

        public void EnableLazyLoading()
        {
            base.ChangeTracker.LazyLoadingEnabled = true;
        }

        public void DisableLazyLoading()
        {
            base.ChangeTracker.LazyLoadingEnabled = false;
        }

        public IEnumerable<T> ExecuteSqlQuery<T>(string sqlQuery, params object[] parameters) where T : class
        {
            return [.. Set<T>().FromSqlRaw(sqlQuery, parameters)];
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
        }
    }
}
