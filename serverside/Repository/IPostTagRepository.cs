using serverside.Models.Domain;

namespace serverside.Repository
{
    public interface IPostTagRepository
    {
        Task<List<PostTags>> GetAllPostTagsAsync();
        Task<PostTags?> GetPostTagByPostAndTagIdAsync(int postId, int tagId);
        Task<PostTags> CreateAsync(PostTags postTag);
        Task<PostTags?> DeleteAsync(int postId, int tagId);
    }
}
