using CookbookBLL.Interfaces;
using CookbookLibrary.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CookbookWebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoryApiController : Controller
    {
        private readonly ICategoryService _categoryService;
        public CategoryApiController(ICategoryService categoryService)
        {
            this._categoryService = categoryService;
        }
        [HttpGet]
        public IEnumerable<CategoryResponse> GetAll()
        {
            IEnumerable<Category> categories = new List<Category>();
            categories = _categoryService.GetAll().Result;
            return categories;
            //return _categoryService.GetAll().Result.Select(x => new CategoryResponse(x.categoryId, x.name, x.description));
        }

        [HttpGet("{id}")]
        public Category GetOne(int id)
        {
            Category category = null;
            category = _categoryService.GetCategoryById(id).Result;
            return category;
        }

        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
            if (_categoryService.Delete(id).IsCompletedSuccessfully)
                return true;
            return false;
        }

        [HttpPost]
        public bool Post(Category category)
        {
            if (_categoryService.Add(category).IsCompletedSuccessfully)
            {
                return true;
            }
            return false;
        }

        [HttpPut]
        public bool Put(Category category)
        {
            if (_categoryService.Update(category).IsCompletedSuccessfully)
                return true;
            return false;
        }
    }
}
