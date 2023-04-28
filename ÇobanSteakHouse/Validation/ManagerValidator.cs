using Business.Business;
using Data.Dto;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ÇobanSteakHouse.Validation
{
    public class ManagerValidator
    {
        public static void ValidateManagerForAdd(ManagerBusiness managerBusiness, ManagerDto managerDto, ModelStateDictionary modelState)
        {
            if (managerBusiness.Get(managerDto.ManagerId, true) != null)
                modelState.AddModelError("managerId", "Manager Id is taken");

            if (string.IsNullOrEmpty(managerDto.Name))
                modelState.AddModelError("name", "Name can't be empty");

            if (string.IsNullOrEmpty(managerDto.Password))
                modelState.AddModelError("password", "Password can't be empty");

            if (string.IsNullOrEmpty(managerDto.Email))
                modelState.AddModelError("email", "Email can't be empty");
        }
    }
}
