using WebLibrary.Domain.Entities;

namespace WebLibrary.DataAccessLayer.Repositories.BookRepositories;

public interface IBookRepository : IRepository<Book>
{
    Task<Book?> GetByIsbnAsync(string isbn);
}