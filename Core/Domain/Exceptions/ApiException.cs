
using System.Net;

namespace BackendSpotify.Core.Domain.Exceptions;
public class ApiException : Exception
{
    public HttpStatusCode StatusCode { get; }

    public ApiException(string message) : base(message)
    {
        StatusCode = HttpStatusCode.InternalServerError;
    }

    public ApiException(string mensaje, HttpStatusCode statusCode) : base(mensaje)
    {
        StatusCode = statusCode;
    }
}

