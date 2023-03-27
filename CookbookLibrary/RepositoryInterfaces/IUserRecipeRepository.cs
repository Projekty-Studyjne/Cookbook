using CookbookLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookbookLibrary.RepositoryInterfaces
{
    public interface IUserRecipeRepository : IDisposable
    {
        IEnumerable<UserRecipe> GetUserRecipes();
        UserRecipe GetUserRecipeById(int userRecipeId);
        void InsertUserRecipe(UserRecipe userRecipe);
        void DeleteUserRecipe(int userRecipeId);
        void UpdateUserRecipe(UserRecipe userRecipe);
        void Save();
    }
}
