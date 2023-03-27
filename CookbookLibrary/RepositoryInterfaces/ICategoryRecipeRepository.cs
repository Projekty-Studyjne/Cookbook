using CookbookLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookbookLibrary.RepositoryInterfaces
{
    public interface ICategoryRecipeRepository : IDisposable
    {
        IEnumerable<CategoryRecipe> GetCategoryRecipes();
        CategoryRecipe GetCategoryRecipeById(int categoryRecipeId);
        void InsertCategoryRecipe(CategoryRecipe categoryRecipe);
        void DeleteCategoryRecipe(int categoryRecipeId);
        void UpdateCategoryRecipe(CategoryRecipe categoryRecipe);
        void Save();
    }
}
