using CookbookLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookbookBLL.Interfaces
{
    public interface ICategoryRecipeService
    {
        Task<IEnumerable<CategoryRecipe>> GetAll();
        Task<CategoryRecipe> GetCategoryRecipeById(int categoryRecipeId);
        Task Update(CategoryRecipe categoryRecipe);
        Task Add(CategoryRecipe categoryRecipe);
        Task Delete(int categoryRecipeId);
    }
}
