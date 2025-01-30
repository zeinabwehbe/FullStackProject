using Microsoft.EntityFrameworkCore;
using serverside.Data;
using serverside.Models.Domain;

namespace serverside.Repository
{
    public class SQLPostRepository : IPostRepository
    {
        private readonly projectDbContext _projectDbContext;

        public SQLPostRepository(projectDbContext projectDbContext)
        {
            this._projectDbContext = projectDbContext;
        }

        public async Task<Posts> CreateAsync(Posts post)
        {
            // Add a new post to the database
            await _projectDbContext.Posts.AddAsync(post);
            await _projectDbContext.SaveChangesAsync();
            return post;
        }
        public async Task<List<Posts>> GetAllPostsAsync(string? filterOn, string? filterQuery, int? userId)
        {
            IQueryable<Posts> postsQuery = _projectDbContext.Posts;

            if (!string.IsNullOrWhiteSpace(filterOn))
            {
                if (filterOn.Equals("Title", StringComparison.OrdinalIgnoreCase))
                {
                    postsQuery = postsQuery.Where(x => x.Title.Contains(filterQuery));
                }
                else if (filterOn.Equals("Author", StringComparison.OrdinalIgnoreCase))
                {
                    postsQuery = postsQuery.Where(x => x.Title.Contains(filterQuery));
                }
            }
            // Filter by UserId if it's provided
            if (userId.HasValue)
            {
                postsQuery = postsQuery.Where(x => x.UserId == userId.Value);
            }

            var posts = await postsQuery.ToListAsync();
            return posts;
        }

        public async Task<Posts?> GetPostByIdAsync(int id)
        {
            // Find the post by ID
            var post = await _projectDbContext.Posts.FindAsync(id);
            return post;
        }

        public async Task<Posts?> UpdateAsync(int id, Posts post)
        {
            // Find the post by ID
            var existingPost = await _projectDbContext.Posts.FindAsync(id);

            if (existingPost == null)
            {
                return null;
            }

            // Update the properties of the existing post
            existingPost.Title = post.Title;
            existingPost.Description = post.Description;
            existingPost.TagId = post.TagId;
            existingPost.CategoryId = post.CategoryId;
            existingPost.Upvotes = post.Upvotes;

            existingPost.UpdatedAt = DateTime.UtcNow;

            // Save changes
            await _projectDbContext.SaveChangesAsync();
            return existingPost;
        }

        public async Task<Posts?> DeleteAsync(int id)
        {
            // Find the post by ID
            var post = await _projectDbContext.Posts.FindAsync(id);

            if (post == null)
            {
                return null;
            }

            // Remove the post from the database
            _projectDbContext.Posts.Remove(post);
            await _projectDbContext.SaveChangesAsync();
            return post;
        }

        public async Task<List<Posts>> GetPostsByUserIdAsync(int userId)
        {
            // Get all posts by a specific user
            var posts = await _projectDbContext.Posts
                .Where(p => p.UserId == userId)
                .ToListAsync();

            return posts;
        }

        public async Task<List<Posts>> GetPostsByCategoryIdAsync(int categoryId)
        {
            // Retrieve all comments for a specific category
            return await _projectDbContext.Posts
                .Where(c => c.CategoryId == categoryId)
                .ToListAsync();
        }


        public async Task<List<Posts>> GetPostsByTagIdAsync(int tagId)
        {
            // Retrieve all comments for a specific posts with tagId
            return await _projectDbContext.Posts
                .Where(c => c.TagId == tagId)
                .ToListAsync();
        }

        public async Task<Posts?> UpvoteAsync(int postId)
        {
            // Find the post by its ID
            var post = await _projectDbContext.Posts.FirstOrDefaultAsync(p => p.Id == postId);

            if (post == null)
            {
                return null; // Post not found
            }

            // Increment the upvote count
            post.Upvotes += 1;

            // Save changes
            await _projectDbContext.SaveChangesAsync();

            return post;
        }
        public async Task<Posts?> DownvoteAsync(int postId)
        {
            // Find the post by its ID
            var post = await _projectDbContext.Posts.FirstOrDefaultAsync(p => p.Id == postId);

            if (post == null)
            {
                return null; // Post not found
            }

            // Increment the upvote count
            post.Downvotes += 1;

            // Save changes
            await _projectDbContext.SaveChangesAsync();

            return post;
        }
    }

}
