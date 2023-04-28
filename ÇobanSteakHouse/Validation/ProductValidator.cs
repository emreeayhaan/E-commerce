using Business.Business;
using Data.Dto;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ÇobanSteakHouse.Validation
{
    public class ProductValidator
    {
        public static void ValidateProductForAdd(ProductBusiness productBusiness, ProductDtoForAdd product, ModelStateDictionary modelState)
        {
            if (product.ProductId <= 0 || productBusiness.Get(product.ProductId) != null)
                modelState.AddModelError("productId", "Product Id should be special and over 0");

            if (string.IsNullOrEmpty(product.ProductName))
                modelState.AddModelError("productName", "Product Name can't to be empty");

            if (product.ProductFile == null)
                modelState.AddModelError("productFile", "Choose a picture");

            if (product.ProductStock < 0)
                modelState.AddModelError("productStock", "Product stock cant be under 0");

            if (!double.TryParse(product.ProductPrice, out double value))
                modelState.AddModelError("productPrice", "Product price is invalid must be float");

            if (new CategoryBusiness().Get(product.CategoryId) == null)
                modelState.AddModelError("categoryId", "There isn't a category please check category id");
        }
    }
}
