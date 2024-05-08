using Application.Services.BlogCategories.Dtos;
using Domain.Entities.Users;

namespace Application.Services.Blogs.Dtos
{
    public class BlogItemDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Photo { get; set; } = string.Empty;
        public DateTime InsertTime { get; set; } = DateTime.Now;
        public DateTime? UpdateTime { get; set; }
        public ApplicationUser? Author { get; set; }
        public IEnumerable<BlogCategoriesItemDto>? Categories { get; set; }
    }
}
