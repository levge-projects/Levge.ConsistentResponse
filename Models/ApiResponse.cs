using Levge.ConsistentResponse.Interfaces;

namespace Levge.ConsistentResponse.Models
{
    public class ApiResponse<T> : IApiResponse
    {
        public bool IsSuccess { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }
        public Dictionary<string, string[]>? Errors { get; set; }

        public static ApiResponse<T> Success(T data, string? message = null)
        {
            return new ApiResponse<T>
            {
                IsSuccess = true,
                Data = data,
                Message = string.IsNullOrEmpty(message) ? "Operation successful." : message
            };
        }

        public static ApiResponse<T> Fail(string message, Dictionary<string, string[]>? errors = null)
        {
            return new ApiResponse<T>
            {
                IsSuccess = false,
                Message = message,
                Errors = errors
            };
        }
    }
}
