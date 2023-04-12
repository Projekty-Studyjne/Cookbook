using CookbookLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookbookBLL.Interfaces
{
    internal interface IUserService
    {
        Task<IEnumerable<User>> GetAll();
        Task<User> GetUserById(int userId);
        Task Update(User user);
        Task Add(User user);
        Task Delete(int userId);
    }
}
