using CookbookLibrary.Entities;
using CookbookLibrary.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CookbookLibrary.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private CookbookDbContext context = new CookbookDbContext();
        private GenericRepository<Category> categoryRepository;
        private GenericRepository<CategoryRecipe> categoryRecipeRepository;
        private GenericRepository<Comment> commentRepository;
        private GenericRepository<Ingredient> ingredientRepository;
        private GenericRepository<IngredientRecipe> ingredientRecipeRepository;
        private GenericRepository<Rating> ratingRepository;
        private GenericRepository<Recipe> recipeRepository;
        private GenericRepository<User> userRepository;
        private GenericRepository<UserRecipe> userRecipeRepository;

        public GenericRepository<Category> CategoryRepository
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

        public GenericRepository<CategoryRecipe> CategoryRecipeRepository
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

        public GenericRepository<Comment> CommentRepository
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

        public GenericRepository<Ingredient> IngredientRepository
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

        public GenericRepository<IngredientRecipe> IngredientRecipeRepository
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

        public GenericRepository<Rating> RatingRepository
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

        public GenericRepository<Recipe> RecipeRepository
        {
            get
            {

                if (this.recipeRepository == null)
                {
                    this.recipeRepository = new GenericRepository<Recipe>(context);
                }
                return recipeRepository;
            }
        }
        public GenericRepository<User> UserRepository
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

        public GenericRepository<UserRecipe> UserRecipeRepository
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
