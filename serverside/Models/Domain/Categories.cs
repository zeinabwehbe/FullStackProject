using Microsoft.Extensions.Hosting;

namespace serverside.Models.Domain
{
    public class Categories
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        // Navigation properties
        public ICollection<Posts> Posts { get; set; }
    }
}
