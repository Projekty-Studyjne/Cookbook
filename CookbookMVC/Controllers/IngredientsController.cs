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
    public class IngredientsController : Controller
    {
        private IUnitOfWork unitOfWork;

        public IngredientsController(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }

        public ViewResult Index()
        {
            var ingredient = unitOfWork.IngredientRepository.Get(includeProperties: "IngredientRecipes");
            return View(ingredient.ToList());
        }

        public ViewResult Details(int id)
        {
            Ingredient ingredient = unitOfWork.IngredientRepository.GetByID(id);
            return View(ingredient);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ingredientId,name,category")] Ingredient ingredient)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.IngredientRepository.Insert(ingredient);
                await unitOfWork.SaveAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ingredient);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            Ingredient ingredient = unitOfWork.IngredientRepository.GetByID(id);
            return View(ingredient);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ingredientId,name,category")] Ingredient ingredient)
        {
            if (id != ingredient.ingredientId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    unitOfWork.IngredientRepository.Update(ingredient);
                    await unitOfWork.SaveAsync();
                }
                catch (DataException)
                {
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
                return RedirectToAction(nameof(Index));
            }
            return View(ingredient);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            Ingredient ingredient = unitOfWork.IngredientRepository.GetByID(id);

            return View(ingredient);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Ingredient ingredient = unitOfWork.IngredientRepository.GetByID(id);
            unitOfWork.IngredientRepository.Delete(id);
            await unitOfWork.SaveAsync();
            return RedirectToAction(nameof(Index));
        }

        protected override void Dispose(bool disposing)
        {
            unitOfWork.Dispose();
            base.Dispose(disposing);
        }
    }
}
