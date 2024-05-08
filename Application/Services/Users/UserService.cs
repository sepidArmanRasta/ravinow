using Application.Contexts;
using Application.Dtos._HTTP;
using Application.Dtos._Pagination;
using Application.Services.Captchas;
using Application.Services.Users.Dtos;
using Application.Tools;
using Domain.Entities.Users;
using Domain.Enumeration._Pagination;
using Domain.Enumeration.Users;
using Microsoft.AspNetCore.Identity;
using System.Net;
using System.Security.Claims;

namespace Application.Services.Users
{
    public class UserService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ICaptchaService captchaService, IDataBaseContext dataBaseContext) : IUserService
    {
        public async Task<ResponseDto<UserDto>> RegisterAsync(RegisterDto registerDto)
        {
            var result = new ResponseDto<UserDto>();

            var modelCheck = registerDto.ModelIsValid();

            if (!modelCheck.Success)
            {
                result.HttpStatusCode = modelCheck.HttpStatusCode;
                result.SystemMessage = modelCheck.SystemMessage;
                result.Data = null;
                return result;
            }

            var validateCaptcha = captchaService.ValidateCaptcha(registerDto.CaptchaId, registerDto.CaptchaValue);

            if (!validateCaptcha.Success)
            {
                result.HttpStatusCode = validateCaptcha.HttpStatusCode;
                result.SystemMessage = validateCaptcha.SystemMessage;
                result.Data = null;
                return result;
            }

            var model = new ApplicationUser
            {
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                PhoneNumber = registerDto.PhoneNumber,
                UserName = registerDto.PhoneNumber,
                PhoneNumberConfirmed = false,
                InsertTime = DateTime.UtcNow,
            };

            var userCreateResult = await userManager.CreateAsync(model, registerDto.Password);
            var user = await userManager.FindByNameAsync(model.UserName);

            if (userCreateResult != null && user != null && userCreateResult.Succeeded)
            {
                await signInManager.SignInAsync(user, true);

                var mappedUser = MappingHelper.Map<UserDto>(user);
                result.Data = mappedUser;

                result.HttpStatusCode = HttpStatusCode.Created;
                return result;
            }
            else
            {
                result.HttpStatusCode = HttpStatusCode.BadRequest;
                result.SystemMessage = userCreateResult?.Errors.FirstOrDefault()?.Description ?? string.Empty;
            }


            return result;
        }

        public async Task<ResponseDto<bool>> LoginAsync(LoginDto loginDto)
        {
            var result = new ResponseDto<bool>();

            var ModelIsValid = loginDto.ModelIsValid();

            if (!ModelIsValid.Success)
            {
                result.HttpStatusCode = ModelIsValid.HttpStatusCode;
                result.SystemMessage = ModelIsValid.SystemMessage;
                result.Data = false;
                return result;
            }

            var validateCaptcha = captchaService.ValidateCaptcha(loginDto.CaptchaId, loginDto.CaptchaValue);

            if (!validateCaptcha.Success)
            {
                result.HttpStatusCode = validateCaptcha.HttpStatusCode;
                result.SystemMessage = validateCaptcha.SystemMessage;
                result.Data = false;
                return result;
            }

            try
            {
                var user = await userManager.FindByNameAsync(loginDto.PhoneNumber);

                if (user == null)
                {
                    result.HttpStatusCode = HttpStatusCode.BadRequest;
                    result.Data = false;
                    result.SystemMessage = "کاربر یافت نشد";
                    return result;
                }

                var passwordIsValid = await userManager.CheckPasswordAsync(user, loginDto.Password);
                var userInfoCheck = await signInManager.CheckPasswordSignInAsync(user, loginDto.Password, true);

                if (userInfoCheck.Succeeded)
                {
                    await signInManager.SignInAsync(user, loginDto.IsPersistent);

                    result.HttpStatusCode = HttpStatusCode.OK;
                    return result;
                }

                result.HttpStatusCode = HttpStatusCode.BadRequest;
                result.Data = true;
                return result;
            }
            catch
            {
                throw;
            }
        }

        public async Task<PaginatedItemsDto<UserDto>> GetListAsync(PaginationRequestDto<UserSearchDto, UserSortEnum> paginationRequestDto)
        {
            var result = new PaginatedItemsDto<UserDto>();

            if (paginationRequestDto.Search != null && paginationRequestDto.Search.CurrentUserId != null && paginationRequestDto.Search.CurrentUserId != Guid.Empty)
            {
                if (paginationRequestDto.Search == null)
                {
                    result.HttpStatusCode = HttpStatusCode.BadRequest;
                    return result;
                }

                var user = await userManager.FindByIdAsync(paginationRequestDto.Search.CurrentUserId.ToString()!);

                if (user == null)
                {
                    result.HttpStatusCode = HttpStatusCode.BadRequest;
                    return result;
                }

                var userRoles = await userManager.GetRolesAsync(user);
                var guidUserRolesResultCheck = userRoles.Select(ur => Guid.TryParse(ur, out _)).ToList();

                if (guidUserRolesResultCheck.Contains(false))
                {
                    result.HttpStatusCode = HttpStatusCode.BadRequest;
                    return result;
                }

                var guidUserRoles = userRoles.Select(ur => Guid.Parse(ur)).ToList();
            }

            var entity = dataBaseContext.ApplicationUsers.AsQueryable();

            if (paginationRequestDto.Search != null)
            {
                if (!string.IsNullOrWhiteSpace(paginationRequestDto.Search.FirstName))
                {
                    entity = entity.Where(u => u.FirstName.Contains(paginationRequestDto.Search.FirstName));
                }

                if (!string.IsNullOrWhiteSpace(paginationRequestDto.Search.LastName))
                {
                    entity = entity.Where(u => u.LastName.Contains(paginationRequestDto.Search.LastName));
                }

                if (!string.IsNullOrWhiteSpace(paginationRequestDto.Search.UserName))
                {
                    entity = entity.Where(u => u.UserName == paginationRequestDto.Search.UserName);
                }

                if (!string.IsNullOrWhiteSpace(paginationRequestDto.Search.PhoneNumber))
                {
                    entity = entity.Where(u => u.PhoneNumber!.Contains(paginationRequestDto.Search.PhoneNumber));
                }

                if (paginationRequestDto.Search.ShowAdmins != null)
                {
                    entity = entity.Where(u => u.IsAdmin == paginationRequestDto.Search.ShowAdmins);
                }

                if (paginationRequestDto.Search.InsertTimeFrom != null)
                {
                    entity = entity.Where(u => u.InsertTime >= paginationRequestDto.Search.InsertTimeFrom);
                }

                if (paginationRequestDto.Search.InsertTimeTo != null)
                {
                    entity = entity.Where(u => u.InsertTime >= paginationRequestDto.Search.InsertTimeTo);
                }
            }

            switch (paginationRequestDto.Sort)
            {
                case UserSortEnum.FirstName: entity = paginationRequestDto.SortType == SortType.Ascending ? entity.OrderBy(u => u.FirstName) : entity.OrderByDescending(u => u.FirstName); break;
                case UserSortEnum.LastName: entity = paginationRequestDto.SortType == SortType.Ascending ? entity.OrderBy(u => u.LastName) : entity.OrderByDescending(u => u.LastName); break;
                case UserSortEnum.FullName: entity = paginationRequestDto.SortType == SortType.Ascending ? entity.OrderBy(u => u.FirstName) : entity.OrderByDescending(u => u.FirstName); break;
                case UserSortEnum.InsertTime: entity = entity = paginationRequestDto.SortType == SortType.Ascending ? entity.OrderBy(u => u.InsertTime) : entity.OrderByDescending(u => u.InsertTime); break;
            }

            var data = entity.PagedResult(paginationRequestDto.Page, paginationRequestDto.PageSize, out int totalCount).ToList();

            var mappedData = MappingHelper.Map<List<UserDto>>(data);

            result = new PaginatedItemsDto<UserDto>
            {
                Count = totalCount,
                Data = mappedData,
                HttpStatusCode = HttpStatusCode.OK,
                PageIndex = paginationRequestDto.Page,
                PageSize = paginationRequestDto.PageSize
            };

            return result;
        }

        public async Task<ResponseDto<UserDto>> UpdateAsync(UpdateUserDto updateUserDto)
        {
            var result = new ResponseDto<UserDto>();

            var modelCheck = updateUserDto.ModelIsValid();

            if (!modelCheck.Success)
            {
                result.HttpStatusCode = modelCheck.HttpStatusCode;
                result.SystemMessage = modelCheck.SystemMessage;
                result.Data = null;
                return result;
            }

            var user = userManager.Users.Where(u => u.Id == updateUserDto.Id).FirstOrDefault();

            if (user == null)
            {
                result.HttpStatusCode = HttpStatusCode.NotFound;
                return result;
            }

            user.PhoneNumber = updateUserDto.PhoneNumber;
            user.FirstName = updateUserDto.FirstName;
            user.LastName = updateUserDto.LastName;
            user.IsAdmin = updateUserDto.IsAdmin;

            var claims = new List<Claim>();

            var response = await userManager.UpdateAsync(user);

            var userClaims = await userManager.GetClaimsAsync(user);

            if (response.Succeeded)
            {
                result.Data = MappingHelper.Map<UserDto>(user);
                result.HttpStatusCode = HttpStatusCode.OK;
                return result;
            }

            result.SystemMessage = response.Errors.FirstOrDefault()?.Description ?? string.Empty;
            return result;
        }

        public async Task<ResponseDto<UserDto>> GetItemAsync(Guid id)
        {
            var result = new ResponseDto<UserDto>();
            var user = await userManager.FindByIdAsync(id.ToString());

            await Task.CompletedTask;

            if (user == null)
            {
                result.HttpStatusCode = HttpStatusCode.NotFound;
                return result;
            }

            var mappedUser = MappingHelper.Map<UserDto>(user);

            result.Data = mappedUser;
            result.HttpStatusCode = HttpStatusCode.OK;
            return result;
        }

        public async Task<ResponseDto<bool>> DisableAsync(Guid userId)
        {
            var result = new ResponseDto<bool>();
            var user = dataBaseContext.ApplicationUsers.Where(u => u.Id == userId).FirstOrDefault();

            await Task.CompletedTask;

            if (user == null)
            {
                result.HttpStatusCode = HttpStatusCode.NotFound;
                return result;
            }

            dataBaseContext.ApplicationUsers.Remove(user);
            dataBaseContext.SaveChanges();

            result.Data = true;
            result.HttpStatusCode = HttpStatusCode.OK;
            return result;
        }

        public void Logout()
        {
            signInManager.SignOutAsync().Wait();
        }
    }
}