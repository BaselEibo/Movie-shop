using Movie_Shop_.Db_Context;
using Movie_Shop_.Models;

namespace Movie_Shop_.Repositorey
{
    public class MovieServices : IMovieServices
    {
        private readonly Movie_DbContext _db;
        private readonly ILogger<MovieServices> _logger;

        public MovieServices(Movie_DbContext movie_DbContext, ILogger<MovieServices> logger)
        {

            _db = movie_DbContext;
            _logger = logger;
        }

        public bool AddMovieIfNotExists(Movie movie)
        {
            if (_db.Movies.Any(m => m.Title == movie.Title)) // Check for duplicates
            {
                return false;
            }

            _db.Movies.Add(movie);
            _db.SaveChanges();
            return true;
        }

        public bool CreateMovie(Movie movie)
        {
            try
            {

                if (_db.Movies.Any(m => m.Title == movie.Title))
                {
                    _logger.LogWarning($"Attempt to create duplicate movie. Title: {movie.Title}");
                    return false;
                }

                _db.Movies.Add(movie);
                _db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error creating movie. Title: {movie?.Title}, Error: {ex.Message}");
                return false;
            }

        }

        public bool DeleteMovie(int id)
        {
            try
            {
                var movie = _db.Movies.FirstOrDefault(m => m.Id == id);

                if (movie == null)
                {
                    _logger.LogWarning($"Attempted to delete a non-existent movie with Id: {id}.");
                    return false;
                }

                _db.Movies.Remove(movie);
                _db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error deleting movie with Id: {id}. Error: {ex.Message}");
                return false;
            }
        }

        public List<Movie> GetAllMovies()
        {
            return _db.Movies.ToList();
        }

        public Movie GetMovieById(int id)
        {
            try
            {
                var movie = _db.Movies.Find(id);
                if (movie == null)
                {
                    _logger.LogWarning($"No movie found with Id: {id}.");
                    return null;
                }
                return movie;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error finding movie with Id: {id}. Error: {ex.Message}");
                return null;
            }

        }

        public bool UpdateMovie(Movie movie)
        {
            try
            {
                var existingMovie = _db.Movies.FirstOrDefault(m => m.Id == movie.Id);
                if (existingMovie == null)
                {
                    _logger.LogWarning($"No movie found with Id: {movie.Id}.");
                    return false;
                }

                existingMovie.Title = movie.Title;
                existingMovie.Price = movie.Price;
                existingMovie.Director = movie.Director;
                existingMovie.ReleaseYear = movie.ReleaseYear;
                existingMovie.ImageUrl = movie.ImageUrl;

                _db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error updating movie with Id: {movie.Id}. Error: {ex.Message}, StackTrace: {ex.StackTrace}");
                return false;
            }
        }
    }
}
