using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebLibrary.BusinessLayer.Services.BookServices;
using WebLibrary.Domain.Entities;
using WebLibrary.Domain.Requests.Book;

namespace WebLibrary.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookController : ControllerBase
{
    private readonly IBookService _bookService;

    public BookController(IBookService bookService)
    {
        _bookService = bookService;
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<ActionResult<List<Book>>> GetAllBooksAsync()
    {
        var books = await _bookService.GetAllBooksAsync();
        return Ok(books);
    }

    [AllowAnonymous]
    [HttpGet("{id}")]
    public async Task<ActionResult<Book>> GetBookByIdAsync([FromRoute] Guid id)
    {
        var book = await _bookService.GetBookByIdAsync(id);
        return book is not null ? Ok(book) : NoContent();
    }
    
    [AllowAnonymous]
    [HttpGet("byISBN/{isbn}")]
    public async Task<ActionResult<Book?>> GetBookByIsbnAsync([FromRoute] string isbn)
    {
        var book = await _bookService.GetBookByIsbnAsync(isbn);
        return book is not null ? Ok(book) : NoContent();
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult> AddBookAsync([FromBody] CreateBookRequest request)
    {
        var createdBook = await _bookService.CreateBookAsync(request);
        return Ok(createdBook);
    }

    [Authorize]
    [HttpPut]
    public async Task<ActionResult> UpdateBookAsync([FromBody] UpdateBookRequest request)
    {
        var isUpdate = await _bookService.UpdateBookAsync(request);
        return isUpdate ? Ok() : BadRequest();
    }
    
    [Authorize]
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteBookAsync([FromRoute] Guid id)
    {
        var isDelete = await _bookService.DeleteBookAsync(id);
        return isDelete ? Ok() : BadRequest();
    }
}