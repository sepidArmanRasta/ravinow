namespace Application.Services.Blogs.Dtos
{
    public class InsertBlogDto
    {
        public string Title { get; set; } 
        public string Body { get; set; } 
        public string Description { get; set; } 
        public string Photo { get; set; } 
        public string FileName { get; set; } 
        public DateTime PublishDate { get; set; }
        public Guid? AuthorId { get; set; }
        public Guid CategoryId { get; set; }
        public string? Tags { get; set; } 
    }
}
