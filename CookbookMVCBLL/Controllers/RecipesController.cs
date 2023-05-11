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
using CookbookLibrary.Repositories;
using System.Data;

namespace CookbookMVCBLL.Controllers
{
    public class RecipesController : Controller
    {
        private IRecipeService service;

        public RecipesController(IRecipeService service)
        {
            this.service = service;
        }
        public async Task<IActionResult> Index()
        {
            var recipes = await service.GetAll();
            return View(recipes.ToList());
        }
        public async Task<ViewResult> Details(int id)
        {
            Recipe recipe = await service.GetRecipeById(id);
            return View(recipe);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("recipeId,title,description,instructions,preparation_time,servings")] Recipe recipe)
        {
            if (ModelState.IsValid)
            {
                service.Add(recipe);
                return RedirectToAction(nameof(Index));
            }
            return View(recipe);
        }

        public async Task<IActionResult> Edit(int id)
        {
            Recipe recipe = await service.GetRecipeById(id);
            return View(recipe);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("recipeId,title,description,instructions,preparation_time,servings")] Recipe recipe)
        {
            if (id != recipe.recipeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    service.Update(recipe);
                }
                catch (DataException)
                {
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
                return RedirectToAction(nameof(Index));
            }
            return View(recipe);
        }

        public async Task<IActionResult> Delete(int id)
        {

            Recipe recipe = await service.GetRecipeById(id);
            return View(recipe);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Recipe recipe = await service.GetRecipeById(id);
            service.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> RecipesByIngredient(int ingredientId)
        {
            var recipes = await service.GetRecipesByIngredient(ingredientId);

            return View(recipes);
        }
        public async Task<IActionResult> RecipeByCategory(int categoryId)
        {
            var recipe = await service.GetRecipesByCategory(categoryId);
            return View(recipe);
        }
    }
}
