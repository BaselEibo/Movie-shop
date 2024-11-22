
using Microsoft.AspNetCore.Mvc;

namespace Movie_Shop_.Controllers
{
    public class MovieController : Controller
    {
        public IActionResult Index()
        {

            return View();
        }
    }


}