using CookbookLibrary;
using CookbookLibrary.Repositories;
using CookbookLibrary.RepositoryInterfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace CookbookMVC
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void Configure(IApplicationBuilder app)
        {
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                name: "RecipeCreate",
                pattern: "Recipes/NewRecipe",
                defaults: new { controller = "Recipes", action = "NewRecipe" });
            });
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<CookbookDbContext>(options =>
                options.UseSqlServer(_configuration.GetConnectionString("DefaultConnection")));
            services.AddDbContext<CookbookDbContext>(options => options.UseInMemoryDatabase("CookbookTest"));           
        }
    }
}
