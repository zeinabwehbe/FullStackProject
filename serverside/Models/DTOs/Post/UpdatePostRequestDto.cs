namespace serverside.Models.DTOs.Post
{
    public class UpdatePostRequestDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int Upvotes { get; set; }
        public int Downvotes { get; set; }
    }
}
