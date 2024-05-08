using Application.Services.Users.Dtos;
using Domain.Entities.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Memory;

namespace WebArMa.Ravino.Ravino.WebApp.Utiles.Helpers.User
{
    public class UserHelper(UserManager<ApplicationUser> userManager, IMemoryCache memoryCache) : IUserHelper
    {
        public UserDto? GetUserInfo(string? UserName)
        {
            if (UserName == null)
                return null;

            var cache = memoryCache.Get<UserDto?>(UserName);

            if (cache == null)
            {

                var user = userManager?.FindByNameAsync(UserName).Result;

                if (user != null)
                {
                    return new UserDto
                    {
                        Id = user.Id,
                        UserName = user.UserName ?? string.Empty,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                    };
                }
                return null;
            }
            else
            {
                return cache;
            }
        }
    }
}
