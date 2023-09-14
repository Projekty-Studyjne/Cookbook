using CookbookLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookbookBLL.Interfaces
{
    public interface IUserRecipeService
    {
        Task<IEnumerable<UserRecipe>> GetAll();
        Task<UserRecipe> GetUserRecipeById(int userRecipeId);
        Task Update(UserRecipe userRecipe);
        Task Add(UserRecipe userRecipe);
        Task Delete(int userRecipeId);
    }
}
