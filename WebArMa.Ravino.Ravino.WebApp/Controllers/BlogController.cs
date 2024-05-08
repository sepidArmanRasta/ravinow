using Application.Dtos._Pagination;
using Application.Services.Blogs;
using Application.Services.Blogs.Dtos;
using Domain.Enumeration.Blog;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebArMa.Ravino.Ravino.WebApp.Utiles.Filters;
using WebArMa.Ravino.Ravino.WebApp.Utiles.Helpers.User;

namespace WebArMa.Ravino.Ravino.WebApp.Controllers
{
    [Route("[controller]")]
    public class BlogController(IUserHelper userHelper, IBlogService blogService) : _Controller(userHelper)
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> GetList([FromBody] PaginationRequestDto<BlogSearchDto, BlogSortEnum> paginationRequestDto)
        {
            return Json(await blogService.GetList(paginationRequestDto));
        }

        [Route("{title}")]
        public async Task<IActionResult> BlogDetail(string title)
        {
            var model = await blogService.GetItem(title);
            ViewData["Title"] = title;
            return View(model.Data);
        }

        [HttpDelete]
        [UserMustBeAdmin]
        [Route("[action]/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return Json(await blogService.Delete(id));
        }

        [Route("[action]")]
        [UserMustBeAdmin]
        public IActionResult Insert()
        {
            return View();
        }

        [HttpPost]
        [Route("[action]")]
        [UserMustBeAdmin]
        public async Task<IActionResult> Insert([FromBody] InsertBlogDto insertBlogDto)
        {
            insertBlogDto.AuthorId = userHelper.GetUserInfo(User.Identity?.Name)?.Id;
            return Json(await blogService.Insert(insertBlogDto));
        }
    }
}