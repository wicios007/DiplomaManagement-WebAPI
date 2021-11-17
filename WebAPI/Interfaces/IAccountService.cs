using WebAPI.Models;

namespace WebAPI.Interfaces
{
    public interface IAccountService
    {
        void RegisterUser(RegisterUserDto dto);
        string GenerateJwt(LoginUserDto dto);
    }
}