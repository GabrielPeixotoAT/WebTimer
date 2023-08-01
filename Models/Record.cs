using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace WebTimer.Models
{
    public class Record
    {
        [Key]
        [Required]
        public int RecordId { get; set; }
        [Required]
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        [Required]
        public int StatusId { get; set; }
        [Required]
        public Status Status { get; set; }
        [Required]
        public IdentityUser User { get; set; }
    }
}
