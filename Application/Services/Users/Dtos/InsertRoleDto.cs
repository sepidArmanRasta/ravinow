using Application.Dtos._HTTP;
using Domain.Enumeration.Users;
using System.Net;

namespace Application.Services.Users.Dtos
{
    public class InsertRoleDto
    {
        public string Name { get; set; } = string.Empty;
        public AccessTypeEnum AccessType { get; set; } = AccessTypeEnum.Default;
        public int AccessCode { get; set; }
    }

    public static class InsertRoleDtoValidator
    {
        public static ResponseDto<bool> ModelIsValid(this InsertRoleDto insertRoleDto)
        {
            var result = new ResponseDto<bool>();

            if (string.IsNullOrWhiteSpace(insertRoleDto.Name))
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
