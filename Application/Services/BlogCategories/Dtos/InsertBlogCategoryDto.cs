using Domain.Enumeration.Blog;

namespace Application.Services.BlogCategories.Dtos
{
    public class InsertBlogCategoryDto
    {
        public string Name { get; set; } = string.Empty;
        public string Photo { get; set; } = string.Empty;
        public string FileName { get; set; } = string.Empty;
        public string? Description { get; set; }
        public Guid? ParentCategoryId { get; set; } = null;
    }
}