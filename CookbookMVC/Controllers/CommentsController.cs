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
    public class CommentsController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();

        public ViewResult Index()
        {
            var comment = unitOfWork.CommentRepository.Get(includeProperties: "Rating");
            return View(comment.ToList());
        }

        public ViewResult Details(int id)
        {
            Comment comment = unitOfWork.CommentRepository.GetByID(id);
            return View(comment);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("commentId,comment,ratingId")] Comment comment)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.CommentRepository.Insert(comment);
                unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(comment);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            Comment comment = unitOfWork.CommentRepository.GetByID(id);
            return View(comment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("commentId,comment,ratingId")] Comment comment)
        {
            if (id != comment.commentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    unitOfWork.CommentRepository.Update(comment);
                    unitOfWork.Save();
                }
                catch (DataException)
                {
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
                return RedirectToAction(nameof(Index));
            }
            return View(comment);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            Comment comment = unitOfWork.CommentRepository.GetByID(id);
            return View(comment);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Comment comment = unitOfWork.CommentRepository.GetByID(id);
            unitOfWork.CommentRepository.Delete(id);
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
