using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DataAccess.Data
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            //string connectionString = "Server=localhost;port=3306;Database=LibertyVilla;Uid=root;Pwd=Adebayo58641999"; 
            string connectionString = "Server = (localdb)\\MSSQLLocalDB;Initial Catalog=LibertyVilla;Integrated Security=True;Persist Security Info=true;Encrypt=False;Connection Timeout=1500;"; 

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            //optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
            optionsBuilder.UseSqlServer(connectionString,
           sqlServerOptions => sqlServerOptions.CommandTimeout(60))
           //.UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole()))
           .EnableSensitiveDataLogging(); // This can be removed in production

            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }


}
