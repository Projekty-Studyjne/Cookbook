using CookbookLibrary.Entities;
using CookbookLibrary.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookbookLibrary.Repositories
{
    public class CommentRepository : ICommentRepository, IDisposable
    {
        private CookbookDbContext context;

        public CommentRepository(CookbookDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<Comment> GetComments()
        {
            return context.Comments.ToList();
        }

        public Comment GetCommentById(int id)
        {
            return context.Comments.Find(id);
        }

        public void InsertComment(Comment comment)
        {
            context.Comments.Add(comment);
        }

        public void DeleteComment(int commentID)
        {
            Comment comment = context.Comments.Find(commentID);
            context.Comments.Remove(comment);
        }

        public void UpdateComment(Comment comment)
        {
            context.Entry(comment).State = EntityState.Modified;
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
