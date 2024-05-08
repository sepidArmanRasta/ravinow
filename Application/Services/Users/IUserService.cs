using Application.Dtos._HTTP;
using Application.Dtos._Pagination;
using Application.Services.Users.Dtos;
using Domain.Enumeration.Users;

namespace Application.Services.Users
{
    public interface IUserService
    {
        Task<ResponseDto<UserDto>> RegisterAsync(RegisterDto registerDto);
        Task<ResponseDto<bool>> LoginAsync(LoginDto loginDto);
        Task<ResponseDto<UserDto>> UpdateAsync(UpdateUserDto updateUserDto);
        Task<ResponseDto<UserDto>> GetItemAsync(Guid userId);
        Task<PaginatedItemsDto<UserDto>> GetListAsync(PaginationRequestDto<UserSearchDto, UserSortEnum> paginationRequestDto);
        Task<ResponseDto<bool>> DisableAsync(Guid userId);
        void Logout();
    }
}