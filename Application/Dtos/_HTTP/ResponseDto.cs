using Application.Tools;
using System.Net;

namespace Application.Dtos._HTTP
{
    public class ResponseDto<T>
    {
        public bool Success
        {
            get
            {
                if (HttpStatusCode >= HttpStatusCode.OK)
                {
                    return HttpStatusCode <= (HttpStatusCode)299;
                }

                return false;
            }
        }
        public HttpStatusCode HttpStatusCode { get; set; } = HttpStatusCode.InternalServerError;
        public string SystemMessage { get; set; } = string.Empty;
        public string Messages => HttpStatusCode.Translate();
        public T? Data { get; set; }
    }
}
