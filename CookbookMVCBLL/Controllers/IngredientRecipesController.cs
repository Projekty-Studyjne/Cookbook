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
    public class IngredientRecipesController : Controller
    {
        private IIngredientRecipeService service;

        public IngredientRecipesController(IIngredientRecipeService service)
        {
            this.service = service;
        }

        public async Task<ViewResult> Index()
        {
            var ingredientRecipe = await service.GetAll();
            return View(ingredientRecipe.ToList());
        }

        public async Task<ViewResult> Details(int id)
        {
            IngredientRecipe ingredientRecipe = await service.GetIngredientRecipeById(id);
            return View(ingredientRecipe);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ingredientId,recipeId,quantity,unit")] IngredientRecipe ingredientRecipe)
        {

            service.Add(ingredientRecipe);
            return RedirectToAction(nameof(Index));
            return View(ingredientRecipe);
        }

        public async Task<IActionResult> Edit(int id)
        {
            IngredientRecipe ingredientRecipe = await service.GetIngredientRecipeById(id);
            return View(ingredientRecipe);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ingredientId,recipeId,quantity,unit")] IngredientRecipe ingredientRecipe)
        {
            if (id != ingredientRecipe.recipeId && id != ingredientRecipe.ingredientId)
            {
                return NotFound();
            }

            try
            {
                service.Update(ingredientRecipe);
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }

            return RedirectToAction(nameof(Index));

            return View(ingredientRecipe);
        }

        public async Task<IActionResult> Delete(int id)
        {
            IngredientRecipe ingredientRecipe = await service.GetIngredientRecipeById(id);
            await service.Delete(ingredientRecipe.recipeId);
            return View(ingredientRecipe);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            IngredientRecipe ingredientRecipe = await service.GetIngredientRecipeById(id);
            service.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
