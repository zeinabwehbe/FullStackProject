using Microsoft.EntityFrameworkCore;
using serverside.Data;
using serverside.Models.Domain;

namespace serverside.Repository
{
    public class SQLVoteRepository : IVoteRepository
    {
        private readonly projectDbContext _projectDbContext;

        public SQLVoteRepository(projectDbContext projectDbContext)
        {
            _projectDbContext = projectDbContext;
        }

        public async Task<Votes> CreateAsync(Votes vote)
        {
            // Add a new vote
            await _projectDbContext.Votes.AddAsync(vote);
            await _projectDbContext.SaveChangesAsync();
            return vote;
        }

        public async Task<Votes?> GetVoteByIdAsync(int id)
        {
            // Retrieve a vote by its ID
            return await _projectDbContext.Votes.FirstOrDefaultAsync(v => v.Id == id);
        }

        public async Task<Votes?> UpdateAsync(int id, Votes vote)
        {
            // Find the existing vote
            var existingVote = await _projectDbContext.Votes.FirstOrDefaultAsync(v => v.Id == id);

            if (existingVote == null)
            {
                return null;
            }

            // Update vote properties
            existingVote.PostId = vote.PostId;
            existingVote.UserId = vote.UserId;
            existingVote.VoteType = vote.VoteType;

            await _projectDbContext.SaveChangesAsync();
            return existingVote;
        }

        public async Task<Votes?> DeleteAsync(int id)
        {
            // Find the vote to delete
            var existingVote = await _projectDbContext.Votes.FirstOrDefaultAsync(v => v.Id == id);

            if (existingVote == null)
            {
                return null;
            }

            _projectDbContext.Votes.Remove(existingVote);
            await _projectDbContext.SaveChangesAsync();
            return existingVote;
        }

        public async Task<List<Votes>> GetAllVotesAsync()
        {
            // Retrieve all votes
            return await _projectDbContext.Votes.ToListAsync();
        }

        public async Task<List<Votes>> GetVotesByPostIdAsync(int candidateId)
        {
            // Retrieve votes for a specific candidate
            return await _projectDbContext.Votes
                .Where(v => v.PostId == candidateId)
                .ToListAsync();
        }

        public async Task<int> CountVotesByUserIdAsync(int electionId)
        {
            // Count votes for a specific election
            return await _projectDbContext.Votes
                .CountAsync(v => v.UserId == electionId);
        }
    }
}
