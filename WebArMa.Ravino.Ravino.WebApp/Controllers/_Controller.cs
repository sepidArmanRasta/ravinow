using Application.Services.Contents;
using Microsoft.AspNetCore.Mvc;
using WebArMa.Ravino.Ravino.WebApp.Utiles.Helpers.User;

namespace WebArMa.Ravino.Ravino.WebApp.Controllers
{
    public class _Controller(IUserHelper userHelper) : Controller
    {
        public override ViewResult View()
        {
            ViewBag.UserInfo = userHelper.GetUserInfo(User.Identity?.Name);
            ViewBag.Route = RouteData.Values["controller"];
            return base.View();
        }

        public override ViewResult View(object? model)
        {
            ViewBag.UserInfo = userHelper.GetUserInfo(User.Identity?.Name);
            ViewBag.Route = RouteData.Values["controller"];
            return base.View(model);
        }

        public override ViewResult View(string? viewName)
        {
            ViewBag.UserInfo = userHelper.GetUserInfo(User.Identity?.Name);
            ViewBag.Route = RouteData.Values["controller"];
            return base.View(viewName);
        }

        public override ViewResult View(string? viewName, object? model)
        {
            ViewBag.UserInfo = userHelper.GetUserInfo(User.Identity?.Name);
            ViewBag.Route = RouteData.Values["controller"];
            return base.View(viewName, model);
        }
    }
}
