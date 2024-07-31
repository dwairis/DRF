using System.Net;

namespace DRF.ViewModels
{
    public class JsonResponseMessage<T>
    {
        public HttpStatusCode Code { get; set; }
        public string Message { get; set; }
        public T Result { get; set; }

        public JsonResponseMessage()
        {

        }
        public JsonResponseMessage(HttpStatusCode code, string message, T result)
        {
            this.Code = code;
            this.Message = message;
            this.Result = result;
        }
    }
}
