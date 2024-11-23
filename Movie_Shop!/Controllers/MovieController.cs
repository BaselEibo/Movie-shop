
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Movie_Shop_.Models;
using Movie_Shop_.Repositorey;

namespace Movie_Shop_.Controllers
{
    public class MovieController : Controller
    {
        private readonly OmdbService _omdbService;
        private readonly IMovieServices _MS;
        private readonly ILogger<MovieController> _logger;
       

        public MovieController(IMovieServices Ms, ILogger<MovieController> logger , OmdbService omdbService)
        {
            _MS = Ms;
            _logger = logger;
            _omdbService = omdbService;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult MovieList()
        {
            var MoviesList = _MS.GetAllMovies();
            return View(MoviesList);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Movie movie)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    bool isCreated = _MS.CreateMovie(movie);

                    if (isCreated)
                    {
                        TempData["Message"] = "Movie created successfully!";
                        return RedirectToAction("MovieList");
                    }
                    else
                    {
                        TempData["Error"] = "Failed to create the movie. It might already exist.";
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Error creating movie. Title: {movie?.Title}, Error: {ex.Message}");
                    TempData["Error"] = "An unexpected error occurred. Please try again later.";
                }
            }
            return View(movie);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            try
            {
                bool isDeleted = _MS.DeleteMovie(id);

                if (isDeleted)
                {
                    TempData["Message"] = "Movie deleted successfully!";
                }
                else
                {
                    TempData["Error"] = "Failed to delete the movie. It might already be deleted or does not exist.";
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unexpected error deleting movie with Id: {id}. Error: {ex.Message}");
                TempData["Error"] = "An unexpected error occurred. Please try again later.";
            }
            return RedirectToAction("MovieList");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var movie = _MS.GetMovieById(id);
            if (movie == null)
            {
                TempData["Error"] = "The movie you're looking for does not exist.";
                return RedirectToAction("MovieList");
            }
            return View(movie);
        }

        [HttpPost]
        public IActionResult Edit(Movie movie)
        {
            if (ModelState.IsValid)
            {
                bool isEdited = _MS.UpdateMovie(movie);
                if (isEdited)
                {
                    TempData["Message"] = "Movie edited successfully!";
                    return RedirectToAction("MovieList");
                }
                else
                {
                    TempData["Error"] = "Failed to edit the movie. It might already be deleted or does not exist.";
                }
            }
            return View(movie);
        }

    }
}


        

   




