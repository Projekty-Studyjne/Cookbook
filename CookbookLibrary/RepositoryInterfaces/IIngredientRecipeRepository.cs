using CookbookLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookbookLibrary.RepositoryInterfaces
{
    public interface IIngredientRecipeRepository : IDisposable
    {
        IEnumerable<IngredientRecipe> GetIngredientRecipes();
        IngredientRecipe GetIngredientRecipeById(int ingredientRecipeId);
        void InsertIngredientRecipe(IngredientRecipe ingredientRecipe);
        void DeleteIngredientRecipe(int ingredientRecipeId);
        void UpdateIngredientRecipe(IngredientRecipe ingredientRecipe);
        void Save();
    }
}
