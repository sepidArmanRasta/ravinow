namespace Application.Services.Blogs.Dtos
{
    public class UpdateBlog
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Photo { get; set; } = string.Empty;
        public string AuthorId { get; set; } = string.Empty;
        public List<Guid> CategoryId { get; set; } = [];
        public List<string>? TagsId { get; set; } = [];
    }
}
