using CookbookBLL.Interfaces;
using CookbookLibrary.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CookbookWebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserApiController : Controller
    {
        private readonly IUserService _userService;
        public UserApiController(IUserService userService)
        {
            this._userService = userService;
        }
        [HttpGet]
        public IEnumerable<User> GetAll()
        {
            IEnumerable<User> users = new List<User>();
            users = _userService.GetAll().Result;
            return users;
        }

        [HttpGet("{id}")]
        public User GetOne(int id)
        {
            User user = null;
            user = _userService.GetUserById(id).Result;
            return user;
        }

        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
            if (_userService.Delete(id).IsCompletedSuccessfully)
                return true;
            return false;
        }

        [HttpPost]
        public bool Post(User user)
        {
            if (_userService.Add(user).IsCompletedSuccessfully)
            {
                return true;
            }
            return false;
        }

        [HttpPut]
        public bool Put(User user)
        {
            if (_userService.Update(user).IsCompletedSuccessfully)
                return true;
            return false;
        }

    }
}
