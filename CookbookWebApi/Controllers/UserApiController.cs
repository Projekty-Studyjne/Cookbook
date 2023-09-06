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
        public IEnumerable<UserResponse> GetAll()
        {
            //IEnumerable<User> users = new List<User>();
            //users = _userService.GetAll().Result;
            //return users;
            return _userService.GetAll().Result.Select(x => new UserResponse(x.userId, x.username,x.email,x.password));
        }

        [HttpGet("{id}")]
        public UserResponse GetOne(int id)
        {
            User user = null;
            user = _userService.GetUserById(id).Result;
            if(user != null)
            {
                return new UserResponse(user.userId, user.username, user.email, user.password);
            }
            return null;
        }

        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
            if (_userService.Delete(id).IsCompletedSuccessfully)
                return true;
            return false;
        }

        [HttpPost]
        public bool Post([FromBody]UserRequest userReq)
        {
            User user= new User();
            user.username=userReq.username;
            user.email = userReq.email;
            user.password=userReq.password;
            if (_userService.Add(user).IsCompletedSuccessfully)
            {
                return true;
            }
            return false;
        }

        [HttpPut]
        public bool Put(int id,[FromBody]UserRequest userReq)
        {
            User? user = _userService.GetUserById(id).Result;
            if (user != null)
            {
                user.username = userReq.username;
                user.email=userReq.email;
                user.password = userReq.password;
                if (_userService.Update(user).IsCompletedSuccessfully)
                    return true;
            }
            return false;
        }

    }
}
