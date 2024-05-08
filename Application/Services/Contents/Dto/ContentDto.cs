using Domain.Enumeration.Content;

namespace Application.Services.Contents.Dto
{
    public class ContentDto
    {
        public Guid Id { get; set; }
        public string? Description { get; set; }
        public string? CallBackURL { get; set; }
        public string? Source { get; set; }
        public ContentTypeEnum ContentType { get; set; }
    }
}