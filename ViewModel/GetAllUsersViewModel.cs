using System.ComponentModel.DataAnnotations;

namespace LeaveManagementSystem.ViewModel
{
    public class GetAllUsersViewodel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool OnLeave { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime LastUpdatedAt { get; set; } = DateTime.Now;
    }
}
