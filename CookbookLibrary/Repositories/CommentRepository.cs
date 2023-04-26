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
    public class CommentRepository : GenericRepository<CommentRepository>
    {
        public CommentRepository(CookbookDbContext context): base(context)
        {

        }
    }
}
