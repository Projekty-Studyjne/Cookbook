using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CookbookLibrary;
using CookbookLibrary.Entities;
using CookbookLibrary.Repositories;
using System.Data;
using CookbookLibrary.RepositoryInterfaces;
using Microsoft.Identity.Client;

namespace CookbookMVC.Controllers
{
    public class UsersController : Controller
    {
        private IUnitOfWork unitOfWork;

        public UsersController(IUnitOfWork _unitOfWork) {
            unitOfWork = _unitOfWork;
        }

        public ViewResult Index()
        {
            var user = unitOfWork.UserRepository.Get(includeProperties: "Ratings,UserRecipes");
            return View(user.ToList());
        }

        public ViewResult Details(int id)
        {
            User user = unitOfWork.UserRepository.GetByID(id);
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
                unitOfWork.UserRepository.Insert(user);
                unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            User user = unitOfWork.UserRepository.GetByID(id);
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
                    unitOfWork.UserRepository.Update(user);
                    unitOfWork.Save();
                }
                catch (DataException)
                {
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        public async Task<IActionResult> Delete(int? id)
        {

            User user = unitOfWork.UserRepository.GetByID(id);
            return View(user);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            User user = unitOfWork.UserRepository.GetByID(id);
            unitOfWork.UserRepository.Delete(id);
            unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }
        protected override void Dispose(bool disposing)
        {
            unitOfWork.Dispose();
            base.Dispose(disposing);
        }
    }
}
