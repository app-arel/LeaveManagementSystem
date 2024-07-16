using System.ComponentModel.DataAnnotations;

namespace LeaveManagementSystem.ViewModel
{
    public class RegisterUserViewModel
    {
        [Required]
        [MaxLength(100)]
        public string FirstName { get; set;}
        [Required]
        [MaxLength(100)]
        public string LastName { get; set;}
        [Required]
        [MinLength(8)]
        [MaxLength(20)]
        public string Password { get; set;}
        [Required]
        [MaxLength(100)]
        [EmailAddress] 
        public string Email { get; set;}
        public int Position { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now; 
        public DateTime LastUpdatedAt { get; set;} = DateTime.Now;

    }
}
