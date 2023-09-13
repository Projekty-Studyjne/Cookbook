using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CookbookLibrary;
using CookbookLibrary.Entities;
using CookbookLibrary.RepositoryInterfaces;
using System.Data;
using CookbookBLL.Interfaces;
using CookbookBLL;
using CookbookMVCBLL;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using CookbookLibrary.Repositories;
using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow;

namespace CookbookMVCBLL.Controllers
{
    public class UsersController : Controller
    {
        private IUserService service;
        public int idLoginUser;
        public UsersController(IUserService service)
        {
            this.service = service;
        }
        
        public async Task<ViewResult> Index()
        {
            var user = await service.GetAll();
            return View(user.ToList());
        }

        public async Task<ViewResult> Details(int id)
        {
            User user = await service.GetUserById(id);
            return View(user);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("userId,username,email,password")] User user)
        {

            service.Add(user);
            return RedirectToAction(nameof(Index));
            return View(user);
        }

        public async Task<IActionResult> Edit(int id)
        {
            User user = await service.GetUserById(id);
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("userId,username,email,password")] User user)
        {
            if (id != user.userId)
            {
                return NotFound();
            }

            try
            {
                service.Update(user);
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }

            return RedirectToAction(nameof(Index));

            return View(user);
        }

        public async Task<IActionResult> Delete(int id)
        {

            User user = await service.GetUserById(id);
            await service.Delete(user.userId);
            return View(user);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            User user = await service.GetUserById(id);
            service.Delete(id);
            return RedirectToAction(nameof(Index));
        }
        public IActionResult EditAccount()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([Bind("username,password")] User user)
        {
            var username = Request.Form["username"];
            var password = Request.Form["password"];

            var authenticatedUser = await service.AuthenticateUser(username, password);
                
                
            if (authenticatedUser != null)
                {
                return RedirectToAction("AccountPanel", "Users", new { id = authenticatedUser.userId});
                }
                else
                {
                    ModelState.AddModelError("InvalidCredentials", "Invalid username or password.");
                }
       
            return View();
        }
        
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([Bind("userId,username,email,password")] User user)
        {

            service.Add(user);
            int id = user.userId;
            return RedirectToAction(nameof(Login));
            
        }


        public async Task<IActionResult> EditAccount(int id)
        {
            User user = await service.GetUserById(id);
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAccount(int id, [Bind("userId,username,email,password")] User user)
        {
            if (id != user.userId)
            {
                return NotFound();
            }
            try
            {
                service.Update(user);
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }

            return RedirectToAction(nameof(Index));

            return View(user);
        }


        [HttpGet]
        [ActionName("AccountPanel")]
        public async Task<IActionResult> AccountPanel(int? id)
        {
                User user = await service.GetUserById(id.Value);
                return View(user);


        }

    }
}
