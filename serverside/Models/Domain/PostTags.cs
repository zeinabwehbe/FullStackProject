using Azure;
using Microsoft.Extensions.Hosting;

namespace serverside.Models.Domain
{
    public class PostTags
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public int TagId { get; set; }

        // Navigation properties
        public Posts Post { get; set; }
        public Tags Tag { get; set; }
    }
}
