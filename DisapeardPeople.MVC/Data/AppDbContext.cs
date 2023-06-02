using DisapeardPeople.MVC.Models;
using Microsoft.EntityFrameworkCore;

namespace DisapeardPeople.MVC.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<DisappearPerson> DisappearPerson { get; set; }


    }
}
