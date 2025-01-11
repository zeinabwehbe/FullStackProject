using Microsoft.EntityFrameworkCore;
using serverside.Data;
using serverside.Models.Domain;
using serverside.Models.DTOs;

namespace serverside.Repository
{
    public class SQLUserRepository : IUserRepository
    {
        private readonly projectDbContext _projectDbContext;

        public SQLUserRepository(projectDbContext projectDbContext)
        {
            this._projectDbContext = projectDbContext;
        }

        public async Task<Users> CreateAsync(Users user)
        {
            // Check if the email already exists
            var existingUser = await _projectDbContext.Users
                .FirstOrDefaultAsync(u => u.Email == user.Email);

            if (existingUser != null)
            {
                throw new Exception("Email already exists."); // Or use a custom exception
            }
            await _projectDbContext.Users.AddAsync(user);
            await _projectDbContext.SaveChangesAsync();
            return user;
        }


        public async Task<Users?> GetUserByIdAsync(int id)
        {
           return await _projectDbContext.Users.FirstOrDefaultAsync( x => x.Id == id);
        }

        public async Task<Users?> UpdateAsync(int id, Users user)
        {
            var existingUser = await _projectDbContext.Users.FirstOrDefaultAsync(x => x.Id == id);

            if (existingUser == null)
            {
                return null;
            }

            existingUser.Username = user.Username;
            existingUser.Email = user.Email;
            existingUser.Password = user.Password; // TODO: Hash the password before saving
            existingUser.Role = user.Role;
            existingUser.ReputationPoints = user.ReputationPoints;


            await _projectDbContext.SaveChangesAsync();
            return existingUser;
        }


        public async Task<Users?> DeleteAsync(int id)
        {
            var existingUser = await _projectDbContext.Users.FirstOrDefaultAsync( x => x.Id == id);
            if (existingUser == null)
            {
                return null;
            }

             _projectDbContext.Users.Remove(existingUser);
            await _projectDbContext.SaveChangesAsync();
            return existingUser;
        }

        public async Task<List<Users>> GetAllUsersAsync()
        {
            IQueryable<Users> usersQuery = _projectDbContext.Users;

            //// filtering
            //if (!string.IsNullOrWhiteSpace(filterOn))
            //{
            //    if (filterOn.Equals("MyProperty", StringComparison.OrdinalIgnoreCase))
            //    {
            //        usersQuery = usersQuery.Where(x => x.MyProperty == filterQuery);
            //    }
            //}

            var users = await usersQuery.ToListAsync();
            return users;
        }

        public async Task<Users?> GetUserByEmailAsync(string email)
        {
            var user = await _projectDbContext.Users.FirstOrDefaultAsync(x => x.Email == email);
            return user;
        }
    }
}
