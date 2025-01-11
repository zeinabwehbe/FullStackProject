namespace serverside.Models.DTOs.Votes
{
    public class VoteDto
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public int UserId { get; set; }
        public string VoteType { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
