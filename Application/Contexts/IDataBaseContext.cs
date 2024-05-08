using Domain.Entities.Blogs;
using Domain.Entities.Captchas;
using Domain.Entities.Contents;
using Domain.Entities.Portfolios;
using Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;

namespace Application.Contexts
{
    public interface IDataBaseContext
    {
        DbSet<Blog> Blogs { get; set; }
        DbSet<BlogCategory> BlogCategories { get; set; }
        DbSet<Captcha> Captchas { get; set; }
        DbSet<Content> Contents { get; set; }
        DbSet<ApplicationRole> ApplicationRoles { get; set; }
        DbSet<ApplicationUser> ApplicationUsers { get; set; }
        DbSet<Portfolio> Portfolios { get; set; }

        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        void EnableLazyLoading();
        void DisableLazyLoading();
        IEnumerable<T> ExecuteSqlQuery<T>(string sqlQuery, params object[] parameters) where T : class;
    }
}