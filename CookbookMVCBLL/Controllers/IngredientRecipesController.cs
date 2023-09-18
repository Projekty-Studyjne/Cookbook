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
using Microsoft.Ajax.Utilities;
using CookbookBLL;
using CookbookLibrary.Migrations;

namespace CookbookMVCBLL.Controllers
{
    public class IngredientRecipesController : Controller
    {
        private IIngredientRecipeService service;
        private static int recipeIdtemp, ingredientIdtemp;

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


        public IActionResult Create(int ingredientId)
        {
            //var ingredientIds = new List<int> { 1, 2, 3 };
            ingredientIdtemp = ingredientId;
            recipeIdtemp = (int)TempData["recipeId"];
            return View();
        }

        public IActionResult AddToDatabase([Bind("quantity, unit")] IngredientRecipe ingredientRecipe)
        {
            IngredientRecipe ingredientRecipeTemp = new IngredientRecipe();
            ingredientRecipeTemp.recipeId = recipeIdtemp;
            ingredientRecipeTemp.ingredientId = ingredientIdtemp;
            ingredientRecipeTemp.unit = ingredientRecipe.unit;
            ingredientRecipeTemp.quantity = ingredientRecipe.quantity;
            service.Add(ingredientRecipeTemp);
            return RedirectToAction("SelectIngredients", "Ingredients");
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("ingredientId,recipeId,quantity,unit")] IngredientRecipe ingredientRecipe)
        //{
        //    //var ingredientIds = new List<int> { 1, 2, 3 };
        //    //ViewBag.IngredientIds = new SelectList(ingredientIds);
        //    service.Add(ingredientRecipe);
        //    return RedirectToAction(nameof(Index));
        //    return View(ingredientRecipe);
        //}
        //[HttpGet]
        //public IActionResult Create(IEnumerable<int> selectedIngredients)
        //{
        //    ViewBag.SelectedIngredients = selectedIngredients;
        //    return View(selectedIngredients);
        //}
        //public IActionResult Create(List<int> selectedIngredients)
        //{
        //    //List<int> selectedIngredients = TempData["SelectedIngredients"] as List<int>;
        //    //List<SelectListItem> selectListItems = selectedIngredients.Select(id => new SelectListItem { Value = id.ToString(), Text = id.ToString() }).ToList();
        //    //ViewBag.SelectedIngredients = new SelectList(selectListItems, "Value", "Text");
        //    //List<int> selectedIngredients = TempData["SelectedIngredients"] as List<int>;
        //    //return View(selectedIngredients);
        //    //List<int> selectedIngredients = TempData["SelectedIngredients"] as List<int>;
        //    return View(selectedIngredients);
        //}
        //public IActionResult Create()
        //{
        //    List<int> selectedIngredients = TempData["SelectedIngredients"] as List<int>;

        //    // Tutaj możesz wykonać odpowiednie operacje na wybranych składnikach.
        //    // Możesz również przekształcić te składniki na SelectListItem lub inny format, który jest potrzebny w Twoim widoku.

        //    ViewBag.SelectedIngredients = selectedIngredients;

        //    return View();
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("ingredientId,recipeId,quantity,unit")] IngredientRecipe ingredientRecipe)
        //{
        //    service.Add(ingredientRecipe);
        //    return RedirectToAction(nameof(Index));
        //    return View(ingredientRecipe);
        //}

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
