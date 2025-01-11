using serverside.Models.Domain;

namespace serverside.Repository
{
    public interface ICategoryRepository
    {
        Task<List<Categories>> GetAllCategoriesAsync();
        Task<Categories?> GetCategoryByIdAsync(int id);
        Task<Categories> CreateAsync(Categories Categories);
        Task<Categories?> UpdateAsync(int id, Categories Categories);
        Task<Categories?> DeleteAsync(int id);
    }
}
