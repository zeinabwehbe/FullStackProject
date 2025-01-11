using Microsoft.Extensions.Hosting;

namespace serverside.Models.Domain
{
    public class Votes
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public int UserId { get; set; }
        public string VoteType { get; set; }
        public DateTime CreatedAt { get; set; }

        // Navigation properties
        public Posts Post { get; set; }
        public Users User { get; set; }
    }
}
