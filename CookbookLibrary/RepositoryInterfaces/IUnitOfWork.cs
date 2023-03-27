using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookbookLibrary.RepositoryInterfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ICategoryRepository Categories { get; }
        ICommentRepository Comments { get; }
        IIngredientRepository Ingredients { get; }
        IRatingRepository Ratings { get; }
        IRecipeRepository Recipes { get; }
        IUserRepository Users { get; }
        ICategoryRecipeRepository CategoryRecipes { get; }
        IIngredientRecipeRepository IngredientRecipes { get; }
        IUserRecipeRepository UserRecipes { get; }
        void Save();
    }
  
}
