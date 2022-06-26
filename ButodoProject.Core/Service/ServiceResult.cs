namespace ButodoProject.Core.Service
{
    public class ServiceResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string Data { get; set; }
        public int StatusCode { get; set; }
    }

    public enum ResponseResultCode
    {
        None = 0,
        Success = 200,
        ValidationError = 400,
        NotAuthorize = 403,
        NotFound = 404,
        SystemError = 500
    }
}
