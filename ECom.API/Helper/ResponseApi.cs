namespace ECom.API.Helper
{
    public class ResponseApi
    {
        public ResponseApi() { }  

        public ResponseApi(int statusCode, string message = null, string details = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetMessageByStatusCode(statusCode);
            Details = details;
        }

        public string GetMessageByStatusCode(int statusCode)
        {
            return statusCode switch
            {
                200 => "Success",
                201 => "Created Successfully",
                204 => "No Content",
                400 => "Bad Request",
                401 => "Unauthorized",
                403 => "Forbidden",
                404 => "Not Found",
                409 => "Conflict",
                500 => "Internal Server Error",
                _ => "Unknown Status"
            };
        }

        public int StatusCode { get; set; }

        public string? Message { get; set; }
        
        public string? Details { get; set; }

    }
}
