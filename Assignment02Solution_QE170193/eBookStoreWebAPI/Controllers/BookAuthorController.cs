using AutoMapper;
using BusinessObject.Models;
using DataAccess.Services.Interface;
using eBookStoreWebAPI.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;

namespace eBookStoreWebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BookAuthorController : ControllerBase
    {
        private readonly IBookAuthorService bookAuthorService;
        private readonly IMapper mapper;

        public BookAuthorController(IBookAuthorService bookAuthorService, IMapper mapper)
        {
            this.bookAuthorService = bookAuthorService;
            this.mapper = mapper;
        }

        [HttpGet("{bookId}")]
        [EnableQuery]
        public async Task<IActionResult> GetAllByBookId(int bookId)
        {
            try
            {
                var list = await bookAuthorService.GetAllByBookIdAsync(bookId);
                var listVM = mapper.Map<IEnumerable<BookAuthorVM>>(list);
                return Ok(listVM);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Status = "Error", Message = ex.Message });
            }
        }

        [HttpGet]
        [EnableQuery]
        public async Task<IActionResult> GetAllBookAuthors()
        {
            try
            {
                var list = await bookAuthorService.GetAllAsync();
                var listVM = mapper.Map<IEnumerable<BookAuthorVM>>(list);
                return Ok(listVM);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Status = "Error", Message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddBookAuthor([FromBody] BookAuthorVM model)
        {
            try
            {
                var bookAuthor = mapper.Map<BookAuthor>(model);
                bookAuthor.royality_percentage = 0;
                bookAuthor.author_order = "";

                var newBookAuthor = await bookAuthorService.AddAsync(bookAuthor);
                var newBookAuthorVM = mapper.Map<BookAuthorVM>(newBookAuthor);

                return CreatedAtAction(nameof(GetAllByBookId), new { bookId = newBookAuthor.book_id }, newBookAuthorVM);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Status = "Error", Message = ex.Message });
            }
        }

        [HttpDelete("{bookId}/{authorId}")]
        public async Task<IActionResult> DeleteBookAuthor(int bookId, int authorId)
        {
            try
            {
                var deleted = await bookAuthorService.DeleteAsync(bookId, authorId);
                if (!deleted)
                    return NotFound(new { Message = "BookAuthor not found" });

                return Ok(new { Message = "Delete Success" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Status = "Error", Message = ex.Message });
            }
        }
    }
}
