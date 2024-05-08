using Application.Dtos._HTTP;
using Application.Dtos._Pagination;
using Application.Services.Blogs.Dtos;
using Domain.Enumeration.Blog;

namespace Application.Services.Blogs
{
    public interface IBlogService
    {
        Task<PaginatedItemsDto<BlogListDto>> GetList(PaginationRequestDto<BlogSearchDto, BlogSortEnum> paginationRequestDto);
        Task<ResponseDto<bool>> Insert(InsertBlogDto insertBlogDto);
        Task<ResponseDto<BlogItemDto>> GetItem(string title);
        Task<ResponseDto<bool>> Delete(Guid guid);
    }
}
