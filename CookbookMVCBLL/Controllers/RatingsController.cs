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
    public class RatingsController : Controller
    {
        private IRatingService service;

        public RatingsController(IRatingService service)
        {
            this.service = service;
        }


        public async Task<ViewResult> Index()
        {
            var rating = await service.GetAll();
            return View(rating.ToList());
        }

        public async Task<ViewResult> Details(int id)
        {
            Rating rating = await service.GetRatingById(id);
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

                service.Add(rating);
                return RedirectToAction(nameof(Index));
            return View(rating);
        }

        public async Task<IActionResult> Edit(int id)
        {
            Rating rating = await service.GetRatingById(id);
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

                try
                {
                    service.Update(rating);
                }
                catch (DataException)
                {
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
                return RedirectToAction(nameof(Index));

            return View(rating);
        }

        public async Task<IActionResult> Delete(int id)
        {
            Rating rating = await service.GetRatingById(id);
            await service.Delete(rating.ratingId);
            return View(rating);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Rating rating = await service.GetRatingById(id);
            service.Delete(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
