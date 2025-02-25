namespace ECommerceBackendTaskAPI.Common.Exceptions
{
    public enum ErrorCode
    {
        None = 200,                      // OK
        UnKnown = 500,                   // Internal Server Error
        BadRequest = 400,                   // Bad Request
        Unauthorized = 401,                 // Unauthorized
        NotFound = 404,                     // Not Found
        Conflict = 409,                     // Conflict
        InternalServerError = 500,

    }
}
