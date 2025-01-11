namespace serverside.Models.DTOs.Notifications
{
    public class AddNotificationRequestDto
    {
        public int UserId { get; set; }
        public string Message { get; set; }
        public string Status { get; set; }
    }
}
