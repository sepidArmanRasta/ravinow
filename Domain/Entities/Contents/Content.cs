using Domain.Attributes;
using Domain.Enumeration.Content;

namespace Domain.Entities.Contents
{
    [Auditable]
    public class Content
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public required string Source { get; set; }
        public string? CallBackURL { get; set; }
        public string? Description { get; set; }
        public ContentTypeEnum ContentType { get; set; }
    }
}