using LeaveManagementSystem.Models;
using LeaveManagementSystem.ViewModel;

namespace LeaveManagementSystem.Mapper
{
    public static class UserMapper
    {
        public static RegisterUserViewModel ToRegisterUserViewModel(this User user)
        {
            return new RegisterUserViewModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Password = user.HashedPassword
            };
        }
        public static User ToUser(this RegisterUserViewModel registerUserViewModel)
        {
            return new User
            {
                FirstName = registerUserViewModel.FirstName,
                LastName = registerUserViewModel.LastName,
                Email = registerUserViewModel.Email,
                HashedPassword = registerUserViewModel.Password
            };
        }
    }
}
