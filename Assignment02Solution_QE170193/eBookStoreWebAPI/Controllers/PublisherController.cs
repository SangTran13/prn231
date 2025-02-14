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
    public class PublisherController : ControllerBase
    {
        private readonly IPublisherService _publisherService;
        private readonly IMapper _mapper;

        public PublisherController(IPublisherService publisherService, IMapper mapper)
        {
            _publisherService = publisherService;
            _mapper = mapper;
        }

        [HttpGet]
        [EnableQuery]
        public async Task<ActionResult<IEnumerable<Publisher>>> Get()
        {
            var list = await _publisherService.GetAllAsync();
            return Ok(list);
        }


        [HttpGet("GetAllPublisher")]
        public async Task<IActionResult> GetAllPublisher()
        {
            try
            {
                var list = await _publisherService.GetAllAsync(); 
                return Ok(list);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Status = "Error",
                    Message = ex.Message
                });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPublisherById(int id)
        {
            var publisher = await _publisherService.GetByIdAsync(id);
            if (publisher == null)
            {
                return NotFound(new { Message = "Publisher not found" });
            }
            return Ok(publisher);
        }

        [HttpPost]
        public async Task<IActionResult> AddPublisher([FromBody] PublisherVM model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var publisher = _mapper.Map<Publisher>(model);
            var addedPublisher = await _publisherService.AddAsync(publisher);

            return CreatedAtAction(nameof(GetPublisherById), new { id = addedPublisher.pub_id }, addedPublisher);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePublisher(int id, [FromBody] PublisherVM model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var publisherExists = await _publisherService.GetByIdAsync(id);
            if (publisherExists == null)
            {
                return NotFound(new { Message = "Publisher not found" });
            }

            var publisher = _mapper.Map<Publisher>(model);
            var updateSuccess = await _publisherService.UpdateAsync(id, publisher);

            if (!updateSuccess)
            {
                return StatusCode(500, new { Message = "Failed to update publisher" });
            }

            return Ok(new { Message = "Update successful" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePublisher(int id)
        {
            var publisherExists = await _publisherService.GetByIdAsync(id);
            if (publisherExists == null)
            {
                return NotFound(new { Message = "Publisher not found" });
            }

            var deleteSuccess = await _publisherService.DeleteAsync(id);
            if (!deleteSuccess)
            {
                return StatusCode(500, new { Message = "Failed to delete publisher" });
            }

            return Ok(new { Message = "Delete successful" });
        }
    }
}
