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

namespace CookbookMVC.Controllers
{
    public class RatingsController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();

        public ViewResult Index()
        {
            var rating = unitOfWork.RatingRepository.Get(includeProperties: "Comment,User,Recipe");
            return View(rating.ToList());
        }

        public ViewResult Details(int id)
        {
            Rating rating = unitOfWork.RatingRepository.GetByID(id);
            return View(rating);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ratingId,rating,userId,recipeId")] Rating rating)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.RatingRepository.Insert(rating); 
                unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(rating);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            Rating rating = unitOfWork.RatingRepository.GetByID(id);
            return View(rating);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ratingId,rating,userId,recipeId")] Rating rating)
        {
            if (id != rating.ratingId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    unitOfWork.RatingRepository.Update(rating);
                    unitOfWork.Save();
                }
                catch (DataException)
                {
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
                return RedirectToAction(nameof(Index));
            }
            return View(rating);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            Rating rating = unitOfWork.RatingRepository.GetByID(id);
            return View(rating);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Rating rating = unitOfWork.RatingRepository.GetByID(id);
            unitOfWork.RatingRepository.Delete(id);
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
