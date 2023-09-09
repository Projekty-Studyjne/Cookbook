using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CookbookLibrary.Migrations
{
    /// <inheritdoc />
    public partial class addImageUrl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    categoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.categoryId);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    commentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    comment = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    ratingId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.commentId);
                });

            migrationBuilder.CreateTable(
                name: "Ingredients",
                columns: table => new
                {
                    ingredientId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    category = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingredients", x => x.ingredientId);
                });

            migrationBuilder.CreateTable(
                name: "Recipes",
                columns: table => new
                {
                    recipeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    imageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    instructions = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    preparation_time = table.Column<double>(type: "float", nullable: false),
                    servings = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipes", x => x.recipeId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    userId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.userId);
                });

            migrationBuilder.CreateTable(
                name: "CategoryRecipe",
                columns: table => new
                {
                    categoryId = table.Column<int>(type: "int", nullable: false),
                    recipeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryRecipe", x => new { x.categoryId, x.recipeId });
                    table.ForeignKey(
                        name: "FK_CategoryRecipe_Categories_categoryId",
                        column: x => x.categoryId,
                        principalTable: "Categories",
                        principalColumn: "categoryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoryRecipe_Recipes_recipeId",
                        column: x => x.recipeId,
                        principalTable: "Recipes",
                        principalColumn: "recipeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IngredientRecipe",
                columns: table => new
                {
                    ingredientId = table.Column<int>(type: "int", nullable: false),
                    recipeId = table.Column<int>(type: "int", nullable: false),
                    quantity = table.Column<float>(type: "real", nullable: false),
                    unit = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IngredientRecipe", x => new { x.ingredientId, x.recipeId });
                    table.ForeignKey(
                        name: "FK_IngredientRecipe_Ingredients_ingredientId",
                        column: x => x.ingredientId,
                        principalTable: "Ingredients",
                        principalColumn: "ingredientId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IngredientRecipe_Recipes_recipeId",
                        column: x => x.recipeId,
                        principalTable: "Recipes",
                        principalColumn: "recipeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ratings",
                columns: table => new
                {
                    ratingId = table.Column<int>(type: "int", nullable: false),
                    rating = table.Column<int>(type: "int", nullable: false),
                    userId = table.Column<int>(type: "int", nullable: false),
                    recipeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ratings", x => x.ratingId);
                    table.ForeignKey(
                        name: "FK_Ratings_Comments_ratingId",
                        column: x => x.ratingId,
                        principalTable: "Comments",
                        principalColumn: "commentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ratings_Recipes_recipeId",
                        column: x => x.recipeId,
                        principalTable: "Recipes",
                        principalColumn: "recipeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ratings_Users_userId",
                        column: x => x.userId,
                        principalTable: "Users",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRecipe",
                columns: table => new
                {
                    userId = table.Column<int>(type: "int", nullable: false),
                    recipeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRecipe", x => new { x.userId, x.recipeId });
                    table.ForeignKey(
                        name: "FK_UserRecipe_Recipes_recipeId",
                        column: x => x.recipeId,
                        principalTable: "Recipes",
                        principalColumn: "recipeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRecipe_Users_userId",
                        column: x => x.userId,
                        principalTable: "Users",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "categoryId", "description", "name" },
                values: new object[,]
                {
                    { 1, "Delicious breakfast recipes", "Breakfast" },
                    { 2, "Tasty dinner recipes", "Dinner" },
                    { 3, "Yummy desserts recipes", "Desserts" }
                });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "commentId", "comment", "ratingId" },
                values: new object[,]
                {
                    { 1, "Great recipe!", 1 },
                    { 2, "I didn't like it very much", 2 },
                    { 3, "This is my favorite dish", 3 }
                });

            migrationBuilder.InsertData(
                table: "Ingredients",
                columns: new[] { "ingredientId", "category", "name" },
                values: new object[,]
                {
                    { 1, "Protein", "Egg" },
                    { 2, "Dairy", "Milk" },
                    { 3, "Grains", "Flour" }
                });

            migrationBuilder.InsertData(
                table: "Recipes",
                columns: new[] { "recipeId", "description", "imageUrl", "instructions", "preparation_time", "servings", "title" },
                values: new object[,]
                {
                    { 1, "A classic breakfast dish", "https://www.allrecipes.com/thmb/HcdHiuwiNOIlOISGKGTI0KxhR7E=/750x0/filters:no_upscale():max_bytes(150000):strip_icc():format(webp)/JF_241160_CreamyCottageCheeseScrambled_4x3_12902-619d00dc88594ea9b8ed884a108db16d.jpg", "1. Whisk eggs and milk together in a bowl. 2. Melt butter in a nonstick skillet over medium heat. 3. Pour egg mixture into skillet and cook, stirring occasionally, until eggs are set but still moist, about 3-5 minutes. 4. Season with salt and pepper to taste.", 10.0, 2, "Scrambled Eggs" },
                    { 2, "A delicious Italian dinner", "https://www.slimmingeats.com/blog/wp-content/uploads/2010/04/spaghetti-bolognese-36-735x735.jpg", "1. Cook spaghetti according to package instructions. 2. Heat oil in a large skillet over medium-high heat. 3. Add ground beef and cook until browned, stirring occasionally. 4. Add onion, carrot, and celery and cook until vegetables are softened. 5. Add garlic and cook until fragrant. 6. Add tomato paste, crushed tomatoes, and beef broth and bring to a simmer. 7. Reduce heat and let simmer until sauce has thickened, about 20-30 minutes. 8. Season with salt and pepper to taste. 9. Serve over spaghetti.", 45.0, 4, "Spaghetti Bolognese" },
                    { 3, "A classic American dessert", "https://images.aws.nestle.recipes/original/5b069c3ed2feea79377014f6766fcd49_Original_NTH_Chocolate_Chip_Cookie.jpg", "1. Preheat oven to 375°F. 2. Cream together butter, white sugar, and brown sugar until smooth. 3. Beat in eggs one at a time, then stir in vanilla. 4. Dissolve baking soda in hot water and add to batter. 5. Stir in flour, chocolate chips, and nuts. 6. Drop by large spoonfuls onto ungreased pans. 7. Bake for about 10 minutes or until edges are nicely browned.", 30.0, 24, "Chocolate Chip Cookies" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "userId", "email", "password", "username" },
                values: new object[,]
                {
                    { 1, "johnsmith@gmail.com", "Abcd1234!", "johnsmith" },
                    { 2, "janedoe@yahoo.com", "Efg4567@", "janedoe" },
                    { 3, "pkow2137@student.polsl.com", "2137", "pkow" }
                });

            migrationBuilder.InsertData(
                table: "CategoryRecipe",
                columns: new[] { "categoryId", "recipeId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 2 },
                    { 3, 3 }
                });

            migrationBuilder.InsertData(
                table: "IngredientRecipe",
                columns: new[] { "ingredientId", "recipeId", "quantity", "unit" },
                values: new object[,]
                {
                    { 1, 1, 4f, "large" },
                    { 1, 2, 1f, "pound" },
                    { 2, 1, 0f, "cup" },
                    { 3, 2, 8f, "kilo" }
                });

            migrationBuilder.InsertData(
                table: "Ratings",
                columns: new[] { "ratingId", "rating", "recipeId", "userId" },
                values: new object[,]
                {
                    { 1, 5, 1, 1 },
                    { 2, 2, 1, 2 },
                    { 3, 4, 2, 3 }
                });

            migrationBuilder.InsertData(
                table: "UserRecipe",
                columns: new[] { "recipeId", "userId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 3, 2 },
                    { 2, 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CategoryRecipe_recipeId",
                table: "CategoryRecipe",
                column: "recipeId");

            migrationBuilder.CreateIndex(
                name: "IX_IngredientRecipe_recipeId",
                table: "IngredientRecipe",
                column: "recipeId");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_recipeId",
                table: "Ratings",
                column: "recipeId");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_userId",
                table: "Ratings",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRecipe_recipeId",
                table: "UserRecipe",
                column: "recipeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoryRecipe");

            migrationBuilder.DropTable(
                name: "IngredientRecipe");

            migrationBuilder.DropTable(
                name: "Ratings");

            migrationBuilder.DropTable(
                name: "UserRecipe");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Ingredients");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "Recipes");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
