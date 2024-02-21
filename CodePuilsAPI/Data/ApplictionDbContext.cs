using CodePuilsAPI.Models.Domin;
using Microsoft.EntityFrameworkCore;

namespace CodePuilsAPI.Data
{
    public class ApplictionDbContext : DbContext
    {
        public ApplictionDbContext(DbContextOptions<ApplictionDbContext> options) : base(options)
        {

        }
        public DbSet<BlogPost> blogPosts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<BlogImage> BlogImages { get; set; }
    }
}
