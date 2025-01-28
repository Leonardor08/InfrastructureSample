namespace Sample.Domain.Models
{
    public class Response<TResponse> : Response
    {
        public TResponse? Data { get; set; }
    }

    public class Response
    {
        public bool? Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public IEnumerable<Error>? Errors { get; set; }
    }
}
