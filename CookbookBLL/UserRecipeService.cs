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
    public class UserRecipeService : IUserRecipeService
    {
        private readonly IUnitOfWork _unitOfWork;
        public UserRecipeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<UserRecipe>> GetAll()
        {
            var userRecipe = await _unitOfWork.UserRecipeRepository.GetAsync(includeProperties: "User,Recipe");
            return userRecipe;
        }

        public async Task<UserRecipe> GetUserRecipeById(int userRecipeId)
        {
            var userRecipe = _unitOfWork.UserRecipeRepository.GetByID(userRecipeId);
            return await Task.FromResult(userRecipe);
        }

        public async Task Update(UserRecipe userRecipe)
        {
            try
            {
                _unitOfWork.UserRecipeRepository.Update(userRecipe);
                _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error occurred while updating userRecipe");
                throw;
            }
        }

        public async Task Add(UserRecipe userRecipe)
        {
            try
            {
                var userRecipeRepos = _unitOfWork.UserRecipeRepository;
                userRecipeRepos.Insert(userRecipe);
                _unitOfWork.Save();
            }
            catch (Exception e)
            {
                Console.WriteLine("An Error occured while adding userRecipe");
                throw;
            }
        }

        public async Task Delete(int userRecipeId)
        {
            try
            {
                var userRecipeRepos = _unitOfWork.UserRepository;
                userRecipeRepos.Delete(userRecipeId);
                _unitOfWork.Save();

            }
            catch (Exception e)
            {
                Console.WriteLine("An error occured while deleting user");
                throw;
            }
        }
    }
}
