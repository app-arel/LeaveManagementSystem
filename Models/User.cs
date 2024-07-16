using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeaveManagementSystem.Models
{
    [Index ("Email", IsUnique = true)]
    public class User
    {
        public Guid Id { get; set; }
        [StringLength (50)]
        public required string FirstName { get; set; }
        [StringLength(50)]
        public required string LastName { get; set; }
        [StringLength(50)]
        [EmailAddress]
        public required string Email { get; set; }
        public required string HashedPassword { get; set; }
        public bool Onleave { get; set; } = false;
        [StringLength (150)]
        public string Reason { get; set; } = string.Empty;
        public DateTime Duration { get; set; } = DateTime.Now;
        public int AllowedDuration { get; set; } = 5;
        public int RemainingDurationAllowed { get; set; }
        public int Position { get; set; } = 1;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime LastUpdatedAt { get; set;} = DateTime.Now;
    }
}
