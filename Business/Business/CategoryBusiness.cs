using Data.Dto;
using Data.Models;
using MongoDB;

namespace Business.Business
{
    public class CategoryBusiness
    {
        private MongoDBService<Category> _categoryService;

        public CategoryBusiness()
        {
            _categoryService = new MongoDBService<Category>();
        }

        public List<Category> Get()
        {
            return _categoryService.GetAll();
        }

        public Category Get(string id)
        {
            return _categoryService.Get(x => x.Id == id);
        }

        public Category Get(int categoryId)
        {
            return _categoryService.Get(x => x.CategoryId == categoryId);
        }
        public List<CategoryDto> GetDto()
        {
            List<CategoryDto> categoryListDto = new List<CategoryDto>();
            List<Category> categories = Get();
            foreach (var item in categories)
            {
                categoryListDto.Add(
                    new CategoryDto()
                    {
                        CategoryId = item.CategoryId,
                        CategoryName = item.CategoryName
                    });
            }
            return categoryListDto;
        }

        public CategoryDto GetDto(string id)
        {
            Category category = Get(id);
            return new CategoryDto()
            {
                CategoryId = category.CategoryId,
                CategoryName = category.CategoryName
            };
        }

        public void Add(CategoryDto categoryDto)
        {
            Category category = new Category()
            {
                CategoryId = categoryDto.CategoryId,
                CategoryName = categoryDto.CategoryName
            };
            _categoryService.Add(category);
        }

        public void Update(CategoryDto categoryDto)
        {
            Category category = Get(categoryDto.CategoryId);
            category.CategoryName = categoryDto.CategoryName;
            _categoryService.Update(x => x.Id == category.Id, category);
        }

        public void Delete(string id)
        {
            _categoryService.Delete(x => x.Id == id);
        }
    }
}
