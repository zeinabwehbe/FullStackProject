namespace serverside.Models.Domain
{
    public class Tags
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        // Navigation properties
        public ICollection<PostTags> PostTags { get; set; }
    }
}
