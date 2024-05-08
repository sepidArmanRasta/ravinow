namespace Application.Services.BlogCategories.Dtos
{
    public class BlogCategorySearchDto
    {
        public Guid? Id { get; set; }
        public string? StringSearch { get; set; }
        public Guid? ParentCategoryId { get; set; }
    }
}
