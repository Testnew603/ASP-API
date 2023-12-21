using ASP_API.Model.Public;
using ASP_API.Model.Student;
using Microsoft.EntityFrameworkCore;
namespace ASP_API
{
    public class Context: DbContext
    {
        public DbSet<StudentDetails> Students { get; set; }
        public DbSet<Domain> Domains { get; set; }

        public Context(DbContextOptions<Context> options): base(options) { }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Properties<Status>().HaveConversion<string>();            
        }
    }
}
