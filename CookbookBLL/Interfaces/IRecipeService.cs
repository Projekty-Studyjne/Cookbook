using CookbookLibrary.Entities;

namespace CookbookBLL.Interfaces
{
    public interface IRecipeService
    {
        Task<IEnumerable<Recipe>> GetAll();
        Task GetRecipeById(int recipeId);
        Task Update(Recipe recipe);
        Task Add(Recipe recipe);
        Task Delete(Recipe recipe);
    }
}