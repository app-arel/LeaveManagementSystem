using System.ComponentModel.DataAnnotations;

namespace LeaveManagementSystem.ViewModel
{
    public class LoginViewModel
    {
        [EmailAddress]
        public required string Email { get; set; }
        [MinLength (8)]
        public required string Password { get; set; }
    }
}
