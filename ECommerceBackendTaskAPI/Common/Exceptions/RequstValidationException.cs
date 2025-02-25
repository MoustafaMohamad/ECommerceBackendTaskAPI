namespace ECommerceBackendTaskAPI.Common.Exceptions
{
    public class RequstValidationException : Exception
    {
        public string Message { get; set; }
        public RequstValidationException(string message) : base(message)
        {
            Message = message;
        }


    }
}
