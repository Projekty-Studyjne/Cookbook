using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Text.RegularExpressions;

namespace CookbookLibrary
{
    public class CookbookDbContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<CategoryRecipe> CategoryRecipes { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<IngredientRecipe> IngredientRecipes { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRecipe> UserRecipes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Cookbook;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetCallingAssembly());

            //modelBuilder.Entity<CategoryRecipe>().HasNoKey();
            //modelBuilder.Entity<IngredientRecipe>().HasNoKey();
            //modelBuilder.Entity<UserRecipe>().HasNoKey();

            modelBuilder.Entity<CategoryRecipe>().HasKey(ep => new { ep.categoryId, ep.recipeId });
            modelBuilder.Entity<UserRecipe>().HasKey(ep => new { ep.userId, ep.recipeId });
            modelBuilder.Entity<IngredientRecipe>().HasKey(ep => new { ep.ingredientId, ep.recipeId });

            //-------
            modelBuilder.Entity<CategoryRecipe>()
        .HasOne<Category>(cr => cr.Category)
        .WithMany(c => c.CategoryRecipes)
        .HasForeignKey(cr => cr.categoryId)
        .IsRequired()
        .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CategoryRecipe>()
                .HasOne<Recipe>(cr => cr.Recipe)
                .WithMany(r => r.CategoryRecipes)
                .HasForeignKey(cr => cr.recipeId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
            //---------
            modelBuilder.Entity<IngredientRecipe>()
                .HasOne<Recipe>(ir => ir.Recipe)
                .WithMany(r => r.IngredientRecipes)
                .HasForeignKey(ir => ir.recipeId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<IngredientRecipe>()
                .HasOne<Ingredient>(ir => ir.Ingredient)
                .WithMany(i => i.IngredientRecipes)
                .HasForeignKey(ir => ir.ingredientId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
            //------
            modelBuilder.Entity<UserRecipe>()
                .HasOne<Recipe>(ur => ur.Recipe)
                .WithMany(r => r.UserRecipes)
                .HasForeignKey(ur => ur.recipeId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserRecipe>()
                .HasOne<User>(ur => ur.User)
                .WithMany(u => u.UserRecipes)
                .HasForeignKey(ur => ur.userId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Rating>()
                .HasOne(r => r.User)
                .WithMany(u => u.Ratings)
                .HasForeignKey(r => r.userId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Rating>()
                .HasOne(r => r.Recipe)
                .WithMany(r => r.Ratings)
                .HasForeignKey(r => r.recipeId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.Rating)
                .WithOne(r => r.Comment)
                .HasForeignKey<Rating>(b => b.ratingId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Ingredient>().HasData(
    new Ingredient { ingredientId = 1, name = "Egg", category = "Protein" },
    new Ingredient { ingredientId = 2, name = "Milk", category = "Dairy" },
    new Ingredient { ingredientId = 3, name = "Flour", category = "Grains" }
);
            modelBuilder.Entity<IngredientRecipe>().HasData(
                new IngredientRecipe { ingredientId = 1, recipeId = 1, quantity = 4, unit = "large" },
                new IngredientRecipe { ingredientId = 2, recipeId = 1, quantity = 1 / 4, unit = "cup" },
                new IngredientRecipe { ingredientId = 1, recipeId = 2, quantity = 1, unit = "pound" },
                new IngredientRecipe { ingredientId = 3, recipeId = 2, quantity = 8, unit = "kilo" }
        );

            modelBuilder.Entity<Recipe>().HasData(
                new Recipe
                {
                    recipeId = 1,
                    title = "Scrambled Eggs",
                    description = "A classic breakfast dish",
                    instructions = "1. Whisk eggs and milk together in a bowl. 2. Melt butter in a nonstick skillet over medium heat. 3. Pour egg mixture into skillet and cook, stirring occasionally, until eggs are set but still moist, about 3-5 minutes. 4. Season with salt and pepper to taste.",
                    preparation_time = 10,
                    servings = 2
                },
                new Recipe
                {
                    recipeId = 2,
                    title = "Spaghetti Bolognese",
                    description = "A delicious Italian dinner",
                    instructions = "1. Cook spaghetti according to package instructions. 2. Heat oil in a large skillet over medium-high heat. 3. Add ground beef and cook until browned, stirring occasionally. 4. Add onion, carrot, and celery and cook until vegetables are softened. 5. Add garlic and cook until fragrant. 6. Add tomato paste, crushed tomatoes, and beef broth and bring to a simmer. 7. Reduce heat and let simmer until sauce has thickened, about 20-30 minutes. 8. Season with salt and pepper to taste. 9. Serve over spaghetti.",
                    preparation_time = 45,
                    servings = 4
                },
                new Recipe
                {
                    recipeId = 3,
                    title = "Chocolate Chip Cookies",
                    description = "A classic American dessert",
                    instructions = "1. Preheat oven to 375°F. 2. Cream together butter, white sugar, and brown sugar until smooth. 3. Beat in eggs one at a time, then stir in vanilla. 4. Dissolve baking soda in hot water and add to batter. 5. Stir in flour, chocolate chips, and nuts. 6. Drop by large spoonfuls onto ungreased pans. 7. Bake for about 10 minutes or until edges are nicely browned.",
                    preparation_time = 30,
                    servings = 24
                }
            );
            modelBuilder.Entity<Category>().HasData(
                  new Category { categoryId = 1, name = "Breakfast", description = "Delicious breakfast recipes" },
                  new Category { categoryId = 2, name = "Dinner", description = "Tasty dinner recipes" },
                  new Category { categoryId = 3, name = "Desserts", description = "Yummy desserts recipes" }
              );

            modelBuilder.Entity<CategoryRecipe>().HasData(
        new CategoryRecipe { categoryId = 1, recipeId = 1 },
                new CategoryRecipe { categoryId = 2, recipeId = 2 },
                new CategoryRecipe { categoryId = 3, recipeId = 3 }
    );


            modelBuilder.Entity<User>().HasData(
                new User { userId = 1, username = "johnsmith", email = "johnsmith@gmail.com", password = "Abcd1234!" },
                new User { userId = 2, username = "janedoe", email = "janedoe@yahoo.com", password = "Efg4567@" },
            new User { userId = 3, username = "pkow", email = "pkow2137@student.polsl.com", password = "2137" }
            );

            modelBuilder.Entity<UserRecipe>().HasData(
                new UserRecipe { userId = 1, recipeId = 1 },
                new UserRecipe { userId = 2, recipeId = 3 },
                new UserRecipe { userId = 3, recipeId = 2 }
            );

            modelBuilder.Entity<Comment>().HasData(
                new Comment { commentId = 1, comment = "Great recipe!", ratingId = 1 },
                new Comment { commentId = 2, comment = "I didn't like it very much", ratingId = 2 },
                new Comment { commentId = 3, comment = "This is my favorite dish", ratingId = 3 }
            );

            modelBuilder.Entity<Rating>().HasData(
                new Rating { ratingId = 1, rating = 5, userId = 1, recipeId = 1 },
                new Rating { ratingId = 2, rating = 2, userId = 2, recipeId = 1 },
                new Rating { ratingId = 3, rating = 4, userId = 3, recipeId = 2 }
            );
        }
    }
}
