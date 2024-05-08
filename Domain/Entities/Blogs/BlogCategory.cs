using Domain.Attributes;
using Domain.Enumeration.Blog;

namespace Domain.Entities.Blogs
{
    [Auditable]
    public class BlogCategory
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public required string Name { get; set; }
        public string? Photo { get; set; }
        public string? Description { get; set; }
        public BlogCategoryTypeEnum BlogCategoryType { get; set; }
        public virtual BlogCategory? ParentCategory { get; set; }
        public virtual IEnumerable<Blog>? Blogs { get; set; }
    }
}