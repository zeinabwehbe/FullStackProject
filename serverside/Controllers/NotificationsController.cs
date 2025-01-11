using Microsoft.AspNetCore.Mvc;
using serverside.Models.Domain;
using serverside.Repository;
using AutoMapper;
using serverside.Models.DTOs.Notifications;

namespace serverside.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        private readonly INotificationRepository notificationRepository;
        private readonly IMapper mapper;

        public NotificationsController(INotificationRepository notificationRepository, IMapper mapper)
        {
            this.notificationRepository = notificationRepository;
            this.mapper = mapper;
        }

        // GET: api/Notifications
        [HttpGet]
        public async Task<IActionResult> GetAllNotifications()
        {
            var notificationsDomain = await notificationRepository.GetAllNotificationsAsync();
            var notificationsDto = mapper.Map<List<NotificationDto>>(notificationsDomain);

            return Ok(notificationsDto);
        }

        // GET: api/Notifications/{id}
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetNotificationById([FromRoute] int id)
        {
            var notificationDomain = await notificationRepository.GetNotificationByIdAsync(id);
            if (notificationDomain == null)
            {
                return NotFound($"Notification with ID {id} not found.");
            }

            var notificationDto = mapper.Map<NotificationDto>(notificationDomain);
            return Ok(notificationDto);
        }

        // GET: api/Notifications/User/{userId}
        [HttpGet("User/{userId:int}")]
        public async Task<IActionResult> GetNotificationsByUserId([FromRoute] int userId)
        {
            var notificationsDomain = await notificationRepository.GetNotificationsByUserIdAsync(userId);
            var notificationsDto = mapper.Map<List<NotificationDto>>(notificationsDomain);

            return Ok(notificationsDto);
        }

        // POST: api/Notifications
        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> CreateNotification([FromBody] AddNotificationRequestDto addNotificationRequestDto)
        {
            var notificationDomain = mapper.Map<Notifications>(addNotificationRequestDto);

            notificationDomain = await notificationRepository.CreateAsync(notificationDomain);

            var notificationDto = mapper.Map<NotificationDto>(notificationDomain);

            return CreatedAtAction(nameof(GetNotificationById), new { id = notificationDomain.Id }, notificationDto);
        }

        // PUT: api/Notifications/{id}
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateNotification([FromRoute] int id, [FromBody] UpdateNotificationRequestDto updateNotificationRequestDto)
        {
            var notificationDomain = mapper.Map<Notifications>(updateNotificationRequestDto);

            var updatedNotification = await notificationRepository.UpdateAsync(id, notificationDomain);

            if (updatedNotification == null)
            {
                return NotFound($"Notification with ID {id} not found.");
            }

            var notificationDto = mapper.Map<NotificationDto>(updatedNotification);
            return Ok(new
            {
                Message = "Notification updated successfully.",
                Notification = notificationDto
            });
        }

        // DELETE: api/Notifications/{id}
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteNotification([FromRoute] int id)
        {
            var deletedNotification = await notificationRepository.DeleteAsync(id);

            if (deletedNotification == null)
            {
                return NotFound($"Notification with ID {id} not found.");
            }

            var notificationDto = mapper.Map<NotificationDto>(deletedNotification);
            return Ok(new
            {
                Message = "Notification deleted successfully.",
                Notification = notificationDto
            });
        }
    }
}
