namespace LeaveManagementSystem.ViewModel
{
    public class GetAllUserOnLeaveViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool OnLeave { get; set; }
        public string Reason { get; set; }
        public string Duration { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime LastUpdatedAt { get; set; } = DateTime.Now;
    }
}
