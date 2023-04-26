using CookbookLibrary.Entities;
using CookbookLibrary.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TestProjectDAL
{
    internal class RecipeRepoDummy : IGenericRepository<Recipe>
    {
        public void Delete(object id)
        {
            throw new NotImplementedException();
        }

        public void Delete(Recipe entityToDelete)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Recipe>> GetAsync(Expression<Func<Recipe, bool>> filter = null, Func<IQueryable<Recipe>, IOrderedQueryable<Recipe>> orderBy = null, string includeProperties = "")
        {
            throw new NotImplementedException();
        }

        public Recipe GetByID(object id)
        {
            throw new NotImplementedException();
        }

        public void Insert(Recipe entity)
        {
            throw new NotImplementedException();
        }

        public void Update(Recipe entityToUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
