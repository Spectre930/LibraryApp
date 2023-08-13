using LibraryApp.Models.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.DataAccess;

public class LibraryContext : DbContext
{
    public LibraryContext(DbContextOptions<LibraryContext> options) : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AuthorBook>().HasKey(ab => new { ab.AuthorId, ab.BookId });

        modelBuilder.Entity<Clients>()
             .HasIndex(c => c.Email)
             .IsUnique();

        modelBuilder.Entity<Employees>()
           .HasIndex(c => c.Email)
           .IsUnique();
    }

    public DbSet<Books> Books { get; set; }
    public DbSet<Authors> Authors { get; set; }
    public DbSet<Borrow> Borrows { get; set; }
    public DbSet<Clients> Clients { get; set; }
    public DbSet<Purchases> Purchases { get; set; }
    public DbSet<AuthorBook> AuthorBooks { get; set; }
    public DbSet<Genres> Genres { get; set; }
    public DbSet<Employees> Employees { get; set; }
    public DbSet<Roles> Roles { get; set; }

}
