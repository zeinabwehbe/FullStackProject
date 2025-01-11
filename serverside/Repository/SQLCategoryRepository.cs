using Microsoft.EntityFrameworkCore;
using serverside.Data;
using serverside.Models.Domain;
using serverside.Models.DTOs;

namespace serverside.Repository
{
    public class SQLCategoryRepository : ICategoryRepository
    {
        private readonly projectDbContext _projectDbContext;

        public SQLCategoryRepository(projectDbContext projectDbContext)
        {
            this._projectDbContext = projectDbContext;
        }

        // CREATE category
        public async Task<Categories> CreateAsync(Categories category)
        {
            // Check if the category already exists
            var existingCategory = await _projectDbContext.Categories
                .FirstOrDefaultAsync(c => c.Name == category.Name);

            if (existingCategory != null)
            {
                throw new Exception("Category already exists.");
            }

            await _projectDbContext.Categories.AddAsync(category);
            await _projectDbContext.SaveChangesAsync();
            return category;
        }

        // GET category by ID
        public async Task<Categories?> GetCategoryByIdAsync(int id)
        {
            return await _projectDbContext.Categories.FirstOrDefaultAsync(c => c.Id == id);
        }

        // UPDATE category
        public async Task<Categories?> UpdateAsync(int id, Categories category)
        {
            var existingCategory = await _projectDbContext.Categories.FirstOrDefaultAsync(c => c.Id == id);

            if (existingCategory == null)
            {
                return null;
            }

            existingCategory.Name = category.Name;

            await _projectDbContext.SaveChangesAsync();
            return existingCategory;
        }

        // DELETE category
        public async Task<Categories?> DeleteAsync(int id)
        {
            var existingCategory = await _projectDbContext.Categories.FirstOrDefaultAsync(c => c.Id == id);

            if (existingCategory == null)
            {
                return null;
            }

            _projectDbContext.Categories.Remove(existingCategory);
            await _projectDbContext.SaveChangesAsync();
            return existingCategory;
        }

        // GET all categories
        public async Task<List<Categories>> GetAllCategoriesAsync()
        {
            return await _projectDbContext.Categories.ToListAsync();
        }

 
    }
}
