using CookbookLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CookbookBLL.Interfaces
{
    public interface IIngredientService
    {
        Task<IEnumerable<Ingredient>> GetAll();
        Task<Ingredient> GetIngredientById(int ingredientId);
        Task Update(Ingredient ingredient);
        Task Delete(int ingredientId);
        Task Add(Ingredient ingredient);
    }
}
