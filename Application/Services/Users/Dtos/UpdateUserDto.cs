using Application.Dtos._HTTP;
using System.Net;
using System.Text.RegularExpressions;

namespace Application.Services.Users.Dtos
{
    public class UpdateUserDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public bool IsAdmin { get; set; }
    }

    public static class UpdateUserDtoValidator
    {
        public static ResponseDto<bool> ModelIsValid(this UpdateUserDto updateUserDto)
        {
            var result = new ResponseDto<bool>();
            var nameRegexPattern = "^[\\u0600-\\u06FF\\s]+$";
            var phoneRegexPattern = "^09[0|1|2|3][0-9]{8}$";

            if (updateUserDto.Id == Guid.Empty)
            {
                result.SystemMessage = "شناسه کاربر نمی‌تواند خالی باشد";
                result.HttpStatusCode = HttpStatusCode.BadRequest;
                return result;
            }

            if (string.IsNullOrWhiteSpace(updateUserDto.FirstName))
            {
                result.SystemMessage = "نام نمی‌تواند خالی باشد";
                result.HttpStatusCode = HttpStatusCode.BadRequest;
                return result;
            }

            if (Regex.IsMatch(updateUserDto.FirstName, nameRegexPattern))
            {
                result.SystemMessage = "لطفا نام خود را به فارسی وارد کنید";
                result.HttpStatusCode = HttpStatusCode.BadRequest;
                return result;
            }

            if (string.IsNullOrWhiteSpace(updateUserDto.LastName))
            {
                result.SystemMessage = "نام خانوادگی نمی‌تواند خالی باشد";
                result.HttpStatusCode = HttpStatusCode.BadRequest;
                return result;
            }

            if (Regex.IsMatch(updateUserDto.LastName, nameRegexPattern))
            {
                result.SystemMessage = "لطفا نام خانوادگی خود را به فارسی وارد کنید";
                result.HttpStatusCode = HttpStatusCode.BadRequest;
                return result;
            }

            if (string.IsNullOrWhiteSpace(updateUserDto.PhoneNumber))
            {
                result.SystemMessage = "شماره تماس نمی‌تواند خالی باشد";
                result.HttpStatusCode = HttpStatusCode.BadRequest;
                return result;
            }

            if (Regex.IsMatch(updateUserDto.LastName, phoneRegexPattern))
            {
                result.SystemMessage = "شماره تلفن همراه وارد شده معتبر نمی‌باشد";
                result.HttpStatusCode = HttpStatusCode.BadRequest;
                return result;
            }

            result.Data = true;
            result.HttpStatusCode = HttpStatusCode.OK;
            return result;
        }
    }

}
