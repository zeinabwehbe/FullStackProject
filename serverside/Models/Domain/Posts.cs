/*Posts:

A post belongs to one user and one category.
A post can have multiple tags and comments.*/
using Microsoft.Extensions.Hosting;

namespace serverside.Models.Domain
{
    public class Posts

    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int TagId { get; set; }
        public int CategoryId { get; set; }
        public int Upvotes { get; set; }
        public int Downvotes { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        // Navigation properties
        public Users User { get; set; }
        public ICollection<Comments> Comments { get; set; }
        public ICollection<Votes> Votes { get; set; }
        public ICollection<PostTags> PostTags { get; set; }
        public Categories Category { get; set; }
    }
}


