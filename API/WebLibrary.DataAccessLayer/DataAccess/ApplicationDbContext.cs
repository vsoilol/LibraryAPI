using Microsoft.EntityFrameworkCore;
using WebLibrary.Domain.Entities;

namespace WebLibrary.DataAccessLayer.DataAccess;

internal sealed class ApplicationDbContext : DbContext
{
    public DbSet<User> Users { get; set; } = null!;

    public DbSet<Book> Books { get; set; } = null!;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
}