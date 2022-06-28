using BookProject.Models;
using Microsoft.EntityFrameworkCore;

namespace BookProject.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Book> Books { get; set; }
        public DbSet<BookCover> BookCovers { get; set; }
        public DbSet<BookWriter> BookWriters { get; set; }
 
    }
}
