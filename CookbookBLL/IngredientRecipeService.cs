
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
    public class IngredientRecipeService : IIngredientRecipeService
    {
        private readonly IUnitOfWork _unitOfWork;
        public IngredientRecipeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<IngredientRecipe>> GetAll()
        {
            var recipes = await _unitOfWork.IngredientRecipeRepository.GetAsync();
            return recipes;
        }

        public async Task<IngredientRecipe> GetIngredientRecipeById(int recipeId)
        {
            var ingredients = _unitOfWork.IngredientRecipeRepository.GetByID(recipeId);
            return await Task.FromResult(ingredients);
        }

        public async Task Update(IngredientRecipe ingredientRecipe)
        {
            try
            {
                _unitOfWork.IngredientRecipeRepository.Update(ingredientRecipe);
                _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error occurred while updating ingredient");
                throw;
            }
        }

        public async Task Add(IngredientRecipe ingredientRecipe)
        {
            try
            {
                var workRepos = _unitOfWork.IngredientRecipeRepository;
                workRepos.Insert(ingredientRecipe);
                _unitOfWork.Save();
            }
            catch (Exception e)
            {
                Console.WriteLine("An Error occured while adding ingredient");
                throw;
            }
        }

        public async Task Delete(int ingredientRecipeId)
        {
            try
            {
                var workRepos = _unitOfWork.IngredientRecipeRepository;
                workRepos.Delete(ingredientRecipeId);
                _unitOfWork.Save();

            }
            catch (Exception e)
            {
                Console.WriteLine("An error occured while deleting recipe");
                throw;
            }
        }

        //public async Task<IngredientRecipe> GetIngredientRecipeByIngredient(int ingredientId)
        //{
        //    try
        //    {
        //        var ingredient = await _unitOfWork.IngredientRecipeRepository
        //            .GetAsync(r => r.IngredientRecipes.Any(i => i.recipeId == recipeId),
        //                includeProperties: "IngredientRecipes");

        //        return ingredients;
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine("An errror occured while getting recipes");
        //        throw;
        //    }
        //}
    }
}

