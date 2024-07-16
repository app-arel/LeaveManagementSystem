using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeaveManagementSystem.Models
{
    [Index ("UserId", IsUnique = true)]
    public class LeaveRequest
    {
        public Guid Id { get; set; }
        public string Reason { get; set; } = string.Empty;
        public DateTime Duration { get; set; }
        public int DaysLeft { get; set; }
        public int AllowedDuration { get; set; }
        public Guid UserId { get; set; }
        [ForeignKey("UserId")]
        public User? User { get; set; }
        public bool Permitted { get; set; }

    }
}
