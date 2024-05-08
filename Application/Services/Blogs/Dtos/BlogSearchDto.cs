namespace Application.Services.Blogs.Dtos
{
    public class BlogSearchDto
    {
        public string? StringSearch { get; set; }
        public DateTime? InsertTimeFrom { get; set; }
        public DateTime? InsertTimeTo { get; set; }
        public DateTime? UpdateTimeFrom { get; set; }
        public DateTime? UpdateTimeTo { get; set; }
        public Guid? AuthorId { get; set; }
        public Guid? CategoryId { get; set; }
        public string? Tags { get; set; }
    }
}
