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
    public class CommentsController : Controller
    {
        private ICommentService service;

        public CommentsController(ICommentService service)
        {
            this.service = service;
        }

        public async Task<ViewResult> Index()
        {
            var comment = await service.GetAll();
            return View(comment.ToList());
        }

        public async Task<ViewResult> Details(int id)
        {
            Comment comment = await service.GetCommentById(id);
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
                service.Add(comment);
                return RedirectToAction(nameof(Index));
            }
            return View(comment);
        }

        public async Task<IActionResult> Edit(int id)
        {
            Comment comment = await service.GetCommentById(id);
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
                    service.Update(comment);
                }
                catch (DataException)
                {
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
                return RedirectToAction(nameof(Index));
            }
            return View(comment);
        }

        public async Task<IActionResult> Delete(int id)
        {
            Comment comment = await service.GetCommentById(id);
            return View(comment);
        }

        // POST: Comments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Comment comment = await service.GetCommentById(id);
            service.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
