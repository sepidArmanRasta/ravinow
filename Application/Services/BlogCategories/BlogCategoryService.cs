using Application.Contexts;
using Application.Dtos._HTTP;
using Application.Dtos._Pagination;
using Application.Services.BlogCategories.Dtos;
using Application.Services.UploadImages;
using Application.Tools;
using Domain.Entities.Blogs;
using Domain.Enumeration._Pagination;
using Domain.Enumeration.BlogCategory;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Application.Services.BlogCategories
{
    public class BlogCategoryService(IDataBaseContext dataBaseContext, IUploadImageService uploadImageService) : IBlogCategoryService
    {
        public async Task<ResponseDto<BlogCategoriesItemDto>> GetItem(Guid guid)
        {
            var result = new ResponseDto<BlogCategoriesItemDto>();

            try
            {
                var item = await dataBaseContext.BlogCategories.FindAsync(guid);

                if (item != null)
                {
                    result.HttpStatusCode = HttpStatusCode.BadRequest;
                    return result;
                }
                var mappedData = MappingHelper.Map<BlogCategoriesItemDto>(item);

                result.HttpStatusCode = HttpStatusCode.OK;
                result.Data = mappedData;
                return result;
            }
            catch
            {
                throw;
            }
        }

        public async Task<PaginatedItemsDto<BlogCategoriesListDto>> GetList(PaginationRequestDto<BlogCategorySearchDto, BlogCategorySortEnum> paginationRequestDto)
        {
            try
            {
                var entity = dataBaseContext.BlogCategories.AsQueryable();

                if (paginationRequestDto != null)
                {
                    if (paginationRequestDto.Search != null)
                    {
                        if (paginationRequestDto.Search.Id != null)
                        {
                            entity = entity.Where(b => b.Id == paginationRequestDto.Search.Id).AsQueryable();
                        }

                        if (paginationRequestDto.Search.StringSearch != null)
                        {
                            entity = entity.Where(b => b.Name.Contains(paginationRequestDto.Search.StringSearch));
                        }

                        if (paginationRequestDto.Search.ParentCategoryId != null)
                        {
                            entity = entity.Include(b => b.ParentCategory).Where(b => b.ParentCategory != null && b.ParentCategory.Id == paginationRequestDto.Search.ParentCategoryId);
                        }
                    }

                    switch (paginationRequestDto.Sort)
                    {
                        case BlogCategorySortEnum.Name:
                            entity = paginationRequestDto.SortType == SortType.Ascending ? entity.OrderBy(b => b.Name).AsQueryable() : entity.OrderByDescending(b => b.Name).AsQueryable(); break;
                    }
                }

                var data = await entity.PagedResult(paginationRequestDto?.Page ?? 0, paginationRequestDto?.PageSize ?? 0, out int totalCount).ToListAsync();

                var mappedData = MappingHelper.Map<List<BlogCategoriesListDto>>(data);

                var result = new PaginatedItemsDto<BlogCategoriesListDto>
                {
                    PageIndex = paginationRequestDto?.Page ?? 1,
                    PageSize = paginationRequestDto?.PageSize ?? mappedData.Count,
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

        public async Task<ResponseDto<bool>> Delete(Guid guid)
        {
            var result = new ResponseDto<bool>();

            try
            {
                var item = dataBaseContext.BlogCategories.Find(guid);

                if (item == null)
                {
                    result.HttpStatusCode = HttpStatusCode.BadRequest;
                    return result;
                }
                else
                {
                    dataBaseContext.BlogCategories.Remove(item);
                    await dataBaseContext.SaveChangesAsync();

                    result.HttpStatusCode = HttpStatusCode.OK;
                    result.Data = true;
                    return result;
                }
            }
            catch
            {
                throw;
            }
        }

        public async Task<ResponseDto<bool>> Update(UpdateBlogCategoryDto updateBlogCategoryDto)
        {
            var result = new ResponseDto<bool>();

            try
            {
                var item = dataBaseContext.BlogCategories.Find(updateBlogCategoryDto.Id);

                if (item == null)
                {
                    result.HttpStatusCode = HttpStatusCode.BadRequest;
                    return result;
                }

                var parentCategory = dataBaseContext.BlogCategories.Find(updateBlogCategoryDto.ParentCategoryId);

                if (parentCategory == null)
                {
                    result.HttpStatusCode = HttpStatusCode.BadRequest;
                    return result;
                }

                item.Description = updateBlogCategoryDto.Description;
                item.ParentCategory = parentCategory;

                result.HttpStatusCode = HttpStatusCode.OK;
                result.Data = true;

                dataBaseContext.BlogCategories.Update(item);
                await dataBaseContext.SaveChangesAsync();
                return result;
            }
            catch
            {
                throw;
            }
        }

        [Authorize(Roles = "Admin")]
        public async Task<ResponseDto<bool>> Insert(InsertBlogCategoryDto insertBlogCategoryDto)
        {
            var result = new ResponseDto<bool>();

            try
            {
                var spliter = ",";

                var itemCheck = dataBaseContext.BlogCategories.Any(b => b.Name == insertBlogCategoryDto.Name);

                if (itemCheck)
                {
                    result.HttpStatusCode = HttpStatusCode.BadRequest;
                    result.SystemMessage = "نام وارد شده از قبل وجود دارد";
                    return result;
                }

                var photoCorrection = insertBlogCategoryDto.Photo[(insertBlogCategoryDto.Photo.IndexOf(spliter) + 1)..];
                byte[] byteArray = Convert.FromBase64String(photoCorrection);
                var imageUri = uploadImageService.UploadImage(byteArray, insertBlogCategoryDto.FileName);

                BlogCategory? parent = null;

                if (insertBlogCategoryDto.ParentCategoryId != null)
                {
                    parent = dataBaseContext.BlogCategories.Where(b => b.Id == insertBlogCategoryDto.ParentCategoryId).FirstOrDefault();
                }

                if (parent == null && insertBlogCategoryDto.ParentCategoryId != null)
                {
                    result.HttpStatusCode = HttpStatusCode.BadRequest;
                    return result;
                }
                else
                {
                    var item = new BlogCategory
                    {
                        Name = insertBlogCategoryDto.Name,
                        Description = insertBlogCategoryDto.Description,
                        Photo = imageUri?.Replace("\\", "/"),
                        ParentCategory = parent
                    };

                    dataBaseContext.BlogCategories.Add(item);
                    await dataBaseContext.SaveChangesAsync();

                    result.HttpStatusCode = HttpStatusCode.Created;
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