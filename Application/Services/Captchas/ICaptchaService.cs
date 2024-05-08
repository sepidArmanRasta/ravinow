using Application.Dtos._HTTP;
using Application.Services.Users.Dtos;

namespace Application.Services.Captchas
{
    public interface ICaptchaService
    {
        ResponseDto<bool> ValidateCaptcha(Guid id, string value);
        ResponseDto<CaptchaDto> Generate();
    }
}
