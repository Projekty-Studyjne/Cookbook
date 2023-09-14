using CookbookBLL.Interfaces;
using CookbookLibrary.Repositories;
using CookbookLibrary.RepositoryInterfaces;
using CookbookBLL;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ICommentService, CommentService>();
builder.Services.AddScoped<IIngredientService, IngredientService>();
builder.Services.AddScoped<IRatingService, RatingService>();
builder.Services.AddScoped<IRecipeService, RecipeService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IIngredientRecipeService, IngredientRecipeService>();
builder.Services.AddScoped<ICategoryRecipeService, CategoryRecipeService>();
builder.Services.AddScoped<IUserRecipeService, UserRecipeService>();

// Add services to the container.
builder.Services.AddControllersWithViews();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseCors(x => x.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());

app.Run();
