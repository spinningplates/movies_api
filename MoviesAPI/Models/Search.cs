using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoviesAPI.Models
{
    [NotMapped]
    public class Search
    {
        public string TitleSearch { get; set; }
    }
}
