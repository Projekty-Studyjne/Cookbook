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
            //IEnumerable<Category> categories = new List<Category>();
            //categories = _categoryService.GetAll().Result;
            //return categories;
            return _categoryService.GetAll().Result.Select(x => new CategoryResponse(x.categoryId, x.name, x.description));
        }

        [HttpGet("{id}")]
        public CategoryResponse GetOne(int id)
        {
            Category category = null;
            category = _categoryService.GetCategoryById(id).Result;
            if (category != null)
            {
                return new CategoryResponse(category.categoryId,category.name, category.description);
            }
            return null;
        }

        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
            if (_categoryService.Delete(id).IsCompletedSuccessfully)
                return true;
            return false;
        }

        [HttpPost]
        public bool Post([FromBody]CategoryRequest categoryReq)
        {
            Category category = new Category();
            category.name = categoryReq.name;
            category.description = categoryReq.description;
            if (_categoryService.Add(category).IsCompletedSuccessfully)
            {
                return true;
            }
            return false;
        }

        [HttpPut]
        public bool Put(int id, [FromBody]CategoryRequest categoryReq)
        {
            Category? category = _categoryService.GetCategoryById(id).Result;
            if (category != null)
            {
                category.name = categoryReq.name;
                category.description = categoryReq.description;
                if (_categoryService.Update(category).IsCompletedSuccessfully)
                    return true;
            }
            return false;
        }
    }
}
