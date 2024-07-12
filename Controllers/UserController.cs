using BCrypt.Net;
using LeaveManagementSystem.Data;
using LeaveManagementSystem.Mapper;
using LeaveManagementSystem.Models;
using LeaveManagementSystem.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace LeaveManagementSystem.Controllers
{
    public class UserController : Controller
    {
        private readonly DataContext _context;
        public UserController(DataContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> MakeUser(RegisterUserViewModel registerUser)
        {
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(registerUser.Password);
            var user = new User
            {
                Email = registerUser.Email,
                FirstName = registerUser.FirstName,
                LastName = registerUser.LastName,
                HashedPassword = hashedPassword
            };
            var get = await _context.Users.FirstOrDefaultAsync(x => x.Email == user.Email);
            if (get != null)
            {
                return View("Error");
            }
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return View("success");

        }
        /*
                public async Task<IActionResult> GetAllUsers()
                {
                    var all = await _context.Users.ToListAsync();
                    return Ok(all);
                }*/
        [HttpGet]
        public async Task<IActionResult> Leave()
        {
            return View("Leave");
        }

        [HttpPost]
        public async Task<IActionResult> MakeLeave(LeaveViewModel leaveViewModel)
        {
            var email = leaveViewModel.Email;
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
            if (user == null)
            {
                return BadRequest();
            }
            user.Reason = leaveViewModel.Reason;
            user.Duration = DateTime.Now.AddDays(leaveViewModel.Duration);
            if(user.Onleave == true)
            {
                if(leaveViewModel.Duration > 0)
                {
                    return View("OnLeave");
                }
            }
            if (user.Duration.Day > 0)
            {
                user.Onleave = true;
            }
            if (user.Position ==1 )
            {
                user.AllowedDuration = 5;
                user.RemainingDurationAllowed = user.AllowedDuration - leaveViewModel.Duration;
            }
            await _context.SaveChangesAsync();
            return View("Success");
        }
        public ActionResult GetUsers()
        {
            var users = _context.Users.ToList();
            return View(users);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
