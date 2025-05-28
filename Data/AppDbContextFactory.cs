using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Date_Management_Project.Data
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<AppDbContext>();
            builder.UseSqlServer(
                "Data Source=SWIFT\\SQLEXPRESS;Database=DateMgmt;Trusted_Connection=True;TrustServerCertificate=True"
            );
            return new AppDbContext(builder.Options);
        }
    }
}
