using AnnonsWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace AnnonsWebApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Ad> AdsInfo { get; set; }
    }
}
