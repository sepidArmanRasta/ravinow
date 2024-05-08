namespace Application.Services.BlogCategories.Dtos
{
    public class UpdateBlogCategoryDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Photo { get; set; }
        public string? Description { get; set; }
        public Guid? ParentCategoryId { get; set; } = null;
    }
}
