using Data.Dto;
using Data.Interface;
namespace Business.Session
{
    public interface ILoginService
    {
        public void LogOut();
        public bool Login(LoginDto loginDto);
        public void SetSession(IUser user);
    }
}
