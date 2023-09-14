using CookbookLibrary.Entities;
using CookbookLibrary.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TestProjectBLL
{
    public class IngredientRepoFake : IGenericRepository<Ingredient>
    {
        private List<Ingredient> ingredients = new List<Ingredient>();
        public Task<IEnumerable<Ingredient>> GetAsync(Expression<Func<Ingredient, bool>> filter = null, Func<IQueryable<Ingredient>, IOrderedQueryable<Ingredient>> orderBy = null, string includeProperties = "")
        {
            IQueryable<Ingredient> query = ingredients.AsQueryable();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return Task.FromResult<IEnumerable<Ingredient>>(orderBy(query).ToList());
            }
            else
            {
                return Task.FromResult<IEnumerable<Ingredient>>(query.ToList());
            }
        }
        public void Delete(object id)
        {
            Ingredient ingredient = ingredients.Find(s => s.ingredientId == (int)id);
            ingredients.Remove(ingredient);
        }

        public void Delete(Ingredient entityToDelete)
        {
            ingredients.Remove(entityToDelete);
        }

        public Ingredient GetByID(object id)
        {
            return ingredients.FirstOrDefault(e => e.ingredientId == (int)id);
        }

        public void Insert(Ingredient entity)
        {
            ingredients.Add(entity);
        }

        public void Update(Ingredient entityToUpdate)
        {
            int index = this.ingredients.FindIndex(s => s.ingredientId == entityToUpdate.ingredientId);
            if (index != -1)
                ingredients[index] = entityToUpdate;
        }


    }
}
