using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoviesAPI.Models
{
    [NotMapped]
    public class ValueSearch
    {
        public string CategoryName { get; set; }
        public string CategoryValue { get; set; }
    }
}
