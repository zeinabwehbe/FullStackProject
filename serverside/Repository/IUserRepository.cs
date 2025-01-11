using serverside.Models.Domain;
using serverside.Models.DTOs;
namespace serverside.Repository
{
    public interface IUserRepository
    {
        Task<List<Users>> GetAllUsersAsync();
        //Task<List<Users>> GetAllUsersAsync(string? filterOn = null,int? filterQuery = null );
        Task<Users?> GetUserByIdAsync(int id);
        Task<Users> CreateAsync(Users user);
        Task<Users?> UpdateAsync(int id, Users user);
        Task<Users?> DeleteAsync(int id);
        Task<Users?> GetUserByEmailAsync(string email);
    }
}
