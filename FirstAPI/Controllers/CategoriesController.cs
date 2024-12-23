using FirstAPI.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FirstAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
     
        private readonly IRepository _repository;

        public CategoriesController(IRepository repository)
        {
            _repository = repository;
        }
        [HttpGet]
        public async Task<IActionResult> Get(int page=1, int take=3)
        {
            var categories = await _repository.GetAll().ToListAsync();

            return Ok(categories);
            //return StatusCode(StatusCodes.Status200OK,categories);
        }
        [HttpGet]

        [Route("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (id < 1) return BadRequest();

            Category category = await _repository.GetByIdAsync(id);

            if (category == null) return NotFound();

            return Ok(category);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromForm]CreateCategoryDTO categoryDto)
        {
            Category category = new Category
            {
                Name = categoryDto.Name
            };
            await _repository.AddAsync(category);
            await _repository.SaveChangesAsync();
            //return Created();
            return StatusCode(StatusCodes.Status201Created);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id < 1) return BadRequest();

            Category category = await _repository.GetByIdAsync(id);
            if (category == null) return NotFound();

           _repository.Delete(category);

           await _repository.SaveChangesAsync();


            return NoContent();
        }
        [HttpPut]
        public async Task<IActionResult> Update(int id, string name)
        {
            if (id < 1) return BadRequest();
            Category category = await _repository.GetByIdAsync(id);
            if (category == null) return NotFound();

            category.Name = name;

            _repository.Update(category);

            await _repository.SaveChangesAsync();
            return NoContent();
        }
        
    }
}
