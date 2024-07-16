using LeaveManagementSystem.Data;
using LeaveManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace LeaveManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DataContext _context;
        

        public HomeController(ILogger<HomeController> logger, DataContext context)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult Index()
        {
            var leaveRequests = _context.LeaveRequests.ToList();
            foreach (var leaveRequest in leaveRequests)
            {
                var leave = _context.LeaveRequests.FirstOrDefault(x=> x.Id == leaveRequest.Id);
                if(leave.Duration < DateTime.Now)
                {
                    var user = _context.Users.FirstOrDefault(x => x.Id == leaveRequest.UserId);
                    user.Onleave = false;
                }
            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
       
    }
}
