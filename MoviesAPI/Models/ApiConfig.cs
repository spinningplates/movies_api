using System;
using System.ComponentModel.DataAnnotations;

namespace MoviesAPI.Models
{
    public class ApiConfig
    {
        [Key]
        public int Id { get; set; }

        public string Token { get; set; }
    }
}
