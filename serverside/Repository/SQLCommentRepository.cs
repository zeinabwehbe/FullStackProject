using Microsoft.EntityFrameworkCore;
using serverside.Data;
using serverside.Models.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace serverside.Repository
{
    public class SQLCommentRepository : ICommentRepository
    {
        private readonly projectDbContext _projectDbContext;

        public SQLCommentRepository(projectDbContext projectDbContext)
        {
            this._projectDbContext = projectDbContext;
        }

        public async Task<Comments> CreateAsync(Comments comment)
        {
            // Add new comment to the database
            await _projectDbContext.Comments.AddAsync(comment);
            await _projectDbContext.SaveChangesAsync();
            return comment;
        }

        public async Task<Comments?> GetCommentByIdAsync(int id)
        {
            // Retrieve a comment by its ID
            return await _projectDbContext.Comments.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<List<Comments>> GetAllCommentsAsync()
        {
            // Retrieve all comments
            return await _projectDbContext.Comments.ToListAsync();
        }



        public async Task<Comments?> UpdateAsync(int id, Comments comment)
        {
            // Find the existing comment
            var existingComment = await _projectDbContext.Comments.FirstOrDefaultAsync(c => c.Id == id);

            if (existingComment == null)
            {
                return null;
            }

            // Update comment properties
            existingComment.Content = comment.Content;
            existingComment.UserId = comment.UserId;
            existingComment.PostId = comment.PostId;
            existingComment.CreatedAt = comment.CreatedAt;

            // Save changes to the database
            await _projectDbContext.SaveChangesAsync();
            return existingComment;
        }

        public async Task<Comments?> DeleteAsync(int id)
        {
            // Find the comment to delete
            var existingComment = await _projectDbContext.Comments.FirstOrDefaultAsync(c => c.Id == id);

            if (existingComment == null)
            {
                return null;
            }

            // Remove the comment from the database
            _projectDbContext.Comments.Remove(existingComment);
            await _projectDbContext.SaveChangesAsync();
            return existingComment;
        }

        public async Task<List<Comments>> GetCommentsByUserIdAsync(int userId)
        {
            // Retrieve votes for a specific candidate
            return await _projectDbContext.Comments
                .Where(v => v.UserId == userId)
                .ToListAsync();
        }

        public async Task<List<Comments>> GetCommentsByPostIdAsync(int postId)
        {
            // Retrieve votes for a specific candidate
            return await _projectDbContext.Comments
                .Where(v => v.PostId == postId)
                .ToListAsync();
        }
    }
}
