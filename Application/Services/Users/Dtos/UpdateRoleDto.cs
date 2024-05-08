using Application.Dtos._HTTP;
using Domain.Enumeration.Users;
using System.Net;

namespace Application.Services.Users.Dtos
{
    public class UpdateRoleDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public AccessTypeEnum AccessType { get; set; } = AccessTypeEnum.Default;
        public int AccessCode { get; set; }
    }

    public static class UpdateRoleDtoValidator
    {
        public static ResponseDto<bool> ModelIsValid(this UpdateRoleDto updateRoleDto)
        {
            var result = new ResponseDto<bool>();

            if (string.IsNullOrWhiteSpace(updateRoleDto.Name))
            {
                result.SystemMessage = "نام نقش نمی‌تواند خالی باشد";
                result.HttpStatusCode = HttpStatusCode.BadRequest;
                return result;
            }

            result.Data = true;
            result.HttpStatusCode = HttpStatusCode.OK;
            return result;
        }
    }
}
