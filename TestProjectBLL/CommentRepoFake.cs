using CookbookLibrary.Entities;
using CookbookLibrary.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TestProjectBLL
{
    public class CommentRepoFake : IGenericRepository<Comment>
    {
        private List<Comment> comments = new List<Comment>();
        public void Delete(object id)
        {
            Comment comment = comments.Find(s => s.commentId == (int)id);
            comments.Remove(comment);
        }

        public void Delete(Comment entityToDelete)
        {
            comments.Remove(entityToDelete);
        }

        public Task<IEnumerable<Comment>> GetAsync(Expression<Func<Comment, bool>> filter = null, Func<IQueryable<Comment>, IOrderedQueryable<Comment>> orderBy = null, string includeProperties = "")
        {
            IQueryable<Comment> query = comments.AsQueryable();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return Task.FromResult<IEnumerable<Comment>>(orderBy(query).ToList());
            }
            else
            {
                return Task.FromResult<IEnumerable<Comment>>(query.ToList());
            }
        }

        public Comment GetByID(object id)
        {
            return comments.FirstOrDefault(e => e.commentId == (int)id);
        }

        public void Insert(Comment entity)
        {
            comments.Add(entity);
        }

        public void Update(Comment entityToUpdate)
        {
            int index = this.comments.FindIndex(s => s.commentId == entityToUpdate.commentId);
            if (index != -1)
                comments[index] = entityToUpdate;
        }
    }
}
