using Mapster;
using WebLibrary.BusinessLayer.Validation.Services;
using WebLibrary.DataAccessLayer.Repositories.BookRepositories;
using WebLibrary.Domain.Dtos;
using WebLibrary.Domain.Entities;
using WebLibrary.Domain.Requests.Book;

namespace WebLibrary.BusinessLayer.Services.BookServices;

internal class BookService : IBookService
{
    private readonly IBookRepository _bookRepository;
    private readonly IValidationService _validationService;

    public BookService(IBookRepository bookRepository, IValidationService validationService)
    {
        _bookRepository = bookRepository;
        _validationService = validationService;
    }

    public async Task<List<BookDto>> GetAllBooksAsync()
    {
        var bookEntities = await _bookRepository.GetAsync();

        var mappedBooks = bookEntities.Adapt<List<BookDto>>();

        return mappedBooks;
    }

    public async Task<BookDto?> GetBookByIdAsync(Guid id)
    {
        var bookEntity = await _bookRepository.GetByIdAsync(id);

        var mappedBook = bookEntity?.Adapt<BookDto>();

        return mappedBook;
    }

    public async Task<BookDto?> GetBookByIsbnAsync(string isbn)
    {
        var bookEntity = await _bookRepository.GetByIsbnAsync(isbn);

        var mappedBook = bookEntity?.Adapt<BookDto>();

        return mappedBook;
    }

    public async Task<BookDto> CreateBookAsync(CreateBookRequest request)
    {
        await _validationService.ValidateAsync(request);

        var bookEntity = request.Adapt<Book>();

        var createdBookEntity = await _bookRepository.InsertAsync(bookEntity);

        return createdBookEntity.Adapt<BookDto>();
    }

    public async Task<bool> UpdateBookAsync(UpdateBookRequest request)
    {
        await _validationService.ValidateAsync(request);

        var bookEntity = await _bookRepository.GetByIdAsync(request.Id);

        if (bookEntity is null)
        {
            return false;
        }

        request.Adapt(bookEntity);

        await _bookRepository.UpdateAsync(bookEntity);
        return true;
    }

    public Task<bool> DeleteBookAsync(Guid id)
    {
        return _bookRepository.DeleteAsync(new Book { Id = id });
    }
}