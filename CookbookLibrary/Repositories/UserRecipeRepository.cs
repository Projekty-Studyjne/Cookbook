using CookbookLibrary.Entities;
using CookbookLibrary.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookbookLibrary.Repositories
{
    public class UserRecipeRepository : IUserRecipeRepository, IDisposable
    {
        private CookbookDbContext context;

        public UserRecipeRepository(CookbookDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<UserRecipe> GetUserRecipes()
        {
            return context.UserRecipes.ToList();
        }

        public UserRecipe GetUserRecipeById(int id)
        {
            return context.UserRecipes.Find(id);
        }

        public void InsertUserRecipe(UserRecipe userRecipe)
        {
            context.UserRecipes.Add(userRecipe);
        }

        public void DeleteUserRecipe(int userRecipeID)
        {
            UserRecipe userRecipe = context.UserRecipes.Find(userRecipeID);
            context.UserRecipes.Remove(userRecipe);
        }

        public void UpdateUserRecipe(UserRecipe userRecipe)
        {
            context.Entry(userRecipe).State = EntityState.Modified;
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }

}
