using Connect_project_with_DB.Model;
using Microsoft.EntityFrameworkCore;

namespace Connect_project_with_DB.Data
{
    // Add - Migration Process---- Code first approach
/*  1. First create DbContext class and inherite it from base class DbContext.
 *     Set constructor and dataset 
 *  2. pass the connection string in appsettings.json file which stablise connection to Db
 *  3. invoke service in program.cs file so it make connection to the database by GetConnectionString.
 *  4. Now inherite the connection form services in program.cs file to Dbcontext class so it make the proper connection to Db 
 *  5. inject dependancy injection of Dbcontext in ApiController class. and start making Api end point. 
 */
    public class ApplicationDbContext : DbContext   // step 1
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { } // step 4
        public DbSet<Employee> Employees { get; set; }
    }
}
