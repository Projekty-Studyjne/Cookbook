using CookbookLibrary.Entities;
using CookbookLibrary.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProjectDAL
{
    internal class RecipeRepoDummy : IRecipeRepository
    {
        public IEnumerable<Recipe> GetRecipes()
        {
            throw new NotImplementedException();
        }

        public Recipe GetRecipeById(int id)
        {
            throw new NotImplementedException();
        }

        public void InsertRecipe(Recipe recipe)
        {
            throw new NotImplementedException();
        }

        public void DeleteRecipe(int recipeId)
        {
            throw new NotImplementedException();
        }

        public void UpdateRecipe(Recipe recipe)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }


        protected virtual void Dispose(bool disposing)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
