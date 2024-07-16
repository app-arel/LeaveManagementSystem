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
            return View("Register");
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
                HashedPassword = hashedPassword,
                Position = registerUser.Position
            };
            var get = await _context.Users.FirstOrDefaultAsync(x => x.Email == user.Email);
            if (get != null)
            {
                return View("Error");
            }
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return View("~/Views/Home/Index.cshtml");
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
            var userId = user.Id;
            var Leave = new LeaveRequest();
            Leave.Id = Leave.Id;
            Leave.UserId = userId;
            Leave.Reason = leaveViewModel.Reason;
            if (user == null)
            {
                return BadRequest();
            }
            Leave.Duration = DateTime.Now.AddDays(leaveViewModel.Day.Day);
            Leave.DaysLeft = leaveViewModel.Day.Day - DateTime.Now.Day;

            if (user.Onleave == true)
            {
                return View("OnLeave");
            }

            if (Leave.Duration.Second > 0)
            {
                user.Onleave = true;
            }
            if (user.Position == 1)
            {
                Leave.AllowedDuration = 5;
                Leave.DaysLeft = leaveViewModel.Day.Day - DateTime.Now.Day;
                var perMonth = DateTime.Now.AddMonths(1);
                if (DateTime.Now > perMonth)
                {
                    Leave.AllowedDuration = 5;
                }
            }
            if (user.Position == 2)
            {
                Leave.AllowedDuration = 7;
                Leave.DaysLeft = leaveViewModel.Day.Day - DateTime.Now.Day;
                var perMonth = DateTime.Now.AddMonths(1);
                if (DateTime.Now > perMonth)
                {
                    Leave.AllowedDuration = 5;
                }
            }
            if (user.Position == 3)
            {
                Leave.AllowedDuration = 12;
                Leave.DaysLeft = leaveViewModel.Day.Day - DateTime.Now.Day;
                var perMonth = DateTime.Now.AddMonths(1);
                if (DateTime.Now > perMonth)
                {
                    Leave.AllowedDuration = 5;
                }
            }
            if (user.Position == 4)
            {
                Leave.AllowedDuration = 14;
                Leave.DaysLeft = leaveViewModel.Day.Day - DateTime.Now.Day;
                var perMonth = DateTime.Now.AddMonths(1);
                if (DateTime.Now > perMonth)
                {
                    Leave.AllowedDuration = 5;
                }
            }
            if (leaveViewModel.Day.Day - DateTime.Now.Day > Leave.AllowedDuration)
            {
                return View("Error");
            }
            await _context.LeaveRequests.AddAsync(Leave);
            await _context.SaveChangesAsync();
            return View("Success");
        }
        [HttpGet]
        public async Task<IActionResult> UserEdit()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> EditUser(EditUserViewModel editUserViewModel)
        {
            var user = await _context.Users.FirstOrDefaultAsync(find => find.Email == editUserViewModel.Email);
            var all = await _context.Users.ToListAsync();
            user.FirstName = editUserViewModel.FirstName;
            user.LastName = editUserViewModel.LastName;
            await _context.SaveChangesAsync();
            return View("~/Views/Home/Index.cshtml");

        }
        [HttpGet]
        public async Task<IActionResult> Login()
        {
            return View("Login");
        }
        [HttpPost]
        public async Task<IActionResult> ValidateUser(LoginViewModel loginViewModel)
        {
            var users = await _context.Users.ToListAsync();
            foreach (var user in users)
            {
                if (BCrypt.Net.BCrypt.Verify(loginViewModel.Password, user.HashedPassword) && loginViewModel.Email == user.Email)
                {
                    return View("Leave");
                }
            }
            return View("Error");

        }
        public async Task<IActionResult> LogOut()
        {
            return View("Login");
        }
        public ActionResult GetUsers()
        {
            var users = _context.Users.ToList();
            var leaves = _context.LeaveRequests.ToList();
            foreach (var leave in leaves)
            {
                if (leave.Duration < DateTime.Now)
                {
                    var user = _context.Users.FirstOrDefault(x => x.Id == leave.UserId);
                    user.Onleave = false;
                    _context.SaveChanges();
                }
            }
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
