using Microsoft.AspNetCore.Mvc;
using Movie_Shop_.Models;
using Movie_Shop_.Repositorey;

namespace Movie_Shop_.Controllers
{
    public class MovieController : Controller
    {
        private readonly IMovieServices _MS;
        private readonly ILogger<MovieController> _logger;
       

        public MovieController(IMovieServices Ms, ILogger<MovieController> logger)
        {
            _MS = Ms;
            _logger = logger;
          
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}