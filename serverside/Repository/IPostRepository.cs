using serverside.Models.Domain;

namespace serverside.Repository
{
    public interface IPostRepository
    {
        Task<List<Posts>> GetAllPostsAsync();
        Task<Posts?> GetPostByIdAsync(int id);
        Task<Posts> CreateAsync(Posts post);
        Task<Posts?> UpdateAsync(int id, Posts post);
        Task<Posts?> DeleteAsync(int id);
        Task<List<Posts>> GetPostsByUserIdAsync(int userId);
        Task<List<Posts>> GetPostsByCategoryIdAsync(int categoryId);
        Task<List<Posts?>> GetPostsByTagIdAsync(int tagId);
        Task<Posts?> UpvoteAsync(int postId);
    }
}
