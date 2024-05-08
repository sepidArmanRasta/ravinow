using Application.Contexts;
using Application.Dtos._HTTP;
using Application.Dtos._Pagination;
using Application.Services.Blogs.Dtos;
using Application.Services.UploadImages;
using Application.Tools;
using Domain.Entities.Blogs;
using Domain.Enumeration._Pagination;
using Domain.Enumeration.Blog;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Application.Services.Blogs
{
    public class BlogService(IDataBaseContext dataBaseContext, IUploadImageService uploadImageService) : IBlogService
    {
        public async Task<ResponseDto<bool>> Delete(Guid guid)
        {
            var result = new ResponseDto<bool>();

            try
            {
                var item = dataBaseContext.Blogs.Find(guid);

                if (item != null)
                {
                    dataBaseContext.Blogs.Remove(item);
                    await dataBaseContext.SaveChangesAsync();
                }

                result.HttpStatusCode = HttpStatusCode.OK;
                return result;
            }
            catch
            {
                throw;
            }
        }

        public async Task<ResponseDto<BlogItemDto>> GetItem(string title)
        {
            var result = new ResponseDto<BlogItemDto>();

            try
            {
                var item = await dataBaseContext.Blogs.Where(b => b.Title == title).FirstOrDefaultAsync();

                if (item == null)
                {
                    result.HttpStatusCode = HttpStatusCode.BadRequest;
                    return result;
                }
                else
                {
                    var mappedData = MappingHelper.Map<BlogItemDto>(item);

                    result.HttpStatusCode = HttpStatusCode.OK;
                    result.Data = mappedData;

                    return result;
                }
            }
            catch
            {
                throw;
            }
        }

        public async Task<PaginatedItemsDto<BlogListDto>> GetList(PaginationRequestDto<BlogSearchDto, BlogSortEnum> paginationRequestDto)
        {
            try
            {
                var entity = dataBaseContext.Blogs.Include(b => b.Categories).AsQueryable();

                if (paginationRequestDto.Search != null)
                {
                    if (paginationRequestDto.Search.StringSearch != null)
                    {
                        entity = entity.Where(b => b.Title.Contains(paginationRequestDto.Search.StringSearch) || b.Description.Contains(paginationRequestDto.Search.StringSearch));
                    }

                    if (paginationRequestDto.Search.InsertTimeFrom != null)
                    {
                        entity = entity.Where(b => b.InsertTime >= paginationRequestDto.Search.InsertTimeFrom);
                    }

                    if (paginationRequestDto.Search.InsertTimeTo != null)
                    {
                        entity = entity.Where(b => b.InsertTime <= paginationRequestDto.Search.InsertTimeTo);
                    }

                    if (paginationRequestDto.Search.AuthorId != null)
                    {
                        entity = entity.Include(b => b.Author).Where(b => b.Author.Id == paginationRequestDto.Search.AuthorId);
                    }

                    if (paginationRequestDto.Search.CategoryId != null)
                    {
                        entity = entity.Include(b => b.Categories).Where(b => b.Categories != null && b.Categories.Any(x => x.Id == paginationRequestDto.Search.CategoryId));
                    }

                    if (paginationRequestDto.Search.Tags != null)
                    {
                        entity = entity.Where(b => b.Tags != null && b.Tags.Contains(paginationRequestDto.Search.Tags)).AsQueryable();
                    }
                }

                switch (paginationRequestDto.Sort)
                {
                    case BlogSortEnum.PublishDate: entity = paginationRequestDto.SortType == SortType.Ascending ? entity.OrderBy(b => b.InsertTime).AsQueryable() : entity.OrderByDescending(b => b.InsertTime).AsQueryable(); break;
                    case BlogSortEnum.UpdateDate: entity = paginationRequestDto.SortType == SortType.Ascending ? entity.OrderBy(b => b.UpdateTime).AsQueryable() : entity.OrderByDescending(b => b.UpdateTime).AsQueryable(); break;
                    case BlogSortEnum.Title: entity = paginationRequestDto.SortType == SortType.Ascending ? entity.OrderBy(b => b.Title).AsQueryable() : entity.OrderByDescending(b => b.Title).AsQueryable(); break;
                }

                var data = await entity.PagedResult(paginationRequestDto.Page, paginationRequestDto.PageSize, out int totalCount).ToListAsync();
                var mappedData = MappingHelper.Map<List<BlogListDto>>(data);

                var result = new PaginatedItemsDto<BlogListDto>
                {
                    PageIndex = paginationRequestDto.Page,
                    PageSize = paginationRequestDto.PageSize,
                    Count = totalCount,
                    Data = mappedData,
                    HttpStatusCode = HttpStatusCode.OK
                };

                return result;
            }
            catch
            {
                throw;
            }


        }

        public async Task<ResponseDto<bool>> Insert(InsertBlogDto insertBlogDto)
        {
            var result = new ResponseDto<bool>();

            try
            {
                var spliter = ",";
                var photoCorrection = insertBlogDto.Photo[(insertBlogDto.Photo.IndexOf(spliter) + 1)..];
                byte[] byteArray = Convert.FromBase64String(photoCorrection);
                var imageUri = uploadImageService.UploadImage(byteArray, insertBlogDto.FileName);

                var itemExists = dataBaseContext.Blogs.Any(b => b.Title == insertBlogDto.Title);

                if (itemExists)
                {
                    result.HttpStatusCode = HttpStatusCode.BadRequest;
                    return result;
                }
                else
                {
                    var category = dataBaseContext.BlogCategories.Where(b => insertBlogDto.CategoryId == b.Id).ToList();

                    if (category == null)
                    {
                        result.HttpStatusCode = HttpStatusCode.BadRequest;
                        return result;
                    }

                    var user = dataBaseContext.ApplicationUsers.Where(u => u.Id == insertBlogDto.AuthorId).FirstOrDefault();

                    if (user == null)
                    {
                        result.HttpStatusCode = HttpStatusCode.Unauthorized;
                        return result;
                    }

                    var item = new Blog
                    {
                        Body = insertBlogDto.Body,
                        Categories = category,
                        Description = insertBlogDto.Description,
                        Photo = imageUri?.Replace("\\", "/"),
                        Title = insertBlogDto.Title,
                        Tags = insertBlogDto.Tags,
                        Author = user!,
                    };

                    dataBaseContext.Blogs.Add(item);
                    await dataBaseContext.SaveChangesAsync();

                    result.HttpStatusCode = HttpStatusCode.Created;
                    result.Data = true;
                    return result;
                }
            }
            catch
            {
                throw;
            }
        }
    }
}