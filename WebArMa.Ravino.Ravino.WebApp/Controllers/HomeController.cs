using Application.Dtos._Pagination;
using Application.Services.Blogs;
using Application.Services.Blogs.Dtos;
using Application.Services.Contents;
using Domain.Enumeration._Pagination;
using Domain.Enumeration.Blog;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebArMa.Ravino.Ravino.WebApp.Models;
using WebArMa.Ravino.Ravino.WebApp.Utiles.Helpers.User;

namespace WebArMa.Ravino.Ravino.WebApp.Controllers
{
    public class HomeController(IUserHelper userHelper, IContentService contentService, IBlogService blogService) : _Controller(userHelper)
    {
        public async Task<IActionResult> Index()
        {
            var newsModel = new PaginationRequestDto<BlogSearchDto, BlogSortEnum>() { Page = 1, PageSize = 8, Search = new BlogSearchDto(), Sort = BlogSortEnum.PublishDate, SortType = SortType.Descending };
            var news = await blogService.GetList(newsModel);
            var model = contentService.GetList(["general-phone", "general-email", "general-address", "main-image", "main-about-us-text", "main-statistics-text", "main-news-text", "main-why-us-text", "main-partners", "main-faq"]);
            ViewBag.News = news.Data;
            return View(model.Data);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}