using Application.Services.Contents;
using Application.Services.Contents.Dto;
using Microsoft.AspNetCore.Mvc;

namespace WebArMa.Ravino.Ravino.WebApp.Controllers
{
    [Route("[controller]")]
    public class ContentController(IWebHostEnvironment webHostEnvironment, IContentService contentService) : Controller
    {
        [Route("{id}")]
        public IActionResult Index(string id)
        {
            var contentUrl = Path.Combine(webHostEnvironment!.ContentRootPath, $"Uploads/{id[..id.IndexOf('.')]}/{id}");

            if (!System.IO.File.Exists(contentUrl))
            {
                return NotFound();
            }

            byte[] file = System.IO.File.ReadAllBytes(contentUrl);

            return File(file, $"image/{id[(id.IndexOf('.') + 1)..]}");
        }

        [HttpPost]
        [Route("[action]")]
        public IActionResult InsertContent([FromBody] ContentDto contentDto)
        {
            return Json(contentService.Insert(contentDto));
        }
    }
}
