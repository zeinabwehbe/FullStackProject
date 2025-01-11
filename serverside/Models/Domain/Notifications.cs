namespace serverside.Models.Domain
{
    public class Notifications
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Message { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }

        // Navigation properties
        public Users User { get; set; }
    }
}
