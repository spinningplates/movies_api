using System;
using System.ComponentModel.DataAnnotations;

namespace MoviesAPI.Models
{
    public class MovieTitle
    {
        [Key]
        public int Id { get; set; }

        public String Title { get; set; }
        public String Year { get; set; }
    }
}
