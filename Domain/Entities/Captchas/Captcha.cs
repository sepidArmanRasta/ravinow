using Domain.Attributes;

namespace Domain.Entities.Captchas
{
    [Auditable]
    public class Captcha
    {
        public Guid Id { get; set; }
        public required string Value { get; set; }
        public DateTime ValidTime { get; set; }
    }
}