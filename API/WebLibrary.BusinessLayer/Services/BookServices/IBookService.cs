using WebLibrary.Domain.Dtos;
using WebLibrary.Domain.Requests.Book;

namespace WebLibrary.BusinessLayer.Services.BookServices;

public interface IBookService
{
    Task<List<BookDto>> GetAllBooksAsync();
    
    Task<BookDto?> GetBookByIdAsync(Guid id);
    
    Task<BookDto?> GetBookByIsbnAsync(string isbn);
    
    Task<BookDto> CreateBookAsync(CreateBookRequest request);
    
    Task<bool> UpdateBookAsync(UpdateBookRequest request);

    Task<bool> DeleteBookAsync(Guid id);
}