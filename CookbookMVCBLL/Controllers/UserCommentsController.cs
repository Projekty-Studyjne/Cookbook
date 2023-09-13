using Microsoft.AspNetCore.Mvc;

namespace CookbookMVCBLL.Controllers
{
    public class UserCommentsController : Controller
    {
        public IActionResult UserComments()
        {
            return View();
        }
    }
}
