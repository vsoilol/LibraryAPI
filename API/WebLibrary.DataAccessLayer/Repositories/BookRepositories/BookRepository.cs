using Microsoft.EntityFrameworkCore;
using WebLibrary.DataAccessLayer.DataAccess;
using WebLibrary.Domain.Entities;

namespace WebLibrary.DataAccessLayer.Repositories.BookRepositories;

internal class BookRepository : GenericRepository<Book>, IBookRepository
{
    public BookRepository(ApplicationDbContext context) : base(context)
    {
    }

    public Task<Book?> GetByIsbnAsync(string isbn)
    {
        return DbSet.AsNoTracking().FirstOrDefaultAsync(_ => _.Isbn == isbn);
    }
}