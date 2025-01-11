using serverside.Models.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace serverside.Repository
{
    public interface ICommentRepository
    {
        Task<List<Comments>> GetAllCommentsAsync();
        Task<Comments?> GetCommentByIdAsync(int id);
        Task<List<Comments>> GetCommentsByUserIdAsync(int userId);
        Task<List<Comments>> GetCommentsByPostIdAsync(int postId);
        Task<Comments> CreateAsync(Comments comment);
        Task<Comments?> UpdateAsync(int id, Comments comment);
        Task<Comments?> DeleteAsync(int id);
    }
}
