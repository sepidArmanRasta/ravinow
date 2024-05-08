namespace Application.Services.Users.Dtos
{
    public class ExtensionTokenDto
    {
        public string Token { get; set; } = string.Empty;
        public Guid RefreshToken { get; set; }
    }
}