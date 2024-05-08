using Application.Contexts;
using Application.Dtos._HTTP;
using Application.Services.Users.Dtos;
using Domain.Entities.Captchas;
using Microsoft.AspNetCore.Hosting;
using SixLabors.Fonts;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using System.Net;

namespace Application.Services.Captchas
{
    public class CaptchaService(IWebHostEnvironment webHostEnvironment, IDataBaseContext dataBaseContext) : ICaptchaService
    {
        public ResponseDto<bool> ValidateCaptcha(Guid id, string value)
        {
            var result = new ResponseDto<bool>();

            var captcha = dataBaseContext.Captchas.Where(c => c.Id == id).FirstOrDefault();

            if (captcha == null || captcha.Value != value)
            {
                result.HttpStatusCode = HttpStatusCode.NotFound;
                result.SystemMessage = "کد امنیتی وارد شده صحیح نمی‌باشد";
                return result;
            }

            dataBaseContext.Captchas.Remove(captcha);
            dataBaseContext.SaveChanges();

            result.HttpStatusCode = HttpStatusCode.OK;
            return result;
        }

        public ResponseDto<CaptchaDto> Generate()
        {
            string captchaText = GenerateRandomText();

            string basePath = webHostEnvironment.WebRootPath;
            string baseImagePath = Path.Combine(basePath, "assets/images/default/captcha.png");
            string baseFontPath = Path.Combine(basePath, "assets/fonts/captcha/recaptchaFont.ttf");

            FontCollection collection = new();
            FontFamily family = collection.Add(baseFontPath);
            Font font = family.CreateFont(25, FontStyle.Italic);

            using Image<Rgba32> baseImage = Image.Load<Rgba32>(baseImagePath);
            baseImage.Mutate(ctx => ctx
                .DrawText(captchaText, font, Color.Black, new PointF(7, 5)));

            using MemoryStream ms = new();
            baseImage.SaveAsPng(ms);
            byte[] imageBytes = ms.ToArray();

            ms.Close();

            var model = new Captcha
            {
                Id = Guid.NewGuid(),
                ValidTime = DateTime.Now.AddMinutes(5),
                Value = captchaText
            };

            dataBaseContext.Captchas.Add(model);

            dataBaseContext.SaveChanges();

            var base64Captcha = Convert.ToBase64String(imageBytes);

            var result = new ResponseDto<CaptchaDto>()
            {
                Data = new CaptchaDto { Id = model.Id, Captcha = base64Captcha },
                HttpStatusCode = HttpStatusCode.OK,
            };

            return result;
        }

        private static string GenerateRandomText()
        {
            Random random = new();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, 4)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
