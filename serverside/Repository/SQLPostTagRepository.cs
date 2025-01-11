using Microsoft.EntityFrameworkCore;
using serverside.Data;
using serverside.Models.Domain;

namespace serverside.Repository
{
    public class SQLPostTagRepository : IPostTagRepository
    {
        private readonly projectDbContext _context;

        public SQLPostTagRepository(projectDbContext context)
        {
            _context = context;
        }

        public async Task<List<PostTags>> GetAllPostTagsAsync()
        {
            return await _context.PostTags.Include(pt => pt.Post).Include(pt => pt.Tag).ToListAsync();
        }

        public async Task<PostTags?> GetPostTagByPostAndTagIdAsync(int postId, int tagId)
        {
            return await _context.PostTags
                .Include(pt => pt.Post)
                .Include(pt => pt.Tag)
                .FirstOrDefaultAsync(pt => pt.PostId == postId && pt.TagId == tagId);
        }

        public async Task<PostTags> CreateAsync(PostTags postTag)
        {
            await _context.PostTags.AddAsync(postTag);
            await _context.SaveChangesAsync();
            return postTag;
        }

        public async Task<PostTags?> DeleteAsync(int postId, int tagId)
        {
            var existingPostTag = await _context.PostTags
                .FirstOrDefaultAsync(pt => pt.PostId == postId && pt.TagId == tagId);

            if (existingPostTag == null)
            {
                return null;
            }

            _context.PostTags.Remove(existingPostTag);
            await _context.SaveChangesAsync();
            return existingPostTag;
        }
    }
}
