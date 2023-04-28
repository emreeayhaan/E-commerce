using Business.Business;
using Data.Dto;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ÇobanSteakHouse.Validation
{
    public class CategoryValidator
    {
        public static void ValidateCategoryForAdd(CategoryBusiness categoryBusiness, CategoryDto categoryDto, ModelStateDictionary modelState)
        {
            if (categoryDto.CategoryId <= 0 || categoryBusiness.Get(categoryDto.CategoryId) != null)
                modelState.AddModelError("categoryId", "Category Id should be special and over 0");

            if (string.IsNullOrEmpty(categoryDto.CategoryName))
                modelState.AddModelError("categoryName", "Category Name can't to be empty");
        }
    }
}
