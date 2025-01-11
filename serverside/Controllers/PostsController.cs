using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using serverside.Data;
using serverside.Models.Domain;
using serverside.Models.DTOs.Post;
using serverside.Repository;

namespace serverside.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        // this is to initialize them
        private readonly IPostRepository postRepository;
        private readonly IMapper mapper;
        private readonly ILogger<PostsController> _logger;


        public PostsController(IPostRepository postRepository, IMapper mapper, ILogger<PostsController> logger)
        {
            this.postRepository = postRepository;
            this.mapper = mapper;
            _logger = logger;


        }


        // GET ALL POSTS
        // GET: https://localhost:7069/api/Posts
        [HttpGet]
        public async Task<IActionResult> GetAllPosts()
        {
            var postsDomain = await postRepository.GetAllPostsAsync();
            var postsDto = mapper.Map<List<PostDto>>(postsDomain);
            return Ok(postsDto);
        }

        // GET POST BY ID
        // GET: https://localhost:7069/api/Posts/{id}
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetPostById([FromRoute] int id)
        {
            var postDomain = await postRepository.GetPostByIdAsync(id);
            if (postDomain == null)
            {
                return NotFound($"Post with ID {id} not found.");
            }

            var postDto = mapper.Map<PostDto>(postDomain);
            return Ok(postDto);
        }

        // GET POSTS BY USER ID
        // GET: https://localhost:7069/api/Posts/user/{userId}
        [HttpGet("user/{userId:int}")]
        public async Task<IActionResult> GetPostsByUserId([FromRoute] int userId)
        {
            var postsDomain = await postRepository.GetPostsByUserIdAsync(userId);
            var postsDto = mapper.Map<List<PostDto>>(postsDomain);
            return Ok(postsDto);
        }

        // GET POSTS BY CATEGORY ID
        // GET: https://localhost:7069/api/Posts/category/{categoryId}
        [HttpGet("categories/{categoryId:int}")]
        public async Task<IActionResult> GetPostsByCategoryId([FromRoute] int categoryId)
        {
            var postsDomain = await postRepository.GetPostsByCategoryIdAsync(categoryId);
            var postsDto = mapper.Map<List<PostDto>>(postsDomain);
            return Ok(postsDto);
        }

        // GET POSTS BY tag ID
        // GET: https://localhost:7069/api/Posts/tag/{tagId}
        [HttpGet("tags/{tagId:int}")]
        public async Task<IActionResult> GetPostsByTagId([FromRoute] int tagId)
        {
            var postsDomain = await postRepository.GetPostsByTagIdAsync(tagId);
            var postsDto = mapper.Map<List<PostDto>>(postsDomain);
            return Ok(postsDto);
        }

        // CREATE A NEW POST
        // POST: https://localhost:7069/api/Posts
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddPostRequestDto addPostRequestDto)
        {
            var postDomainModel = mapper.Map<Posts>(addPostRequestDto);
            postDomainModel = await postRepository.CreateAsync(postDomainModel);
            var postDto = mapper.Map<PostDto>(postDomainModel);

            return CreatedAtAction(nameof(GetPostById), new { id = postDomainModel.Id }, postDto);
        }

        // UPDATE POST
        // PUT: https://localhost:7069/api/Posts/{id}
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdatePostRequestDto updatePostRequestDto)
        {
            var postDomainModel = mapper.Map<Posts>(updatePostRequestDto);
            postDomainModel = await postRepository.UpdateAsync(id, postDomainModel);

            if (postDomainModel == null)
            {
                return NotFound($"Post with ID {id} not found.");
            }

            var postDto = mapper.Map<PostDto>(postDomainModel);
            return Ok(new
            {
                Message = "Post updated successfully.",
                UpdatedPost = postDto
            });

        }

        // DELETE POST
        // DELETE: https://localhost:7069/api/Posts/{id}
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var postDomainModel = await postRepository.DeleteAsync(id);

            if (postDomainModel == null)
            {
                return NotFound($"Post with ID {id} not found.");
            }

            var postDto = mapper.Map<PostDto>(postDomainModel);
            return Ok(postDto);
        }
        // UPVOTE POST
        [HttpPost("{id}/upvote")]
        public async Task<IActionResult> UpvotePost(int id)
        {
            var postDomainModel = await postRepository.UpvoteAsync(id);

            if (postDomainModel == null)
            {
                return NotFound(new { message = "Post not found." });
            }

            // Map the upvoted post domain model to a DTO
            var postDto = mapper.Map<PostDto>(postDomainModel);

            return Ok(new { message = "Post upvoted successfully", postDto });
        }

    }

}
