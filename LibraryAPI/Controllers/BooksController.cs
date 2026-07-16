using LibraryAPI.DTOs;
using LibraryAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI.Controllers
{
    [ApiController]
    [Route("api/books")]
    public class BooksController : ControllerBase
    {
        private readonly BookService _bookService;

        public BooksController(BookService bookService) => _bookService = bookService;

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll([FromQuery] string? search, [FromQuery] string? branch)
        {
            var books = await _bookService.GetAllAsync(search, branch);
            return Ok(books);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetById(int id)
        {
            var book = await _bookService.GetByIdAsync(id);
            return book == null ? NotFound() : Ok(book);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] CreateBookDto dto)
        {
            var book = await _bookService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = book.Id }, book);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateBookDto dto)
        {
            var success = await _bookService.UpdateAsync(id, dto);
            return success ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _bookService.DeleteAsync(id);
            return success ? NoContent() : NotFound();
        }
    }
}
