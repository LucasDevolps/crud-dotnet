using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EstoqueApi.Application
{
    public sealed class RequestLog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public DateTime Timestamp { get; set; }
        public string RequestMethod { get; set; } = string.Empty;
        public string RequestPath { get; set; } = string.Empty;
        public string RequestBody { get; set; } = string.Empty;
        public string RequestHeaders { get; set; } = string.Empty;
        public int? StatusCode { get; set; }
        public string ResponseBody { get; set; } = string.Empty;
        public string ResponseHeaders { get; set; } = string.Empty; 
        public long? ResponseTimeMs { get; set; }
    }
}
