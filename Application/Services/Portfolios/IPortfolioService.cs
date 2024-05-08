using Application.Dtos._HTTP;
using Application.Services.Blogs.Dtos;

namespace Application.Services.Blogs
{
    public interface IPortfolioService
    {
        Task<List<BlogListDto>> GetList();
        Task<ResponseDto<bool>> Insert(InsertPortfolioDto insertPortfolioDto);
        Task<ResponseDto<bool>> Delete(Guid guid);
    }
}
