using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Movie_Shop_.Models;
using Movie_Shop_.Repositorey;

namespace Movie_Shop_.Controllers
{
    public class MovieController : Controller
    {
        private readonly OmdbService _omdbService;

        public MovieController( OmdbService omdbService)
        {
            
            _omdbService = omdbService;
        }


        [HttpPost]
        public async Task<IActionResult> ImportMovies()
        {
            var movieTitles = new List<string>
                {
                    "The Shawshank Redemption", "The Dark Knight",
                    "Pulp Fiction", "The Godfather",
                    "Schindler's List", "Forrest Gump",
                    "Inception", "Fight Club",
                    "The Lord of the Rings: The Return of the King", "The Matrix",
                    "Goodfellas", "Star Wars: Episode V - The Empire Strikes Back",
                    "The Silence of the Lambs", "Saving Private Ryan",
                    "The Green Mile", "Interstellar",
                    "The Prestige", "The Lion King",
                    "The Departed", "Gladiator",
                    "Back to the Future", "The Lord of the Rings: The Fellowship of the Ring",
                    "The Lord of the Rings: The Two Towers", "Avengers: Endgame",
                    "Avengers: Infinity War", "The Social Network",
                    "Se7en", "Joker",
                    "Django Unchained", "Braveheart",
                    "The Wolf of Wall Street", "The Truman Show",
                    "Jurassic Park", "Titanic",
                    "The Incredibles", "Inside Out",
                    "Toy Story", "Finding Nemo",
                    "The Grand Budapest Hotel", "The Dark Knight Rises",
                    "The Hateful Eight", "Inglourious Basterds",
                    "Whiplash", "A Beautiful Mind",
                    "The Pianist", "Eternal Sunshine of the Spotless Mind",
                    "La La Land", "Mad Max: Fury Road",
                    "Shutter Island", "The Avengers",
                    "Spider-Man: Into the Spider-Verse", "The Big Short",
                    "Bohemian Rhapsody", "Black Panther",
                    "Wonder Woman", "Deadpool",
                    "The Lego Movie", "Logan",
                    "Guardians of the Galaxy", "Doctor Strange",
                    "Iron Man", "Captain America: The Winter Soldier",
                    "Thor: Ragnarok", "Ant-Man",
                    "Star Wars: Episode IV - A New Hope", "Star Wars: Episode VI - Return of the Jedi",
                    "Star Wars: Episode VII - The Force Awakens", "Star Wars: Episode VIII - The Last Jedi",
                    "Star Wars: Episode IX - The Rise of Skywalker", "Rogue One: A Star Wars Story",
                    "Solo: A Star Wars Story", "The Hunger Games",
                    "The Hunger Games: Catching Fire", "The Hunger Games: Mockingjay - Part 1",
                    "The Hunger Games: Mockingjay - Part 2", "The Twilight Saga: Breaking Dawn - Part 1",
                    "The Twilight Saga: Breaking Dawn - Part 2", "Harry Potter and the Sorcerer's Stone",
                    "Harry Potter and the Chamber of Secrets", "Harry Potter and the Prisoner of Azkaban",
                    "Harry Potter and the Goblet of Fire", "Harry Potter and the Order of the Phoenix",
                    "Harry Potter and the Half-Blood Prince", "Harry Potter and the Deathly Hallows: Part 1",
                    "Harry Potter and the Deathly Hallows: Part 2", "Fantastic Beasts and Where to Find Them",
                    "Fantastic Beasts: The Crimes of Grindelwald", "Frozen",
                    "Frozen II", "Moana",
                    "Zootopia", "Ratatouille",
                    "WALL-E", "Up",
                    "Coco", "Monsters, Inc.",
                    "Cars", "A Bug's Life"
                };


            foreach (var title in movieTitles)
            {
                var movie = await _omdbService.GetMovieByTitleAsync(title);
                if (movie != null)
                {
                    var isAdded = _MS.AddMovieIfNotExists(movie);
                    if (!isAdded)
                    {
                        TempData["Error"] = $"Movie '{movie.Title}' already exists and was skipped.";
                    }
                }
            }

            TempData["Message"] = "Movies imported successfully!";
            return RedirectToAction("MovieList");
        }

        public IActionResult Index()
        {
            return View();
        }

    }
}
