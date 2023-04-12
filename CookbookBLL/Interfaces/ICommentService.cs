using CookbookLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookbookBLL.Interfaces
{
    internal interface ICommentService
    {
        Task<IEnumerable<Comment>> GetAll();
        Task<Comment> GetCommentById(int commentId);
        Task Update(Comment comment);
        Task Add(Comment comment);
        Task Delete(int commentId);
    }
}
