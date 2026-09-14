using System.Net;

namespace shoe_shop_backend.Application.ResquestResponse
{
    public class ApiResponse<T>
    {
        public int Status { get; set; }
        public string Message { get; set; } = string.Empty;
        public T? Data { get; set; }

        public static ApiResponse<T> Success(T data, string message = "Success")
        {
            return new ApiResponse<T> { Status = (int)HttpStatusCode.OK, Message = message, Data = data };
        }

        public static ApiResponse<T> Fail(string message, HttpStatusCode statusCode = HttpStatusCode.BadRequest, T? data = default)
        {
            return new ApiResponse<T> { Status = (int)statusCode, Message = message, Data = data };
        }

        // ---------- IMPLICIT CONVERSION ----------
        public static implicit operator ApiResponse<T>(T data)
        {
            return Success(data);
        }
    }
}

