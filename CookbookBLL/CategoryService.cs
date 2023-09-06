using CookbookBLL.Interfaces;
using CookbookLibrary.Entities;
using CookbookLibrary.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookbookBLL
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        public CategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task Add(Category category)
        {
            try
            {
                var workRepos = _unitOfWork.CategoryRepository;
                workRepos.Insert(category);
                _unitOfWork.Save();
            }
            catch (Exception e)
            {
                Console.WriteLine("An Error occured while adding category");
                throw;
            }
        }

        public async Task Delete(int categoryId)
        {
            try
            {
                var workRepos = _unitOfWork.CategoryRepository;
                workRepos.Delete(categoryId);
                _unitOfWork.Save();

            }
            catch (Exception e)
            {
                Console.WriteLine("An error occured while deleting category");
                throw;
            }
        }

        public async Task<IEnumerable<Category>> GetAll()
        {
            var category = await _unitOfWork.CategoryRepository.GetAsync(includeProperties: "CategoryRecipes");
            return category;
        }

        public async Task<Category> GetCategoryById(int categoryId)
        {
            var category = _unitOfWork.CategoryRepository.GetByID(categoryId);
            return await Task.FromResult(category);
        }

        public async Task Update(Category category)
        {
            try
            {
                _unitOfWork.CategoryRepository.Update(category);
                _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error occurred while updating category");
                throw;
            }
        }
    }
}
