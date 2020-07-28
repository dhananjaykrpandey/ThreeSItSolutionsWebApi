using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
namespace ThreeSItSolutionsWebApi.Models
{
    public class Db3SItSoultion:DbContext
    { 
        public DbSet<MContactUs> MContactUs { get; set; }
        public DbSet<MEnquiry> MEnquiry { get; set; }
        public Db3SItSoultion(DbContextOptions<Db3SItSoultion> options)
         : base(options)
        { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           //optionsBuilder.UseSqlServer(Configuration.GetConnectionString("ArnikaInfotechDBConnection")));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
       
    }
}
