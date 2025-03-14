
using Sample.Domain.CustomAttributes;

namespace Sample.Domain.Models
{
    [EntityName("Error")]
    public class ErrorLog : IEntity<Guid>
    {
        public string Request { get; set; } = string.Empty;
        public string LogException { get; set; } = string.Empty;
        public string StackTrace { get; set; } = string.Empty;
        public Guid Id { get ; set ; }
        public DateTime CreatedDate { get ; set ; }
        public DateTime? UpdateDate { get ; set ; }
    }
}
