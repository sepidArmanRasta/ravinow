using Application.Services.Captchas;
using Application.Services.Users;
using Application.Services.Users.Dtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebArMa.Ravino.Ravino.WebApp.Utiles.Helpers.User;

namespace WebArMa.Ravino.Ravino.WebApp.Controllers
{
    [Route("[controller]")]
    public class AccountController(IUserHelper userHelper, ICaptchaService captchaService, IUserService userService) : _Controller(userHelper)
    {
        [Route("[action]")]
        public IActionResult Login()
        {
            if (User.Identity?.IsAuthenticated == true)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            return Json(await userService.LoginAsync(loginDto));
        }

        [Route("[action]")]
        public IActionResult Register()
        {
            if (User.Identity?.IsAuthenticated == true)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            return Json(await userService.RegisterAsync(registerDto));
        }

        [HttpPost]
        [Route("[action]")]
        public IActionResult GenerateCaptcha()
        {
            return Json(captchaService.Generate());
        }

        [Route("[action]")]
        public IActionResult Logout(string callBackUrl)
        {
            userService.Logout();
            if (string.IsNullOrEmpty(callBackUrl))
            {
                return RedirectToAction("Login");
            }

            return RedirectToRoute(callBackUrl);
        }
    }
}
