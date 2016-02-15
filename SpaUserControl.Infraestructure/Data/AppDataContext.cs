using System.Data.Entity;
using SpaUserControl.Domain.Models;
using SpaUserControl.Infraestructure.Data.Map;

namespace SpaUserControl.Infraestructure.Data
{
    public class AppDataContext: DbContext
    {
        public AppDataContext()
            : base("AppConnectionString")
        {
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Database.SetInitializer<AppDataContext>(null);
            modelBuilder.Configurations.Add(new UserMap());
        }
    }
}