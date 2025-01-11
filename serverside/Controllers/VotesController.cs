using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using serverside.Models.Domain;
using serverside.Models.DTOs.Votes;
using serverside.Repository;

namespace serverside.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VotesController : ControllerBase
    {
        private readonly IVoteRepository _votesRepository;
        private readonly IMapper _mapper;

        public VotesController(IVoteRepository votesRepository, IMapper mapper)
        {
            _votesRepository = votesRepository;
            _mapper = mapper;
        }

        // GET ALL VOTES
        [HttpGet]
        public async Task<IActionResult> GetAllVotes()
        {
            var votesDomain = await _votesRepository.GetAllVotesAsync();

            // Map Domain Models to DTOs
            var votesDto = _mapper.Map<List<VoteDto>>(votesDomain);

            return Ok(votesDto);
        }

        // GET VOTE BY ID
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetVoteById([FromRoute] int id)
        {
            var voteDomain = await _votesRepository.GetVoteByIdAsync(id);

            if (voteDomain == null)
            {
                return NotFound();
            }

            // Map Domain Models to DTOs
            var voteDto = _mapper.Map<VoteDto>(voteDomain);

            return Ok(voteDto);
        }

        // POST to create a new vote
        [HttpPost]
        [ValidateModel] // Custom validation attribute if you have one
        public async Task<IActionResult> Create([FromBody] AddVoteRequestDto addVoteRequestDto)
        {
            // Map DTO to domain model
            var voteDomainModel = _mapper.Map<Votes>(addVoteRequestDto);

            // Use domain model to create a vote
            voteDomainModel = await _votesRepository.CreateAsync(voteDomainModel);

            // Map Domain Models to DTOs
            var voteDto = _mapper.Map<VoteDto>(voteDomainModel);

            return CreatedAtAction(nameof(GetVoteById), new { id = voteDomainModel.Id }, voteDto);
        }

        // PUT to update a vote
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateVoteRequestDto updateVoteRequestDto)
        {
            if (ModelState.IsValid)
            {
                // Map DTO to domain model
                var voteDomainModel = _mapper.Map<Votes>(updateVoteRequestDto);

                // Check if vote exists
                voteDomainModel = await _votesRepository.UpdateAsync(id, voteDomainModel);

                if (voteDomainModel == null)
                {
                    return NotFound($"Vote with ID {id} not found.");
                }

                // Map Domain Models to DTOs
                var voteDto = _mapper.Map<VoteDto>(voteDomainModel);

                return Ok(new
                {
                    Message = "Vote updated successfully.",
                    UpdatedVote = voteDto
                });
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        // DELETE VOTE
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var voteDomainModel = await _votesRepository.DeleteAsync(id);

            if (voteDomainModel == null)
            {
                return NotFound($"Vote with ID {id} not found.");
            }

            // Map Domain Models to DTOs
            var voteDto = _mapper.Map<VoteDto>(voteDomainModel);

            return Ok(voteDto);
        }

        // GET VOTES BY POST ID
        [HttpGet("PostId/{postId}")]
        public async Task<IActionResult> GetVotesByPostId([FromRoute] int postId)
        {
            var votesDomain = await _votesRepository.GetVotesByPostIdAsync(postId);

            if (votesDomain == null || !votesDomain.Any())
            {
                return NotFound();
            }

            // Map Domain Models to DTOs
            var votesDto = _mapper.Map<List<VoteDto>>(votesDomain);

            return Ok(votesDto);
        }

        // GET VOTE COUNT BY USER ID
        [HttpGet("CountVotesByUserId/{userId}")]
        public async Task<IActionResult> CountVotesByUserId([FromRoute] int userId)
        {
            var count = await _votesRepository.CountVotesByUserIdAsync(userId);
            return Ok(count);
        }
    }
}
