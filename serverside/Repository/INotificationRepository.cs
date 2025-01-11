using serverside.Models.Domain;

namespace serverside.Repository
{
    public interface INotificationRepository
    {
        Task<List<Notifications>> GetAllNotificationsAsync();
        Task<Notifications?> GetNotificationByIdAsync(int id);
        Task<List<Notifications>> GetNotificationsByUserIdAsync(int userId);
        Task<Notifications> CreateAsync(Notifications notification);
        Task<Notifications?> UpdateAsync(int id, Notifications notification);
        Task<Notifications?> DeleteAsync(int id);
    }
}
