using CookbookLibrary.Entities;

namespace CookbookBLL.Interfaces
{
    public interface IRecipeService
    {
        Task<IEnumerable<Recipe>> GetAll();
        Task<Recipe> GetRecipeById(int recipeId);
        Task Update(Recipe recipe);
        Task Add(Recipe recipe);
        Task Delete(int recipeId);
    }
}