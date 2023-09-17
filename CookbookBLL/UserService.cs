using CookbookBLL.Interfaces;
using CookbookLibrary.Entities;
using CookbookLibrary.RepositoryInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
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
                _unitOfWork.Save();
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
                _unitOfWork.Save();

            }
            catch (Exception e)
            {
                Console.WriteLine("An error occured while deleting user");
                throw;
            }
        }

        public async Task<User> AuthenticateUser(string username, string password)
        {

            var users = await _unitOfWork.UserRepository.GetAsync(filter: x => x.username == username);
            var user = users.FirstOrDefault(u => u.password == password);

            return user;
        }

        public async Task<User> GetUserByRating(int ratingId)
        {
            var user = await _unitOfWork.RatingRepository
            .GetAsync(filter: r => r.ratingId == ratingId, includeProperties: "User");

            return user.FirstOrDefault()?.User;
        }
    }
}
