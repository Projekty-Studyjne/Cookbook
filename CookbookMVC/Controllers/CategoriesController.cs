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
using System.Xml;

namespace CookbookMVC.Controllers
{
    public class CategoriesController : Controller
    {
        private IUnitOfWork unitOfWork;

        public CategoriesController(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }

        public ViewResult Index()
        {
            var category = unitOfWork.CategoryRepository.Get(includeProperties: "CategoryRecipes");
            return View(category.ToList());
        }

        public ViewResult Details(int id)
        {
            Category category = unitOfWork.CategoryRepository.GetByID(id);
            return View(category);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("categoryId,name,description")] Category category)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.CategoryRepository.Insert(category);
                unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            Category category = unitOfWork.CategoryRepository.GetByID(id);
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("categoryId,name,description")] Category category)
        {
            if (id != category.categoryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    unitOfWork.CategoryRepository.Update(category);
                    unitOfWork.Save();
                }
                catch (DataException)
                {
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        public async Task<IActionResult> Delete(int? id)
        {

            Category category = unitOfWork.CategoryRepository.GetByID(id);
            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Category category = unitOfWork.CategoryRepository.GetByID(id);
            unitOfWork.CategoryRepository.Delete(id);
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
