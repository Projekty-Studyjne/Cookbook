using CookbookLibrary;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CookbookLibrary.Repositories;

namespace TestProjectDAL
{
    public class UnitOfTestRecipeRepo
    {
        [Fact]
        public void TestUnitOfWork()
        {
            var recipeRepo = new RecipeRepoDummy();
            var unitOfWork = new UnitOfWork();
            Assert.Same(recipeRepo, unitOfWork.RecipeRepository);
        }
    }
    
}
