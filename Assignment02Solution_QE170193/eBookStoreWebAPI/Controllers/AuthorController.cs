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
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService authorService;
        private readonly IMapper mapper;

        public AuthorController(IAuthorService authorService, IMapper mapper)
        {
            this.authorService = authorService;
            this.mapper = mapper;
        }

        [HttpGet]
        [EnableQuery] 
        public ActionResult<IQueryable<Author>> Get()
        {
            try
            {
                var authors = authorService.GetAll(); 
                return Ok(authors);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Status = "Error", Message = ex.Message });
            }
        }


        [HttpGet]
        public async Task<IActionResult> GetAllAuthor()
        {
            try
            {
                var authors = await authorService.GetAllAsync();
                var authorVMs = mapper.Map<IEnumerable<AuthorVM>>(authors);
                return Ok(authorVMs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Status = "Error", Message = ex.Message });
            }
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetAuthorById(int id)
        {
            try
            {
                var author = await authorService.GetAsync(id);
                if (author == null) return NotFound(new { Message = "Author Not Found" });

                var authorVM = mapper.Map<AuthorVM>(author);
                return Ok(authorVM);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Status = "Error", Message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddAuthor([FromBody] AuthorVM model)
        {
            try
            {
                var author = mapper.Map<Author>(model);
                var newAuthor = await authorService.AddAsync(author);
                var authorVM = mapper.Map<AuthorVM>(newAuthor);
                return CreatedAtAction(nameof(GetAuthorById), new { id = newAuthor.author_id }, authorVM);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Status = "Error", Message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAuthor(int id, [FromBody] AuthorVM model)
        {
            try
            {
                var authorDB = await authorService.GetAsync(id);
                if (authorDB == null) return NotFound(new { Message = "Author Not Found" });

                var author = mapper.Map<Author>(model);
                var updated = await authorService.UpdateAsync(id, author);
                if (!updated) return StatusCode(500, new { Message = "Update Fail" });

                return Ok(new { Message = "Update Success" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Status = "Error", Message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            try
            {
                var authorDB = await authorService.GetAsync(id);
                if (authorDB == null) return NotFound(new { Message = "Author Not Found" });

                var deleted = await authorService.DeleteAsync(id);
                if (!deleted) return StatusCode(500, new { Message = "Delete Fail" });

                return Ok(new { Message = "Delete Success" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Status = "Error", Message = ex.Message });
            }
        }
    }
}
