using CookbookLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookbookLibrary.RepositoryInterfaces
{
    public interface ICategoryRepository : IDisposable
    {
        IEnumerable<Category> GetCategories();
        Category GetCategoryById(int categoryId);
        void InsertCategory(Category category);
        void DeleteCategory(int categoryId);
        void UpdateCategory(Category category);
        void Save();
    }

}
