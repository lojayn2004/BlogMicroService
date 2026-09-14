using Microsoft.AspNetCore.Mvc;

namespace BlogService.Dtos.Result
{
    public class ServiceResult<T>
    {
        public bool IsSuccess { get; set; }

        public string? Message { get; set; }

        public ResultType ResultType { get; set; }
        public T? Data {  get; set; }


        public static ServiceResult<T> Success(T data)
        {
            return new ServiceResult<T>()
            {
                IsSuccess = true,
                ResultType = ResultType.Ok,
                Data = data
            };
        }

        public static ServiceResult<T> Faliure(ResultType resultType, string message)
        {
            return new ServiceResult<T>()
            {
                IsSuccess = false,
                ResultType = resultType,
                Message = message
            };
        }


    }
}
