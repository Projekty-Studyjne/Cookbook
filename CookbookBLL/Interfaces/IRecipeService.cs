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
        Task<IEnumerable<Recipe>> GetRecipesByIngredientName(string ingredientName);
        Task<IEnumerable<Recipe>> GetRecipesByName(string name);
        Task<IEnumerable<Recipe>> GetRecipesByCategory(int categoryId);
        int GetMaxId();
        Task AddCategoryToRecipe(int recipeId, int categoryId);
    }
}