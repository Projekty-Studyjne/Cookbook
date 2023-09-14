using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CookbookLibrary;
using CookbookLibrary.Entities;
using CookbookBLL.Interfaces;
using System.Data;
using CookbookBLL;

namespace CookbookMVCBLL.Controllers
{
    public class UserRecipesController : Controller
    {
        private IUserRecipeService service;

        public UserRecipesController(IUserRecipeService service)
        {
            this.service = service;
        }

        public async Task<ViewResult> Index()
        {
            var userRecipe = await service.GetAll();
            return View(userRecipe.ToList());
        }

        public async Task<ViewResult> Details(int id)
        {
            UserRecipe userRecipe = await service.GetUserRecipeById(id);
            return View(userRecipe);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("userId, recipeId")] UserRecipe userRecipe)
        {

            service.Add(userRecipe);
            return RedirectToAction(nameof(Index));
            return View(userRecipe);
        }

        public async Task<IActionResult> Edit(int id)
        {
            UserRecipe userRecipe = await service.GetUserRecipeById(id);
            return View(userRecipe);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("userId, recipeId")] UserRecipe userRecipe)
        {
            if (id != userRecipe.userId && id != userRecipe.recipeId)
            {
                return NotFound();
            }

            try
            {
                service.Update(userRecipe);
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }

            return RedirectToAction(nameof(Index));

            return View(userRecipe);
        }

        public async Task<IActionResult> Delete(int id)
        {
            UserRecipe userRecipe = await service.GetUserRecipeById(id);
            await service.Delete(userRecipe.userId);
            return View(userRecipe);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            UserRecipe userRecipe = await service.GetUserRecipeById(id);
            service.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
