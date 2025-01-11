namespace serverside.Models.DTOs.Comments
{
    public class UpdateCommentRequestDto
    {
        public int PostId { get; set; }
        public int UserId { get; set; }
        public string Content { get; set; }
    }
}
