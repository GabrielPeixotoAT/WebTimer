using System.ComponentModel.DataAnnotations;

namespace WebTimer.Models
{
    public class Status
    {
        [Key]
        [Required]
        public int StatusId { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
