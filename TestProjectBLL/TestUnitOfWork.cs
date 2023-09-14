using CookbookLibrary;
using CookbookLibrary.Entities;
using CookbookLibrary.Repositories;
using CookbookLibrary.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProjectBLL
{
    public class TestUnitOfWork : IUnitOfWork
    {
        private CookbookDbContext context = new CookbookDbContext();
        private IGenericRepository<Category> categoryRepository;
        private IGenericRepository<CategoryRecipe> categoryRecipeRepository;
        private IGenericRepository<Comment> commentRepository;
        private IGenericRepository<Ingredient> ingredientRepository;
        private IGenericRepository<IngredientRecipe> ingredientRecipeRepository;
        private IGenericRepository<Rating> ratingRepository;
        private IGenericRepository<Recipe> recipeRepository;
        private IGenericRepository<User> userRepository;
        private IGenericRepository<UserRecipe> userRecipeRepository;


        public TestUnitOfWork(RecipeRepoFake? recipeRepoFake, CategoryRecipeRepoFake categoryRecipeRepoFake)
        {
            this.recipeRepository = recipeRepoFake;
            this.categoryRecipeRepository = categoryRecipeRepoFake;
        }

        public TestUnitOfWork(RecipeRepoFake recipeRepoFake, IngredientRepoFake ingredientRepoFake)
        {
            this.recipeRepository = recipeRepoFake;
            this.ingredientRepository = ingredientRepoFake;
        }

        public TestUnitOfWork(IngredientRepoFake ingredientRepoFake)
        {
            this.ingredientRepository = ingredientRepoFake;
        }

        public TestUnitOfWork(RecipeRepoFake recipeRepoFake)
        {
            this.recipeRepository = recipeRepoFake;
        }

        public TestUnitOfWork(CommentRepoFake commentRepoFake)
        {
            this.commentRepository = commentRepoFake;
        }

        public IGenericRepository<Category> CategoryRepository
        {
            get
            {

                if (this.categoryRepository == null)
                {
                    this.categoryRepository = new GenericRepository<Category>(context);
                }
                return categoryRepository;
            }
        }

        public IGenericRepository<CategoryRecipe> CategoryRecipeRepository
        {
            get
            {

                if (this.categoryRecipeRepository == null)
                {
                    this.categoryRecipeRepository = new GenericRepository<CategoryRecipe>(context);
                }
                return categoryRecipeRepository;
            }
        }

        public IGenericRepository<Comment> CommentRepository
        {
            get
            {

                if (this.commentRepository == null)
                {
                    this.commentRepository = new GenericRepository<Comment>(context);
                }
                return commentRepository;
            }
        }

        public IGenericRepository<Ingredient> IngredientRepository
        {
            get
            {

                if (this.ingredientRepository == null)
                {
                    this.ingredientRepository = new GenericRepository<Ingredient>(context);
                }
                return ingredientRepository;
            }
        }

        public IGenericRepository<IngredientRecipe> IngredientRecipeRepository
        {
            get
            {

                if (this.ingredientRecipeRepository == null)
                {
                    this.ingredientRecipeRepository = new GenericRepository<IngredientRecipe>(context);
                }
                return ingredientRecipeRepository;
            }
        }

        public IGenericRepository<Rating> RatingRepository
        {
            get
            {

                if (this.ratingRepository == null)
                {
                    this.ratingRepository = new GenericRepository<Rating>(context);
                }
                return ratingRepository;
            }
        }

        public IGenericRepository<Recipe> RecipeRepository
        {
            get
            {

                if (this.recipeRepository == null)
                {
                    this.recipeRepository = new RecipeRepoFake();
                }
                return recipeRepository;
            }
        }

        public IGenericRepository<User> UserRepository
        {
            get
            {

                if (this.userRepository == null)
                {
                    this.userRepository = new GenericRepository<User>(context);
                }
                return userRepository;
            }
        }

        public IGenericRepository<UserRecipe> UserRecipeRepository
        {
            get
            {

                if (this.userRecipeRepository == null)
                {
                    this.userRecipeRepository = new GenericRepository<UserRecipe>(context);
                }
                return userRecipeRepository;
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
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

        public async Task SaveAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}

