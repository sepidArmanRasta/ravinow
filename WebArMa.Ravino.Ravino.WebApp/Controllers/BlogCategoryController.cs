using Application.Dtos._Pagination;
using Application.Services.BlogCategories;
using Application.Services.BlogCategories.Dtos;
using Domain.Enumeration.BlogCategory;
using Microsoft.AspNetCore.Mvc;
using System;
using WebArMa.Ravino.Ravino.WebApp.Utiles.Helpers.User;

namespace WebArMa.Ravino.Ravino.WebApp.Controllers
{
    [Route("[controller]")]
    public class BlogCategoryController(IUserHelper userHelper, IBlogCategoryService blogCategoryService) : _Controller(userHelper)
    {
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> GetList([FromBody] PaginationRequestDto<BlogCategorySearchDto, BlogCategorySortEnum> paginationRequestDto)
        {
            return Json(await blogCategoryService.GetList(paginationRequestDto));
        }

        [HttpGet]
        [Route("[action]/{id}")]
        public async Task<IActionResult> GetItem([FromRoute] Guid id)
        {
            return Json(await blogCategoryService.GetItem(id));
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Insert([FromBody] InsertBlogCategoryDto insertBlogCategoryDto)
        {
            return Json(await blogCategoryService.Insert(insertBlogCategoryDto));
        }

        public async Task<IActionResult> Update([FromBody] UpdateBlogCategoryDto updateBlogCategoryDto)
        {
            return Json(await blogCategoryService.Update(updateBlogCategoryDto));
        }

        [HttpDelete]
        [Route("[action]/{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            return Json(await blogCategoryService.Delete(id));
        }
    }
}
