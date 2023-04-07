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

namespace CookbookMVC.Controllers
{
    public class RecipesController : Controller
    {
        private IUnitOfWork unitOfWork;

        public RecipesController(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }

        public ViewResult Index()
        {
            var recipe = unitOfWork.RecipeRepository.Get(includeProperties: "IngredientRecipes,UserRecipes,CategoryRecipes,Ratings");
            return View(recipe.ToList());
        }

        public ViewResult Details(int id)
        {
            Recipe recipe = unitOfWork.RecipeRepository.GetByID(id);
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
                unitOfWork.RecipeRepository.Insert(recipe);
                unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(recipe);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            Recipe recipe = unitOfWork.RecipeRepository.GetByID(id);
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
                    unitOfWork.RecipeRepository.Update(recipe);
                    unitOfWork.Save();
                }
                catch (DataException)
                {
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
                return RedirectToAction(nameof(Index));
            }
            return View(recipe);
        }

        public async Task<IActionResult> Delete(int? id)
        {

            Recipe recipe = unitOfWork.RecipeRepository.GetByID(id);
            return View(recipe);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Recipe recipe = unitOfWork.RecipeRepository.GetByID(id);
            unitOfWork.RecipeRepository.Delete(id);
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
