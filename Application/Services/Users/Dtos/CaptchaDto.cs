namespace Application.Services.Users.Dtos
{
    public class CaptchaDto
    {
        public Guid Id { get; set; }
        public string Captcha { get; set; } = string.Empty;
    }
}