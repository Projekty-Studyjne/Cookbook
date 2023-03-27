using CookbookLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookbookLibrary.RepositoryInterfaces
{
    public interface IIngredientRepository : IDisposable
    {
        IEnumerable<Ingredient> GetIngredients();
        Ingredient GetIngredientById(int ingredientId);
        void InsertIngredient(Ingredient ingredient);
        void DeleteIngredient(int ingredientId);
        void UpdateIngredient(Ingredient ingredient);
        void Save();
    }

}
