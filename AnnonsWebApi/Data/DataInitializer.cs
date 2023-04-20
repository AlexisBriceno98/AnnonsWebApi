using AnnonsWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace AnnonsWebApi.Data
{
    public class DataInitializer
    {
        private readonly ApplicationDbContext _dbContext;

        public DataInitializer(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void MigrateData()
        {
            _dbContext.Database.Migrate();
            SeedData();
            _dbContext.SaveChanges();
        }
        private void SeedData()
        {
            if (!_dbContext.AdsInfo
        .Any(a => a.Title == "Tech Heaven"))
            {
                _dbContext.Add(new Ad
                {
                    Title = "Tech Heaven",
                    Description = "Discover the latest gadgets and accessories at unbeatable prices, with free shipping on all orders.",
                    TargetUrl = "https://example.com/shop",
                    CreatedAt = DateTime.Now,
                });
            }
        }

    }
}
