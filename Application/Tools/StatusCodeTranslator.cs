using Domain.Enumeration.Users;
using System.Net;

namespace Application.Tools
{
    public static class StatusCodeTranslator
    {
        public static string Translate(this HttpStatusCode httpStatusCode)
        {
            return httpStatusCode switch
            {
                HttpStatusCode.OK => "درخواست با موفقیت انجام شد",
                HttpStatusCode.Continue => "در انتظار ادامه درخواست",
                HttpStatusCode.SwitchingProtocols => "پروتکل تغییر کرد",
                HttpStatusCode.Processing => "در حال پردازش",
                HttpStatusCode.EarlyHints => "فرایند هنوز در حال اجراست",
                HttpStatusCode.Created => "با موفقیت ایجاد شد",
                HttpStatusCode.Accepted => "با موفقیت تایید شد",
                HttpStatusCode.NonAuthoritativeInformation => "اطلاعات غیر معتبر",
                HttpStatusCode.NoContent => "بدون محتوا",
                HttpStatusCode.ResetContent => "محتوا تنظیم مجدد شد",
                HttpStatusCode.PartialContent => "محتوای جزئی",
                HttpStatusCode.MultiStatus => "چند وضعیتی",
                HttpStatusCode.AlreadyReported => "اطلاعات تکراری",
                HttpStatusCode.IMUsed => "منبع در حال استفاده می\u200cباشد",
                HttpStatusCode.Ambiguous => "انتخاب\u200cهای چندگانه",
                HttpStatusCode.Moved => "منتقل شد",
                HttpStatusCode.Found => "یافت شد",
                HttpStatusCode.RedirectMethod => "در حال انتقال",
                HttpStatusCode.NotModified => "تغییر نیافته",
                HttpStatusCode.UseProxy => "استفاده از پراکسی",
                HttpStatusCode.RedirectKeepVerb => "موقتا منتقل شد",
                HttpStatusCode.PermanentRedirect => "به صورت دائمی منتقل شد",
                HttpStatusCode.BadRequest => "اطلاعات ورودی نادرست است",
                HttpStatusCode.Unauthorized => "دسترسی غیر مجاز",
                HttpStatusCode.PaymentRequired => "نیاز به پرداخت دارد",
                HttpStatusCode.Forbidden => "دسترسی ممنوع",
                HttpStatusCode.NotFound => "یافت نشد",
                HttpStatusCode.MethodNotAllowed => "متد مجاز نیست",
                HttpStatusCode.NotAcceptable => "قابل قبول نیست",
                HttpStatusCode.ProxyAuthenticationRequired => "نیاز به احراز هویت پراکسی دارد",
                HttpStatusCode.RequestTimeout => "زمان نشست به پایان رسید",
                HttpStatusCode.Conflict => "تضاد",
                HttpStatusCode.Gone => "از بین رفته",
                HttpStatusCode.LengthRequired => "حجم داده صحیح نمی\u200cباشد",
                HttpStatusCode.PreconditionFailed => "شرط پیش\u200cفرض انجام نشد",
                HttpStatusCode.RequestUriTooLong => "آدرس (URI) بیش از حد طولانی است",
                HttpStatusCode.UnsupportedMediaType => "نوع رسانه پشتیبانی نمی\u200cشود",
                HttpStatusCode.RequestedRangeNotSatisfiable => "محدوده قابل پذیرش نیست",
                HttpStatusCode.ExpectationFailed => "انتظار نامناسب",
                HttpStatusCode.MisdirectedRequest => "درخواست به اشتباه",
                HttpStatusCode.UnprocessableEntity => "موجودیت قابل پردازش نیست",
                HttpStatusCode.Locked => "قفل شده",
                HttpStatusCode.FailedDependency => "وابستگی ناموفق",
                HttpStatusCode.UpgradeRequired => "ارتقاء مورد نیاز است",
                HttpStatusCode.PreconditionRequired => "شرط پیش\u200cفرض مورد نیاز است",
                HttpStatusCode.TooManyRequests => "تعداد درخواست بیش از حد",
                HttpStatusCode.RequestHeaderFieldsTooLarge => "فیلدهای هدر درخواست بیش از حد بزرگ هستند",
                HttpStatusCode.UnavailableForLegalReasons => "به دلایل قانونی در دسترس نیست",
                HttpStatusCode.InternalServerError => "خطای داخلی سرور",
                HttpStatusCode.NotImplemented => "پیاده\u200cسازی نشده",
                HttpStatusCode.BadGateway => "دسترسی نادرست",
                HttpStatusCode.ServiceUnavailable => "سرویس در دسترس نیست",
                HttpStatusCode.GatewayTimeout => "زمان انتظار نشست به پایان رسید",
                HttpStatusCode.HttpVersionNotSupported => "نسخه HTTP پشتیبانی نمی\u200cشود",
                HttpStatusCode.VariantAlsoNegotiates => "درخواست نا معتبر",
                HttpStatusCode.InsufficientStorage => "فضای کافی وجود ندارد",
                HttpStatusCode.LoopDetected => "حلقه شناسایی شد",
                HttpStatusCode.NotExtended => "توسعه داده نشد",
                HttpStatusCode.NetworkAuthenticationRequired => "نیاز به احراز هویت شبکه دارد",
                HttpStatusCode.Unused => "بدون استفاده",
                HttpStatusCode.RequestEntityTooLarge => "حجم اطلاعات ارسالی بسیار زیاد است",
                _ => string.Empty,
            };
        }

        public static string Translate(this AccessTypeEnum accessTypeEnum)
        {
            return accessTypeEnum switch
            {
                AccessTypeEnum.Default => "دسترسی عادی",
                AccessTypeEnum.Manager => "مدیر",
                _ => string.Empty
            };
        }
    }
}