using CookbookLibrary.Entities;
using CookbookLibrary.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CookbookLibrary.Repositories
{
    public class RecipeRepository : GenericRepository<Recipe>, IRecipeRepository
    {
        internal CookbookDbContext context;
        internal DbSet<Recipe> dbSet;

        public RecipeRepository(CookbookDbContext context) : base(context)
        {
            this.context = context;
            this.dbSet = context.Set<Recipe>();
        }

        public async Task<IEnumerable<Recipe>> GetAsync(
            Expression<Func<Recipe, bool>> filter = null,
            Func<IQueryable<Recipe>, IOrderedQueryable<Recipe>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<Recipe> query = dbSet;

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
                return await orderBy(query).ToListAsync();
            }
            else
            {
                return await query.ToListAsync();
            }
        }

        public virtual Recipe GetByID(object id)
        {
            return dbSet.Find(id);
        }

        public virtual void Insert(Recipe entity)
        {
            dbSet.Add(entity);
        }

        public virtual void Delete(object id)
        {
            Recipe entityToDelete = dbSet.Find(id);
            Delete(entityToDelete);
        }

        public virtual void Delete(Recipe entityToDelete)
        {
            if (context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
        }

        public virtual void Update(Recipe entityToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            context.Entry(entityToUpdate).State = EntityState.Modified;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
