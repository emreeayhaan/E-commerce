using Business.Business;
using Business.Extensions;
using Data.Dto;
using Data.Interface;
using Microsoft.AspNetCore.Http;

namespace Business.Session
{
    public class LoginService : ILoginService
    {
        private readonly HttpContext httpContext;
        CustomerBusiness _customerBusiness;
        ManagerBusiness _managerBusiness;

        public LoginService(IHttpContextAccessor httpContextAccessor)
        {
            _customerBusiness = new CustomerBusiness();
            _managerBusiness = new ManagerBusiness();
            this.httpContext = httpContextAccessor.HttpContext;
        }


        public void SetSession(IUser user)
        {
            if (user is CustomerDto)
            {
                CustomerDto customer = (CustomerDto)user;
                httpContext.Session.SetString("Name", customer.Name);
                httpContext.Session.SetString("Email", customer.Email);
                httpContext.Session.SetString("UserId", customer.CustomerId);
                httpContext.Session.SetString("SessionType", "Customer");
                httpContext.Session.SetString("IsLoggedIn", "true");
            }
            else if (user is ManagerDto)
            {
                ManagerDto manager = (ManagerDto)user;
                httpContext.Session.SetString("Name", manager.Name);
                httpContext.Session.SetString("Email", manager.Email);
                httpContext.Session.SetString("UserId", manager.ManagerId);
                httpContext.Session.SetString("SessionType", "Manager");
                httpContext.Session.SetString("IsLoggedIn", "true");
            }
        }

        public bool Login(LoginDto loginDto)
        {
            CustomerDto customerDto = _customerBusiness.GetDto(loginDto);
            if (customerDto != null)
            {
                SetSession(customerDto);
                return true;
            }
            ManagerDto managerDto = _managerBusiness.GetDto(loginDto);
            if (managerDto != null)
            {
                SetSession(managerDto);
                return true;
            }
            return false;
        }

        public void LogOut()
        {
            httpContext.Session.Remove("Name");
            httpContext.Session.Remove("Email");
            httpContext.Session.Remove("UserId");
            httpContext.Session.Remove("SessionType");
            httpContext.Session.Remove("IsLoggedIn");
        }
    }
}
