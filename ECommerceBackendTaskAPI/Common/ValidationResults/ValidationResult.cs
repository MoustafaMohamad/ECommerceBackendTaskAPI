namespace ECommerceBackendTaskAPI.Common.ValidationResults
{
    public class ValidationResult<T> : Result
    {
        public T Data { get; set; }
        public Error[] Errors { get; set; }

        public static ValidationResult<T> WithErrors(Error[] errors)
        {
            return new ValidationResult<T>
            {
                IsSuccess = false,
                Errors = errors
            };
        }
    }


}
