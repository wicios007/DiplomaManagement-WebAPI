using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPI.Entities;
using WebAPI.Models;
using WebAPI.Services;

namespace WebAPI.Interfaces
{
    public interface IAccountService
    {
        Task<int> RegisterUser(RegisterUserDto dto);
        Task<LoginReturn> Login(LoginUserDto dto);
        Task<List<UserDto>> GetUsers();
        Task<List<UserDto>> GetUsersFromDepartment(int departmentId, int roleValue);
        Task<List<UserDto>> GetUsersByRole(int roleValue);
        Task<UserDto> GetCurrentUser();
        Task<UserDto> GetUserById(int id);
        Task<List<Role>> GetRoles();

    }
}