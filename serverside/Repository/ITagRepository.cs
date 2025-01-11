using serverside.Models.Domain;

namespace serverside.Repository
{
    public interface ITagRepository
    {
        Task<List<Tags>> GetAllTagsAsync();
        Task<Tags?> GetTagByIdAsync(int id);
        Task<List<Tags>> GetTagsByNameAsync(string name);
        Task<Tags> CreateAsync(Tags tag);
        Task<Tags?> UpdateAsync(int id, Tags tag);
        Task<Tags?> DeleteAsync(int id);
    }
}
