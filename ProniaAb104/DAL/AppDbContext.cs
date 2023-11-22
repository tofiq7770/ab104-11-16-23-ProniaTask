using Microsoft.EntityFrameworkCore;
using ProniaAb104.Models;

namespace ProniaAb104.DAL
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Slide> Slides { get; set; }


    }
}
