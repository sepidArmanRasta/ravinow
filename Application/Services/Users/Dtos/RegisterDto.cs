using Application.Dtos._HTTP;
using System.Net;
using System.Text.RegularExpressions;

namespace Application.Services.Users.Dtos
{
    public class RegisterDto
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string RePassword { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public Guid CaptchaId { get; set; }
        public string CaptchaValue { get; set; } = string.Empty;
    }

    public static class RegisterDtoValidator
    {
        public static ResponseDto<bool> ModelIsValid(this RegisterDto loginDto)
        {
            var result = new ResponseDto<bool>();
            var nameRegexPattern = "^[\\u0600-\\u06FF\\s]+$";
            var phoneRegexPattern = "^09[0|1|2|3][0-9]{8}$";

            if (string.IsNullOrWhiteSpace(loginDto.FirstName))
            {
                result.SystemMessage = "نام نمی‌تواند خالی باشد";
                result.HttpStatusCode = HttpStatusCode.BadRequest;
                return result;
            }

            if (!Regex.IsMatch(loginDto.FirstName, nameRegexPattern))
            {
                result.SystemMessage = "لطفا نام خود را به فارسی وارد کنید";
                result.HttpStatusCode = HttpStatusCode.BadRequest;
                return result;
            }

            if (string.IsNullOrWhiteSpace(loginDto.LastName))
            {
                result.SystemMessage = "نام خانوادگی نمی‌تواند خالی باشد";
                result.HttpStatusCode = HttpStatusCode.BadRequest;
                return result;
            }

            if (!Regex.IsMatch(loginDto.LastName, nameRegexPattern))
            {
                result.SystemMessage = "لطفا نام خانوادگی خود را به فارسی وارد کنید";
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

            if (loginDto.Password != loginDto.RePassword)
            {
                result.SystemMessage = "رمز عبور و تکرار آن باید یکسان باشد";
                result.HttpStatusCode = HttpStatusCode.BadRequest;
                return result;
            }

            if (string.IsNullOrWhiteSpace(loginDto.CaptchaValue))
            {
                result.SystemMessage = "مقدار کد امنیتی نمی‌تواند خالی باشد";
                result.HttpStatusCode = HttpStatusCode.BadRequest;
                return result;
            }

            if (loginDto.CaptchaId == Guid.Empty)
            {
                result.SystemMessage = "شناسه کد امنیتی نمی‌تواند خالی باشد";
                result.HttpStatusCode = HttpStatusCode.BadRequest;
                return result;
            }

            result.Data = true;
            result.HttpStatusCode = HttpStatusCode.OK;
            return result;
        }
    }
}
