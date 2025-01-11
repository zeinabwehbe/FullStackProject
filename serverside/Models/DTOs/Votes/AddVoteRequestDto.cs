namespace serverside.Models.DTOs.Votes
{
    public class AddVoteRequestDto
    {
        public int PostId { get; set; }
        public int UserId { get; set; }
        public string VoteType { get; set; }
    }
}
