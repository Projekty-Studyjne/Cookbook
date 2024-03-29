﻿using System;
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
using System.Xml;
using CookbookBLL.Interfaces;
using System.Diagnostics;
using NuGet.Protocol;
using CookbookBLL;

namespace CookbookMVC.Controllers
{
    public class CategoriesController : Controller
    {
        private ICategoryService service;

        public CategoriesController(ICategoryService service)
        {
            this.service = service;
        }

        public async Task <ViewResult> Index()
        {
            var category = await service.GetAll();
            return View(category.ToList());
        }

        public async Task<ViewResult> Details(int id)
        {
            Category category = await service.GetCategoryById(id);  
            return View(category);
        }

        public IActionResult Create()
        {
            return View();
        }


        public IActionResult AddCategoryToRecipe(int categoryId)
        {
            int recipeId = RecipeService.getRecipeId();
            return RedirectToAction("AddCategoryToRecipe", "Recipes", new { categoryId = categoryId, recipeId = recipeId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("categoryId,name,description")] Category category)
        {
                service.Add(category);
                return RedirectToAction(nameof(Index));
            return View(category);
        }

        public async Task<IActionResult> Edit(int id)
        {
            Category category = await service.GetCategoryById(id);
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,Category category)
        {
            if (id != category.categoryId)
            {
                return NotFound();
            }
                try
                {
                    service.Update(category);
                }
                catch (DataException)
                {
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
                return RedirectToAction(nameof(Index));
            return View(category);
        }

        public async Task<IActionResult> Delete(int id)
        {
            Category category = await service.GetCategoryById(id);
            await service.Delete(category.categoryId);
            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Category category = await service.GetCategoryById(id);
            service.Delete(id);
            return RedirectToAction(nameof(Index));
        }


    }
}
