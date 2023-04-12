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
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            var user = await _unitOfWork.UserRepository.GetAsync(includeProperties: "Ratings,UserRecipes");
            return user;
        }

        public async Task<User> GetUserById(int userId)
        {
            var user = _unitOfWork.UserRepository.GetByID(userId);
            return await Task.FromResult(user);
        }

        public async Task Update(User user)
        {
            try
            {
                _unitOfWork.UserRepository.Update(user);
                await _unitOfWork.SaveAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error occurred while updating user");
                throw;
            }
        }

        public async Task Add(User user)
        {
            try
            {
                var userRepos = _unitOfWork.UserRepository;
                userRepos.Insert(user);
                _unitOfWork.Save();
            }
            catch (Exception e)
            {
                Console.WriteLine("An Error occured while adding user");
                throw;
            }
        }

        public async Task Delete(int userId)
        {
            try
            {
                var userRepos = _unitOfWork.UserRepository;
                userRepos.Delete(userId);
                await _unitOfWork.SaveAsync();

            }
            catch (Exception e)
            {
                Console.WriteLine("An error occured while deleting user");
                throw;
            }
        }
    }
}
