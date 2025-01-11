using Microsoft.Extensions.Hosting;

namespace serverside.Models.Domain
{
    public class Comments
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public int UserId { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }

        // Navigation properties
        public Posts Post { get; set; }
        public Users User { get; set; }
    }
}
