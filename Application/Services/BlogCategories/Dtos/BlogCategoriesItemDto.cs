namespace Application.Services.BlogCategories.Dtos
{
    public class BlogCategoriesItemDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Photo { get; set; }
        public string? Description { get; set; }
        public virtual BlogCategoriesItemDto? ParentCategory { get; set; }
    }
}
