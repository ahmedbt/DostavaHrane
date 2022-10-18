using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using DostavaHrane.Models;

namespace DostavaHrane.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<DostavaHrane.Models.Restoran> Restoran { get; set; }
        public DbSet<DostavaHrane.Models.Jelo> Jelo { get; set; }
    }
}