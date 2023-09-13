using CookbookBLL.Interfaces;
using CookbookLibrary.Entities;
using CookbookLibrary.Repositories;
using CookbookLibrary.RepositoryInterfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CookbookMVCBLL.Controllers
{
    public class AccountController : Controller
    {
        //private readonly IUserService _userService;
        //private readonly IUnitOfWork _unitOfWork;

        //public AccountController(IUserService userService)
        //{
        //    _userService = userService;
        //}
        //public IUserService(IUnitOfWork unitOfWork)
        //{
        //    _unitOfWork = unitOfWork;
        //}

        public IActionResult Login()
        {
            return View();
        }

        //public async Task<IActionResult> Login(string username, string password)
        //{
        //    var user = await _unitOfWork.UserRepository.GetByUsername(username);

        //    if (user == null)
        //    {
        //        return RedirectToAction("Index", "Login", new { ErrorMessage = "Invalid username or password." });
        //    }

        //    // The login succeeded.
        //    // Redirect the user to the home page.
        //    return RedirectToAction("Index");
        //}
    }
}