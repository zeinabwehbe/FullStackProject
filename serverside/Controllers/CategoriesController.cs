using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using serverside.Models.Domain;
using serverside.Models.DTOs;
using serverside.Models.DTOs.Categories;
using serverside.Repository;

namespace serverside.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoriesController(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        // GET ALL CATEGORIES
        [HttpGet]
        public async Task<IActionResult> GetAllCategories( )
        {
            var categoriesDomain = await _categoryRepository.GetAllCategoriesAsync( );

            // Map Domain Models to DTOs
            var categoriesDto = _mapper.Map<List<CategoryDto>>(categoriesDomain);

            return Ok(categoriesDto);
        }

        // GET CATEGORY BY ID
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetCategoryById([FromRoute] int id)
        {
            var categoryDomain = await _categoryRepository.GetCategoryByIdAsync(id);

            if (categoryDomain == null)
            {
                return NotFound();
            }

            var categoryDto = _mapper.Map<CategoryDto>(categoryDomain);

            return Ok(categoryDto);
        }

        // CREATE CATEGORY
        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody] AddCategoryRequestDto addCategoryRequestDto)
        {
            var categoryDomainModel = _mapper.Map<Categories>(addCategoryRequestDto);

            categoryDomainModel = await _categoryRepository.CreateAsync(categoryDomainModel);

            var categoryDto = _mapper.Map<CategoryDto>(categoryDomainModel);

            return CreatedAtAction(nameof(GetCategoryById), new { id = categoryDomainModel.Id }, categoryDto);
        }

        // UPDATE CATEGORY
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateCategoryRequestDto updateCategoryRequestDto)
        {
            if (ModelState.IsValid)
            {
                var categoryDomainModel = _mapper.Map<Categories>(updateCategoryRequestDto);

                categoryDomainModel = await _categoryRepository.UpdateAsync(id, categoryDomainModel);

                if (categoryDomainModel == null)
                {
                    return NotFound($"Category with ID {id} not found.");
                }

                var categoryDto = _mapper.Map<CategoryDto>(categoryDomainModel);

                return Ok(new
                {
                    Message = "Category updated successfully.",
                    UpdatedCategory = categoryDto
                });
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        // DELETE CATEGORY
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var categoryDomainModel = await _categoryRepository.DeleteAsync(id);

            if (categoryDomainModel == null)
            {
                return NotFound($"Category with ID {id} not found.");
            }

            var categoryDto = _mapper.Map<CategoryDto>(categoryDomainModel);

            return Ok(categoryDto);
        }
    }
}
