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
    public class CategoryRecipeService : ICategoryRecipeService
    {
        private readonly IUnitOfWork _unitOfWork;
        public CategoryRecipeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<CategoryRecipe>> GetAll()
        {
            var categoryRecipe = await _unitOfWork.CategoryRecipeRepository.GetAsync(includeProperties: "Category,Recipe");
            return categoryRecipe;
        }

        public async Task<CategoryRecipe> GetCategoryRecipeById(int categoryRecipeId)
        {
            var categoryRecipe = _unitOfWork.CategoryRecipeRepository.GetByID(categoryRecipeId);
            return await Task.FromResult(categoryRecipe);
        }

        public async Task Update(CategoryRecipe categoryRecipe)
        {
            try
            {
                _unitOfWork.CategoryRecipeRepository.Update(categoryRecipe);
                _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error occurred while updating categoryRecipe");
                throw;
            }
        }

        public async Task Add(CategoryRecipe categoryRecipe)
        {
            try
            {
                var categoryRecipeRepos = _unitOfWork.CategoryRecipeRepository;
                categoryRecipeRepos.Insert(categoryRecipe);
                _unitOfWork.Save();
            }
            catch (Exception e)
            {
                Console.WriteLine("An Error occured while adding categoryRecipe");
                throw;
            }
        }

        public async Task Delete(int categoryRecipeId)
        {
            try
            {
                var categoryRecipeRepos = _unitOfWork.CategoryRecipeRepository;
                categoryRecipeRepos.Delete(categoryRecipeId);
                _unitOfWork.Save();

            }
            catch (Exception e)
            {
                Console.WriteLine("An error occured while deleting categoryRecipe");
                throw;
            }
        }
    }
}
