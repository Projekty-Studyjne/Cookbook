using CookbookLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookbookLibrary.RepositoryInterfaces
{
    public interface IRecipeRepository : IDisposable
    {
        IEnumerable<Recipe> GetRecipes();
        Recipe GetRecipeById(int recipeId);
        void InsertRecipe(Recipe recipe);
        void DeleteRecipe(int recipeId);
        void UpdateRecipe(Recipe recipe);
        void Save();
    }
}
