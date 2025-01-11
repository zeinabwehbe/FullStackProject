using Microsoft.AspNetCore.Mvc;
using serverside.Models.Domain;
using serverside.Repository;
using AutoMapper;
using serverside.Models.DTOs.Tags;

namespace serverside.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagsController : ControllerBase
    {
        private readonly ITagRepository _tagRepository;
        private readonly IMapper _mapper;

        public TagsController(ITagRepository tagRepository, IMapper mapper)
        {
            _tagRepository = tagRepository;
            _mapper = mapper;
        }

        // GET ALL TAGS
        // GET: api/Tags
        [HttpGet]
        public async Task<IActionResult> GetAllTags()
        {
            var tagsDomain = await _tagRepository.GetAllTagsAsync();
            var tagsDto = _mapper.Map<List<TagDto>>(tagsDomain);
            return Ok(tagsDto);
        }

        // GET TAG BY ID
        // GET: api/Tags/{id}
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetTagById([FromRoute] int id)
        {
            var tagDomain = await _tagRepository.GetTagByIdAsync(id);

            if (tagDomain == null)
            {
                return NotFound($"Tag with ID {id} not found.");
            }

            var tagDto = _mapper.Map<TagDto>(tagDomain);
            return Ok(tagDto);
        }

        // GET TAGS BY NAME
        // GET: api/Tags/search?name={name}
        [HttpGet("search")]
        public async Task<IActionResult> GetTagsByName([FromQuery] string name)
        {
            var tagsDomain = await _tagRepository.GetTagsByNameAsync(name);
            var tagsDto = _mapper.Map<List<TagDto>>(tagsDomain);
            return Ok(tagsDto);
        }

        // CREATE A TAG
        // POST: api/Tags
        [HttpPost]
        public async Task<IActionResult> CreateTag([FromBody] AddTagRequestDto addTagRequestDto)
        {
            var tagDomain = _mapper.Map<Tags>(addTagRequestDto);

            tagDomain = await _tagRepository.CreateAsync(tagDomain);

            var tagDto = _mapper.Map<TagDto>(tagDomain);

            return CreatedAtAction(nameof(GetTagById), new { id = tagDto.Id }, tagDto);
        }

        // UPDATE A TAG
        // PUT: api/Tags/{id}
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateTag([FromRoute] int id, [FromBody] UpdateTagRequestDto updateTagRequestDto)
        {
            var tagDomain = _mapper.Map<Tags>(updateTagRequestDto);

            var updatedTag = await _tagRepository.UpdateAsync(id, tagDomain);

            if (updatedTag == null)
            {
                return NotFound($"Tag with ID {id} not found.");
            }

            var tagDto = _mapper.Map<TagDto>(updatedTag);

            return Ok(new
            {
                Message = "Tag updated successfully.",
                UpdatedTag = tagDto
            });
        }

        // DELETE A TAG
        // DELETE: api/Tags/{id}
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteTag([FromRoute] int id)
        {
            var deletedTag = await _tagRepository.DeleteAsync(id);

            if (deletedTag == null)
            {
                return NotFound($"Tag with ID {id} not found.");
            }

            var tagDto = _mapper.Map<TagDto>(deletedTag);

            return Ok(new
            {
                Message = "Tag deleted successfully.",
                DeletedTag = tagDto
            });
        }
    }
}
