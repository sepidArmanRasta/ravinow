using Domain.Attributes;
using Domain.Entities.Users;

namespace Domain.Entities.Blogs
{
    [Auditable]
    public class Blog
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public required string Title { get; set; }
        public required string Body { get; set; }
        public required string Description { get; set; }
        public string? Tags { get; set; }
        public string? Photo { get; set; }
        public DateTime PublishDate { get; set; }
        public DateTime InsertTime { get; set; } = DateTime.Now;
        public DateTime UpdateTime { get; set; } = DateTime.Now;
        public virtual required ApplicationUser Author { get; set; }
        public virtual IEnumerable<BlogCategory>? Categories { get; set; }
    }
}