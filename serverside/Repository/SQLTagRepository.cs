using Microsoft.EntityFrameworkCore;
using serverside.Data;
using serverside.Models.Domain;

namespace serverside.Repository
{
    public class SQLTagRepository : ITagRepository
    {
        private readonly projectDbContext _dbContext;

        public SQLTagRepository(projectDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Get all tags
        public async Task<List<Tags>> GetAllTagsAsync()
        {
            return await _dbContext.Tags.ToListAsync();
        }

        // Get a tag by ID
        public async Task<Tags?> GetTagByIdAsync(int id)
        {
            return await _dbContext.Tags.FirstOrDefaultAsync(tag => tag.Id == id);
        }

        // Get tags by name
        public async Task<List<Tags>> GetTagsByNameAsync(string name)
        {
            return await _dbContext.Tags
                .Where(tag => tag.Name.Contains(name))
                .ToListAsync();
        }

        // Create a new tag
        public async Task<Tags> CreateAsync(Tags tag)
        {
            await _dbContext.Tags.AddAsync(tag);
            await _dbContext.SaveChangesAsync();
            return tag;
        }

        // Update an existing tag
        public async Task<Tags?> UpdateAsync(int id, Tags tag)
        {
            var existingTag = await _dbContext.Tags.FirstOrDefaultAsync(t => t.Id == id);

            if (existingTag == null)
            {
                return null;
            }

            existingTag.Name = tag.Name;

            await _dbContext.SaveChangesAsync();
            return existingTag;
        }

        // Delete a tag
        public async Task<Tags?> DeleteAsync(int id)
        {
            var existingTag = await _dbContext.Tags.FirstOrDefaultAsync(t => t.Id == id);

            if (existingTag == null)
            {
                return null;
            }

            _dbContext.Tags.Remove(existingTag);
            await _dbContext.SaveChangesAsync();
            return existingTag;
        }
    }
}
