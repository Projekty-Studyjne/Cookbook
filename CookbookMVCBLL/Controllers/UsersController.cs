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

namespace CookbookMVCBLL.Controllers
{
    public class UsersController : Controller
    {
        private IUserService service;

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
            if (ModelState.IsValid)
            {
                service.Add(user);
                return RedirectToAction(nameof(Index));
            }
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

            if (ModelState.IsValid)
            {
                try
                {
                    service.Update(user);
                }
                catch (DataException)
                {
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }

                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        public async Task<IActionResult> Delete(int id)
        {

            User user = await service.GetUserById(id);
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
    }
}
