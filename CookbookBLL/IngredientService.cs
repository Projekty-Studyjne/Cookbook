using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CookbookBLL.Interfaces;
using CookbookLibrary.Entities;
using CookbookLibrary.Repositories;
using CookbookLibrary.RepositoryInterfaces;

namespace CookbookBLL
{
    internal class IngredientService : IIngredientService
    {
        private readonly IUnitOfWork _unitOfWork;
        public IngredientService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Ingredient>> GetAll()
        {
            var recipes = await _unitOfWork.IngredientRepository.GetAsync(includeProperties: "IngredientRecipes");
            return recipes;
        }

        public async Task<Ingredient> GetIngredientById(int ingredientId)
        {
            var ingredient = _unitOfWork.IngredientRepository.GetByID(ingredientId);
            return await Task.FromResult(ingredient);
        }

        public async Task Update(Ingredient ingredient)
        {
            try
            {
                _unitOfWork.IngredientRepository.Update(ingredient);
                await _unitOfWork.SaveAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error occurred while updating ingredient");
                throw;
            }
        }

        public async Task Add(Ingredient ingredient)
        {
            try
            {
                var workRepos = _unitOfWork.IngredientRepository;
                workRepos.Insert(ingredient);
                _unitOfWork.Save();
            }
            catch (Exception e)
            {
                Console.WriteLine("An Error occured while adding ingredient");
                throw;
            }
        }

        public async Task Delete(int ingredientId)
        {
            try
            {
                var workRepos = _unitOfWork.IngredientRepository;
                workRepos.Delete(ingredientId);
                await _unitOfWork.SaveAsync();

            }
            catch (Exception e)
            {
                Console.WriteLine("An error occured while deleting recipe");
                throw;
            }
        }
    }
}
