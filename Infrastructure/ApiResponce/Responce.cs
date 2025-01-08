using System.Net;

namespace Infrastructure.ApiResponses;

public class Responce<T>
{
    public int StatusCode { get; set; }
    public T? Data { get; set; }
    public string Message { get; set; }

    public Responce(T date)
    {
        Data = date;
        StatusCode = 200;
    }

    public Responce(HttpStatusCode statusCode, string message)
    {
        StatusCode = (int)statusCode;
        Message = message;
        Data = default;
    }
}