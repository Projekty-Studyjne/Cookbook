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
    public class CategoryRecipeRepoFake : IGenericRepository<CategoryRecipe>
    {
        private List<CategoryRecipe> categoryRecipes = new List<CategoryRecipe>();
        public void Delete(object id)
        {
            CategoryRecipe categoryRecipe = categoryRecipes.Find(s => s.categoryId == (int)id);
            categoryRecipes.Remove(categoryRecipe);
        }

        public void Delete(CategoryRecipe entityToDelete)
        {
            categoryRecipes.Remove(entityToDelete);
        }

        public Task<IEnumerable<CategoryRecipe>> GetAsync(Expression<Func<CategoryRecipe, bool>> filter = null, Func<IQueryable<CategoryRecipe>, IOrderedQueryable<CategoryRecipe>> orderBy = null, string includeProperties = "")
        {
            IQueryable<CategoryRecipe> query = categoryRecipes.AsQueryable();

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
                return Task.FromResult<IEnumerable<CategoryRecipe>>(orderBy(query).ToList());
            }
            else
            {
                return Task.FromResult<IEnumerable<CategoryRecipe>>(query.ToList());
            }
        }

        public CategoryRecipe GetByID(object id)
        {
            return categoryRecipes.FirstOrDefault(e => e.categoryId == (int)id);
        }

        public void Insert(CategoryRecipe entity)
        {
            categoryRecipes.Add(entity);
        }

        public void Update(CategoryRecipe entityToUpdate)
        {
            int index = this.categoryRecipes.FindIndex(s => s.categoryId == entityToUpdate.categoryId && s.recipeId == entityToUpdate.recipeId);
            if (index != -1)
                categoryRecipes[index] = entityToUpdate;
        }
    }
}
