using Application.Dtos._HTTP;
using Application.Dtos._Pagination;
using Application.Services.BlogCategories.Dtos;
using Domain.Enumeration.BlogCategory;

namespace Application.Services.BlogCategories
{
    public interface IBlogCategoryService
    {
        Task<PaginatedItemsDto<BlogCategoriesListDto>> GetList(PaginationRequestDto<BlogCategorySearchDto, BlogCategorySortEnum> paginationRequestDto);
        Task<ResponseDto<bool>> Insert(InsertBlogCategoryDto insertBlogCategoryDto);
        Task<ResponseDto<bool>> Update(UpdateBlogCategoryDto updateBlogCategoryDto);
        Task<ResponseDto<bool>> Delete(Guid guid);
        Task<ResponseDto<BlogCategoriesItemDto>> GetItem(Guid guid);
    }
}
