using System;
using System.ComponentModel.DataAnnotations;

namespace MoviesAPI.Models
{
    public class Movie
    {
        [Key]
        public int Id { get; set; }

        public String Title { get; set; }
        public String Description { get; set; }
        public String Director { get; set; }
        public String Genre { get; set; }
        public String ImageLink { get; set; }
        public String Year { get; set; }        
    }
}
