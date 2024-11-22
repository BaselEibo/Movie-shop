using System.ComponentModel.DataAnnotations;

namespace Movie_Shop_.Models
{
    public class Movie
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Title { get; set; } = string.Empty;

        public string Director { get; set; } = string.Empty;

        [Display(Name = "Release Year")]
        public int ReleaseYear { get; set; }

        [Range(0, (double)decimal.MaxValue, ErrorMessage = "The Price Must be a Positive Value")]
        public decimal Price { get; set; }

        [Url]
        public string ImageUrl { get; set; } = string.Empty;
    }
}
