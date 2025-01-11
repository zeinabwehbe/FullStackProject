using Microsoft.AspNetCore.Mvc;
using serverside.Models.Domain;
using serverside.Repository;
using AutoMapper;
using serverside.Models.DTOs.Comments;

namespace serverside.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IMapper _mapper;

        public CommentsController(ICommentRepository commentRepository, IMapper mapper)
        {
            _commentRepository = commentRepository;
            _mapper = mapper;
        }

        // GET ALL COMMENTS
        // GET: api/Comments
        [HttpGet]
        public async Task<IActionResult> GetAllComments()
        {
            var comments = await _commentRepository.GetAllCommentsAsync();
            var commentsDto = _mapper.Map<List<CommentDto>>(comments);
            return Ok(commentsDto);
        }

        // GET COMMENT BY ID
        // GET: api/Comments/{id}
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetCommentById([FromRoute] int id)
        {
            var comment = await _commentRepository.GetCommentByIdAsync(id);
            if (comment == null)
            {
                return NotFound($"Comment with ID {id} not found.");
            }

            var commentDto = _mapper.Map<CommentDto>(comment);
            return Ok(commentDto);
        }

        // GET COMMENTS BY USER ID
        // GET: api/Comments/user/{userId}
        [HttpGet("user/{userId:int}")]
        public async Task<IActionResult> GetCommentsByUserId([FromRoute] int userId)
        {
            var comments = await _commentRepository.GetCommentsByUserIdAsync(userId);
            var commentsDto = _mapper.Map<List<CommentDto>>(comments);
            return Ok(commentsDto);
        }

        // GET COMMENTS BY POST ID
        // GET: api/Comments/post/{postId}
        [HttpGet("post/{postId:int}")]
        public async Task<IActionResult> GetCommentsByPostId([FromRoute] int postId)
        {
            var comments = await _commentRepository.GetCommentsByPostIdAsync(postId);
            var commentsDto = _mapper.Map<List<CommentDto>>(comments);
            return Ok(commentsDto);
        }

        // CREATE COMMENT
        // POST: api/Comments
        [HttpPost]
        public async Task<IActionResult> CreateComment([FromBody] AddCommentRequestDto addCommentRequestDto)
        {
            var commentDomain = _mapper.Map<Comments>(addCommentRequestDto);

            commentDomain = await _commentRepository.CreateAsync(commentDomain);

            var commentDto = _mapper.Map<CommentDto>(commentDomain);
            return CreatedAtAction(nameof(GetCommentById), new { id = commentDto.Id }, commentDto);
        }

        // UPDATE COMMENT
        // PUT: api/Comments/{id}
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateComment([FromRoute] int id, [FromBody] UpdateCommentRequestDto updateCommentRequestDto)
        {
            var commentDomain = _mapper.Map<Comments>(updateCommentRequestDto);

            var updatedComment = await _commentRepository.UpdateAsync(id, commentDomain);
            if (updatedComment == null)
            {
                return NotFound($"Comment with ID {id} not found.");
            }

            var commentDto = _mapper.Map<CommentDto>(updatedComment);
            return Ok(new
            {
                Message = "Comment updated successfully.",
                UpdatedComment = commentDto
            });
        }

        // DELETE COMMENT
        // DELETE: api/Comments/{id}
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteComment([FromRoute] int id)
        {
            var deletedComment = await _commentRepository.DeleteAsync(id);
            if (deletedComment == null)
            {
                return NotFound($"Comment with ID {id} not found.");
            }

            var commentDto = _mapper.Map<CommentDto>(deletedComment);
            return Ok(commentDto);
        }
    }
}
