namespace ECommerceBackendTaskAPI.Common.ResultDtos
{
    public class ResultDto<T>
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
        public ErrorCode ErrorCode { get; set; }


        public static ResultDto<T> Sucess(T data, string message = "")
        {
            return new ResultDto<T>
            {
                IsSuccess = true,
                Data = data,
                Message = message,
                ErrorCode = ErrorCode.None,
            };
        }

        public static ResultDto<T> Faliure(ErrorCode errorCode, string message)
        {
            return new ResultDto<T>
            {
                IsSuccess = false,
                Data = default,
                Message = message,
                ErrorCode = errorCode,
            };
        }
    }
}
