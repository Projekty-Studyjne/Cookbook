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
    public class CategoryRepository : ICategoryRepository, IDisposable
    {
        private CookbookDbContext context;

        public CategoryRepository(CookbookDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<Category> GetCategories()
        {
            return context.Categories.ToList();
        }

        public Category GetCategoryById(int id)
        {
            return context.Categories.Find(id);
        }

        public void InsertCategory(Category category)
        {
            context.Categories.Add(category);
        }

        public void DeleteCategory(int categoryID)
        {
            Category category = context.Categories.Find(categoryID);
            context.Categories.Remove(category);
        }

        public void UpdateCategory(Category category)
        {
            context.Categories.Update(category);
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

