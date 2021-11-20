using WebAPI.Models;

namespace WebAPI.Interfaces
{
    public interface IAccountService
    {
        public void LoginUser(LoginUserDto dto);

        public void RegisterUser(RegisterUserDto dto);
/*        void RegisterUser(RegisterUserDto dto);
        string GenerateJwt(LoginUserDto dto);*/
    }
}