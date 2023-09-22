using Authorization.Common.Database.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Authorization.Common.Database.Persistence
{
    public class AppDbContext : DbContext
    {
        public const string DEFAULT_SCHEMA = "App";
        public DbSet<UserEntity> Users { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
         : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
    }
}
