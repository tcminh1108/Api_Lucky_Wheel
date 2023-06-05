using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Domain.Models
{
    public class AppDBContext : DbContext
    {
        public AppDBContext()
        {
            
        }
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<CategoryVoucher> CategoryVouchers { get; set; }
        public DbSet<Voucher> Vouchers { get; set; }
        public DbSet<VipPhone> VipPhones { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var fullPath = Directory.GetCurrentDirectory();
                string solutiondir = Directory.GetParent(
                        Directory.GetCurrentDirectory()).Parent.FullName;
                IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(solutiondir + "\\VongQuay\\VongQuay")
                //.SetBasePath(fullPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();
                optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            }

        }
    }
}
