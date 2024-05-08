using Application.Services.Users.Dtos;

namespace WebArMa.Ravino.Ravino.WebApp.Utiles.Helpers.User
{
    public interface IUserHelper
    {
        UserDto? GetUserInfo(string? UserName);
    }
}
