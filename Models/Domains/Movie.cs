using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieStore.Models.Domains
{
    public class Movie
    {
        public int Id { get; set; }

        [Required]
        public string? Title { get; set; }
        public string? ReleaseYear { get; set; }
        public string? MovieImage { get; set; }

        [Required]
        public string? Cast { get; set; }
        public string? Director { get; set; }

        [NotMapped]
        public IFormFile? ImageFile { get; set; }

        [NotMapped]
        [Required]
        public List<int>? Genre { get; set; }

        [NotMapped]
        public IEnumerable<SelectListItem>? GenreList { get; set; }

        [NotMapped]
        public int GenreNames { get; set; }

        [NotMapped]
        public MultiSelectList? MultiSelectList { get; set; }
    }
}
