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
using Microsoft.AspNetCore.Http;
using System.Data;

namespace CookbookMVCBLL.Controllers
{
    public class IngredientsController : Controller
    {
        private IIngredientService service;

        public IngredientsController(IIngredientService service)
        {
            this.service = service;
        }

        public async Task<ViewResult> Index()
        {
            var ingredient = await service.GetAll();
            return View(ingredient.ToList());
        }

  
        public async Task<ViewResult> Details(int id)
        {
            Ingredient ingredient = await service.GetIngredientById(id);
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
                service.Add(ingredient);
                return RedirectToAction(nameof(Index));
            return View(ingredient);
        }

        public async Task<IActionResult> Edit(int id)
        {
            Ingredient ingredient = await service.GetIngredientById(id);
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

                try
                {
                    service.Update(ingredient);
                    
                }
                catch (DataException)
                {
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
                return RedirectToAction(nameof(Index));
            return View(ingredient);
        }

        public async Task<IActionResult> Delete(int id)
        {
            Ingredient ingredient = await service.GetIngredientById(id);
            await service.Delete(ingredient.ingredientId);
            return View(ingredient);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Ingredient ingredient = await service.GetIngredientById(id);
            service.Delete(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
