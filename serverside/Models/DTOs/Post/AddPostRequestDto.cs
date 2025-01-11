using serverside.Models.Domain;

namespace serverside.Models.DTOs.Post
{
    public class AddPostRequestDto
    {
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int TagId { get; set; }
        public int CategoryId { get; set; }
        public int Upvotes { get; set; }
        public int Downvotes { get; set; }
    }
}
