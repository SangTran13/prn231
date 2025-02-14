using AutoMapper;
using BusinessObject.Models;
using DataAccess.Services.Interface;
using eBookStoreWebAPI.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;

namespace eBookStoreWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;
        private readonly IMapper _mapper;

        public BookController(IBookService bookService, IMapper mapper)
        {
            _bookService = bookService ?? throw new ArgumentNullException(nameof(bookService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        [EnableQuery]
        public async Task<ActionResult<IEnumerable<Book>>> Get()
        {
            var books = await _bookService.GetAllAsync();
            return Ok(books);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookById(int id)
        {
            var book = await _bookService.GetByIdAsync(id);
            if (book == null)
            {
                return NotFound(new { Message = "Book not found" });
            }
            return Ok(book);
        }

        [HttpPost]
        public async Task<IActionResult> AddBook([FromBody] BookVM model)
        {
            if (model == null)
            {
                return BadRequest(new { Message = "Invalid book data" });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (model.Price < 0 || model.Royalty < 0 || model.YtdSales < 0)
            {
                return BadRequest(new { Message = "Price, Royalty, and YtdSales cannot be negative." });
            }

            var book = _mapper.Map<Book>(model);

            try
            {
                var addedBook = await _bookService.AddAsync(book);

                if (addedBook == null)
                {
                    return StatusCode(500, new { Message = "Failed to add book." });
                }

                return CreatedAtAction(nameof(GetBookById), new { id = addedBook.book_id }, addedBook);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while adding the book.", Error = ex.Message });
            }
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(int id, [FromBody] BookVM model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var book = _mapper.Map<Book>(model);
            var updated = await _bookService.UpdateAsync(id, book);
            if (!updated)
            {
                return NotFound(new { Message = "Book not found" });
            }
            return Ok(new { Message = "Update Successful" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var deleted = await _bookService.DeleteAsync(id);
            if (!deleted)
            {
                return NotFound(new { Message = "Book not found" });
            }
            return Ok(new { Message = "Delete Successful" });
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchBooks([FromQuery] string query)
        {
            var books = await _bookService.SearchAsync(query);
            return Ok(books);
        }
    }
}