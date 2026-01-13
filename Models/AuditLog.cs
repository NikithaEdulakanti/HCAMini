using System;
using System.ComponentModel.DataAnnotations;

namespace HCAMiniEHR.Models
{
    public class AuditLog
    {
        [Key]
        public int AuditId { get; set; }

        [Required]
        [StringLength(50)]
        public string TableName { get; set; } = string.Empty;

        [Required]
        [StringLength(20)]
        public string Action { get; set; } = string.Empty;

        public int RecordId { get; set; }

        public string? OldValue { get; set; }

        public string? NewValue { get; set; }

        public DateTime ChangedAt { get; set; } = DateTime.Now;
    }
}
