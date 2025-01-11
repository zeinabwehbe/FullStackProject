using System.Xml.Linq;
using Microsoft.Extensions.Hosting;
/**Users:

A user can have multiple posts, comments, votes, and notifications.*/
namespace serverside.Models.Domain
{
    public class Users
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public int ReputationPoints { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        // Navigation properties
        public ICollection<Posts> Posts { get; set; }
        public ICollection<Comments> Comments { get; set; }
        public ICollection<Votes> Votes { get; set; }
        public ICollection<Notifications> Notifications { get; set; }
    }

}
