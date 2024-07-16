using System.ComponentModel.DataAnnotations;

namespace LeaveManagementSystem.ViewModel
{
    public class LeaveViewModel
    {
        public string Email { get; set; }
        [Required]
        [MaxLength(150)]
        [MinLength(5)]
        public string Reason { get; set; } = string.Empty;
        public DateTime Day { get; set; }
    }
}
