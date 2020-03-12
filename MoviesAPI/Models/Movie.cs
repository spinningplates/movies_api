using System;
using System.ComponentModel.DataAnnotations;

namespace MoviesAPI.Models
{
    public class Movie
    {
        [Key]
        public int Id { get; set; }

        public String Title { get; set; }
        public String Plot { get; set; }
        public String Director { get; set; }
        public String Genre { get; set; }
        public String Poster { get; set; }
        public String Year { get; set; }
        public String imdbId { get; set; }
    }
}
