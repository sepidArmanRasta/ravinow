namespace Application.Services.Blogs.Dtos
{
    public class PortfolioDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Photo { get; set; } = string.Empty;
    }
}