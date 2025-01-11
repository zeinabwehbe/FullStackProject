namespace serverside.Models.DTOs.Comments
{
    public class AddCommentRequestDto
    {
        public int PostId { get; set; }
        public int UserId { get; set; }
        public string Content { get; set; }
    }
}
