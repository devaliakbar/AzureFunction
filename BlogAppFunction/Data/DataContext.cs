using BlogAppFunction.Entities;
using Microsoft.EntityFrameworkCore;

namespace BlogAppFunction.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Blog> Blogs { get; set; }
    }
}

