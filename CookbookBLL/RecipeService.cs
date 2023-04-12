using CookbookBLL.Interfaces;
using CookbookLibrary.Entities;
using CookbookLibrary.Repositories;
using CookbookLibrary.RepositoryInterfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace CookbookBLL
{
    internal class RecipeService : IRecipeService
    {
        private readonly IUnitOfWork _unitOfWork;
        public RecipeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Recipe>> GetAll()
        {
            var recipes = _unitOfWork.RecipeRepository.Get(includeProperties: "IngredientRecipes,UserRecipes,CategoryRecipes,Ratings");
            return await Task.FromResult(recipes);
        }

        public async Task<Recipe> GetRecipeById(int recipeId)
        {
            var recipe = _unitOfWork.RecipeRepository.GetByID(recipeId);
            return await Task.FromResult(recipe);
        }

        public async Task Update(Recipe recipe)
        {
            try
            {
                _unitOfWork.RecipeRepository.Update(recipe);
                await _unitOfWork.SaveAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error occurred while updating recipe");
                throw;
            }
        }

        public async Task Add(Recipe recipe)
        {
            try
            {
                var workRepos = _unitOfWork.RecipeRepository;
                workRepos.Insert(recipe);
                _unitOfWork.Save();
            }
            catch (Exception e)
            {
                Console.WriteLine("An Error occured while adding recipe");
                throw;
            }
        }

        public async Task Delete(int recipeId)
        {
            try
            {
                var workRepos = _unitOfWork.RecipeRepository;
                workRepos.Delete(recipeId);
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
