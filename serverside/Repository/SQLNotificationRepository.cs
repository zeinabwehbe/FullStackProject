using Microsoft.EntityFrameworkCore;
using serverside.Data;
using serverside.Models.Domain;

namespace serverside.Repository
{
    public class SQLNotificationRepository : INotificationRepository
    {
        private readonly projectDbContext _projectDbContext;

        public SQLNotificationRepository(projectDbContext projectDbContext)
        {
            _projectDbContext = projectDbContext;
        }

        public async Task<List<Notifications>> GetAllNotificationsAsync()
        {
            return await _projectDbContext.Notifications.ToListAsync();
        }

        public async Task<Notifications?> GetNotificationByIdAsync(int id)
        {
            return await _projectDbContext.Notifications.FirstOrDefaultAsync(n => n.Id == id);
        }

        public async Task<List<Notifications>> GetNotificationsByUserIdAsync(int userId)
        {
            return await _projectDbContext.Notifications
                .Where(n => n.UserId == userId)
                .ToListAsync();
        }

        public async Task<Notifications> CreateAsync(Notifications notification)
        {
            await _projectDbContext.Notifications.AddAsync(notification);
            await _projectDbContext.SaveChangesAsync();
            return notification;
        }

        public async Task<Notifications?> UpdateAsync(int id, Notifications notification)
        {
            var existingNotification = await _projectDbContext.Notifications.FirstOrDefaultAsync(n => n.Id == id);

            if (existingNotification == null)
            {
                return null;
            }

            // Update fields
            existingNotification.UserId = notification.UserId;
            existingNotification.Message = notification.Message;
            existingNotification.Status = notification.Status;

            await _projectDbContext.SaveChangesAsync();
            return existingNotification;
        }

        public async Task<Notifications?> DeleteAsync(int id)
        {
            var notification = await _projectDbContext.Notifications.FirstOrDefaultAsync(n => n.Id == id);
            if (notification == null)
            {
                return null;
            }

            _projectDbContext.Notifications.Remove(notification);
            await _projectDbContext.SaveChangesAsync();
            return notification;
        }
    }
}
