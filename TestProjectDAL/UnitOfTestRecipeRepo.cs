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
        public void TestGetRecipes()
        {
            var options = new DbContextOptionsBuilder<CookbookDbContext>().UseMemoryDatabase
            var uczelniaContext = new UczelniaContext(options);
            StudentRepo studentRepo = new StudentRepo(uczelniaContext);

            Assert.Empty(studentRepo.GetStudents());
            studentRepo.InsertStudent(new Model.Student { StudentId = 1, Nazwisko = "Kowal", Ocena = 3 });
            studentRepo.Save();
            Assert.Equal(1, studentRepo.GetStudents().Count());
        }
    }
    }
}
