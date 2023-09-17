using CookbookLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookbookBLL.Interfaces
{
    public interface IIngredientRecipeService
    {
        Task<IEnumerable<IngredientRecipe>> GetAll();
        Task<IngredientRecipe> GetIngredientRecipeById(int ingredientRecipeId);
        Task Update(IngredientRecipe ingredientRecipe);
        Task Delete(int ingredientId);
        Task Add(IngredientRecipe ingredientRecipe);
        IEnumerable<Ingredient> GetAllIngredientsWithNames();
    }
}
