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

namespace CookbookMVCBLL.Controllers
{
    public class CategoryRecipesController : Controller
    {
        private ICategoryRecipeService service;

        public CategoryRecipesController(ICategoryRecipeService service)
        {
            this.service = service;
        }

        public async Task<ViewResult> Index()
        {
            var categoryRecipe = await service.GetAll();
            return View(categoryRecipe.ToList());
        }

        public async Task<ViewResult> Details(int id)
        {
            CategoryRecipe categoryRecipe = await service.GetCategoryRecipeById(id);
            return View(categoryRecipe);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("categoryId, recipeId")] CategoryRecipe categoryRecipe)
        {

            service.Add(categoryRecipe);
            return RedirectToAction(nameof(Index));
            return View(categoryRecipe);
        }

        public async Task<IActionResult> Edit(int id)
        {
            CategoryRecipe categoryRecipe = await service.GetCategoryRecipeById(id);
            return View(categoryRecipe);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("categoryId, recipeId")] CategoryRecipe categoryRecipe)
        {
            if (id != categoryRecipe.recipeId && id != categoryRecipe.categoryId)
            {
                return NotFound();
            }

            try
            {
                service.Update(categoryRecipe);
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }

            return RedirectToAction(nameof(Index));

            return View(categoryRecipe);
        }

        public async Task<IActionResult> Delete(int id)
        {
            CategoryRecipe categoryRecipe = await service.GetCategoryRecipeById(id);
            await service.Delete(categoryRecipe.categoryId);
            return View(categoryRecipe);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            CategoryRecipe categoryRecipe = await service.GetCategoryRecipeById(id);
            service.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}