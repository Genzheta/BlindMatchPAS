using System.ComponentModel.DataAnnotations;

namespace BlindMatchPAS.Core.Entities
{
    public class AuditLog
    {
        public int Id { get; set; }

        [Required]
        public string UserEmail { get; set; } = string.Empty;

        [Required]
        public string Action { get; set; } = string.Empty;

        [Required]
        public string Path { get; set; } = string.Empty;

        public string? Details { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        public int? StatusCode { get; set; }
        public long ElapsedMilliseconds { get; set; }
    }
}
