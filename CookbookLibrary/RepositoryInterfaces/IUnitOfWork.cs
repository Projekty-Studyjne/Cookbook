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
        IGenericRepository<Category> CategoryRepository { get; }
        IGenericRepository<CategoryRecipe> CategoryRecipeRepository { get; }
        IGenericRepository<Comment> CommentRepository { get; }
        IGenericRepository<Ingredient> IngredientRepository { get; }
        IGenericRepository<IngredientRecipe> IngredientRecipeRepository { get; }
        IGenericRepository<Rating> RatingRepository { get; }
        IGenericRepository<Recipe> RecipeRepository { get; }
        IGenericRepository<User> UserRepository { get; }
        IGenericRepository<UserRecipe> UserRecipeRepository { get; }

        public void Save();
        public void Dispose(bool disposing);
        public void Dispose();

        Task SaveAsync();
    }
  
}
