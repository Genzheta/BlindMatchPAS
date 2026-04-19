using System.ComponentModel.DataAnnotations;

namespace BlindMatchPAS.Core.Entities
{
    // Records important system actions for security and tracking
    public class AuditLog
    {
        // Primary key for the log entry
        public int Id { get; set; }

        [Required]
        // Email address of the user who performed the action
        public string UserEmail { get; set; } = string.Empty;

        [Required]
        // Description of the action performed
        public string Action { get; set; } = string.Empty;

        [Required]
        // The URL path where the action occurred
        public string Path { get; set; } = string.Empty;

        // Additional context or data associated with the action
        public string? Details { get; set; }

        // Date and time of the action
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        // HTTP status code associated with the response
        public int? StatusCode { get; set; }

        // Time taken for the action to complete in milliseconds
        public long ElapsedMilliseconds { get; set; }
    }
}
