using CookbookLibrary.Entities;
using CookbookLibrary.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookbookLibrary.RepositoryInterfaces
{
    public interface IUnitOfWork : IDisposable
    {
        GenericRepository<Category> CategoryRepository { get; }
        GenericRepository<CategoryRecipe> CategoryRecipeRepository { get; }
        GenericRepository<Comment> CommentRepository { get; }
        GenericRepository<Ingredient> IngredientRepository { get; }
        GenericRepository<IngredientRecipe> IngredientRecipeRepository { get; }
        GenericRepository<Rating> RatingRepository { get; }
        GenericRepository<Recipe> RecipeRepository { get; }
        GenericRepository<User> UserRepository { get; }
        GenericRepository<UserRecipe> UserRecipeRepository { get; }

        public void Save();
        public void Dispose(bool disposing);
        public void Dispose();

        Task SaveAsync();
    }
  
}
