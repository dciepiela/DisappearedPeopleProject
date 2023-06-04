using DisapeardPeople.MVC.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DisapeardPeople.MVC.Data
{
    public class AppDbContext:IdentityDbContext<UserModel>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<DisappearPerson> DisappearPerson { get; set; }


    }
}
