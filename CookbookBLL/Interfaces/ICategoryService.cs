using CookbookLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookbookBLL.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetAll();
        Task<Category> GetCategoryById(int categoryId);
        Task Update(Category category);
        Task Add(Category category);
        Task Delete(int categoryId);
    }
}
