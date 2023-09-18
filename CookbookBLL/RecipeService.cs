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
using CookbookLibrary;
using System.Runtime.CompilerServices;

namespace CookbookBLL
{
    public class RecipeService : IRecipeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private static int _recipeId;
        public RecipeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Recipe>> GetAll()
        {
            var recipes = await _unitOfWork.RecipeRepository.GetAsync(includeProperties: "IngredientRecipes,UserRecipes,CategoryRecipes,Ratings");
            return recipes;
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
                _unitOfWork.Save();
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
                var recipeRepos = _unitOfWork.RecipeRepository;
                recipeRepos.Insert(recipe);
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
                var recipeRepos = _unitOfWork.RecipeRepository;
                recipeRepos.Delete(recipeId);
                _unitOfWork.Save();

            }
            catch (Exception e)
            {
                Console.WriteLine("An error occured while deleting recipe");
                throw;
            }
        }

        public async Task<IEnumerable<Recipe>> GetRecipesByIngredientName(string ingredientName)
        {
            try
            {
                var recipes = await _unitOfWork.RecipeRepository
                    .GetAsync(r => r.IngredientRecipes.Any(ir => ir.Ingredient.name == ingredientName));

                return recipes;
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred while getting recipes");
                throw;
            }
        }

        public async Task<IEnumerable<Recipe>> GetRecipesByName(string name)
        {
            try
            {
                var recipes = await _unitOfWork.RecipeRepository
                    .GetAsync(r => r.title.Contains(name));
                return recipes;
            }
            catch (Exception e)
            {
                Console.WriteLine("An errror occured while getting recipes");
                throw;
            }
        }

        public async Task<IEnumerable<Recipe>> GetRecipesByCategory(int categoryId)
        {
            try
            {
                var recipes = await _unitOfWork.RecipeRepository
                    .GetAsync(r => r.CategoryRecipes.Any(i => i.categoryId == categoryId),
                        includeProperties: "CategoryRecipes");

                return recipes;
            }
            catch (Exception e)
            {
                Console.WriteLine("An errror occured while getting recipes");
                throw;
            }
        }


        public async Task AddCategoryToRecipe(int recipeId, int categoryId)
        {
            CategoryRecipe categoryRecipe = new CategoryRecipe { recipeId= recipeId, categoryId = categoryId };
            _unitOfWork.CategoryRecipeRepository.Insert(categoryRecipe);
            _unitOfWork.Save();
        }



        public int GetMaxId()
        {
            var recipes = _unitOfWork.RecipeRepository.GetAsync();
            return recipes.Result.Max(r => r.recipeId);

        }

        public static void setRecipeId(int id)
        {
            _recipeId = id;
        }

        public static int getRecipeId()
        {
            return _recipeId;
        }
    }
}
