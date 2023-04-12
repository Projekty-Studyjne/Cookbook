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

        //public async Task<Recipe> GetRecipeById(int recipeId)
        //{
        //    var recipe = _unitOfWork.RecipeRepository.GetByID(recipeId);
        //    return await Task.FromResult(recipe);
        //}

        //public async Task Update(Recipe recipe)
        //{
        //    try
        //    {
        //        unitOfWork.RecipeRepository.Update(recipe);
        //        unitOfWork.Save();
        //    }
        //    catch (Exception ex)
        //    {
        //        // Handle the exception
        //    }
        //}

        //public async Task Add(Work workInput)
        //{
        //    try
        //    {
        //        await _unitOfWork.BeginTransaction();
        //        var workRepos = _unitOfWork.Repository();
        //        await workRepos.InsertAsync(workInput);
        //        await _unitOfWork.CommitTransaction();
        //    }
        //    catch (Exception e)
        //    {
        //        await _unitOfWork.RollbackTransaction();
        //        throw;
        //    }
        //}
        //public async Task Delete(int workId)
        //{
        //    try
        //    {
        //        await _unitOfWork.BeginTransaction();
        //        var workRepos = _unitOfWork.Repository();
        //        var work = await workRepos.FindAsync(workId);
        //        if (work == null)
        //            throw new KeyNotFoundException();
        //        await workRepos.DeleteAsync(work);
        //        await _unitOfWork.CommitTransaction();
        //    }
        //    catch (Exception e)
        //    {
        //        await _unitOfWork.RollbackTransaction();
        //        throw;
        //    }
        //}
    }
}
