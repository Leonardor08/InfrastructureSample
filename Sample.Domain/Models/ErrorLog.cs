namespace Sample.Domain.Models
{
    public class ErrorLog : Entity
    {
        public override Guid Id { get; set; }
        public string Request { get; set; } = string.Empty;
        public string LogException { get; set; } = string.Empty;
        public string StackTrace { get; set; } = string.Empty;
    }
}
