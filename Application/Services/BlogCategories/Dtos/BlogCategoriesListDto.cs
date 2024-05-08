using Domain.Entities.Blogs;

namespace Application.Services.BlogCategories.Dtos
{
    public class BlogCategoriesListDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Photo { get; set; }
        public string? Description { get; set; }
        public virtual BlogCategory? ParentCategory { get; set; }
    }
}
