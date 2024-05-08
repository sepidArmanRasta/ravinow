using Application.Dtos._HTTP;
using System.Net;
using System.Text.RegularExpressions;

namespace Application.Services.Users.Dtos
{
    public class LoginDto
    {
        public string PhoneNumber { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public bool IsPersistent { get; set; } = true;
        public int? Code { get; set; }
        public Guid CaptchaId { get; set; }
        public string CaptchaValue { get; set; } = string.Empty;
    }

    public static class LoginDtoValidator
    {
        public static ResponseDto<bool> ModelIsValid(this LoginDto loginDto)
        {
            var result = new ResponseDto<bool>();
            var phoneRegexPattern = "^09[0|1|2|3][0-9]{8}$";

            if (loginDto == null)
            {
                result.SystemMessage = "داده‌ای دریافت نشد";
                result.HttpStatusCode = HttpStatusCode.BadRequest;
                return result;
            }

            if (string.IsNullOrWhiteSpace(loginDto.PhoneNumber))
            {
                result.SystemMessage = "شماره تماس نمی‌تواند خالی باشد";
                result.HttpStatusCode = HttpStatusCode.BadRequest;
                return result;
            }

            if (!Regex.IsMatch(loginDto.PhoneNumber, phoneRegexPattern))
            {
                result.SystemMessage = "شماره تلفن همراه وارد شده معتبر نمی‌باشد";
                result.HttpStatusCode = HttpStatusCode.BadRequest;
                return result;
            }

            if (string.IsNullOrWhiteSpace(loginDto.Password))
            {
                result.SystemMessage = "رمز عبور نمی‌تواند خالی باشد";
                result.HttpStatusCode = HttpStatusCode.BadRequest;
                return result;
            }

            if (loginDto.CaptchaId == Guid.Empty)
            {
                result.SystemMessage = "شناسه کد امنیتی نمی‌تواند خالی باشد";
                result.HttpStatusCode = HttpStatusCode.BadRequest;
                return result;
            }

            if (string.IsNullOrWhiteSpace(loginDto.CaptchaValue))
            {
                result.SystemMessage = "کد امنیتی نمی‌تواند خالی باشد";
                result.HttpStatusCode = HttpStatusCode.BadRequest;
                return result;
            }

            result.Data = true;
            result.HttpStatusCode = HttpStatusCode.OK;
            return result;
        }
    }

}
