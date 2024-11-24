using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Movie_Shop_.Models;

namespace Movie_Shop_.Repositorey
{
    public class OmdbService
    {
        private readonly HttpClient _httpClient;
        private const string ApiKey = "1d640913"; // Replace with your actual API key
        private const string BaseUrl = "http://www.omdbapi.com/";

        public OmdbService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Movie?> GetMovieByTitleAsync(string title)
        {
            try
            {
                // Build the API URL
                var url = $"{BaseUrl}?apikey={ApiKey}&t={Uri.EscapeDataString(title)}"; 

                // Make an HTTP GET request
                var response = await _httpClient.GetStringAsync(url);

                // Deserialize JSON response into a MovieDto object
                var movieDto = JsonSerializer.Deserialize<MovieDto>(response);

                // Validate and map the MovieDto to Movie model
                if (movieDto != null && !string.IsNullOrEmpty(movieDto.Title))
                {
                    return new Movie
                    {
                        Title = movieDto.Title,
                        Director = movieDto.Director,
                        ReleaseYear = int.TryParse(movieDto.Year, out var year) ? year : 0,
                        Price = 9.99M, // Default price
                        ImageUrl = movieDto.Poster
                    };
                }

                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching movie: {ex.Message}");
                return null;
            }
        }
    }
}
public class MovieDto
{
    public string Title { get; set; }
    public string Year { get; set; }
    public string Director { get; set; }
    public string Poster { get; set; }
}
