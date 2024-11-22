using Movie_Shop_.Models;

namespace Movie_Shop_.Repositorey
{
    public interface IMovieServices
    {
        public List<Movie> GetAllMovies();

        public bool CreateMovie(Movie movie);

        public bool UpdateMovie(Movie movie);

        public bool DeleteMovie(int id);

        public Movie GetMovieById(int id);

        public bool AddMovieIfNotExists(Movie movie);
    }
}