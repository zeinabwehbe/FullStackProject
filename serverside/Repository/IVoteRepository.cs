using serverside.Models.Domain;

namespace serverside.Repository
{
    public interface IVoteRepository
    {
        Task<List<Votes>> GetAllVotesAsync();
        Task<Votes?> GetVoteByIdAsync(int id);
        Task<Votes> CreateAsync(Votes vote);
        Task<Votes?> UpdateAsync(int id, Votes vote);
        Task<Votes?> DeleteAsync(int id);
        Task<List<Votes>> GetVotesByPostIdAsync(int candidateId);
        Task<int> CountVotesByUserIdAsync(int electionId);
    }

}
