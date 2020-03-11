using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MoviesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class OmdbController : Controller
    {
        private readonly MovieProcessor movieProcessor;

        public OmdbController()
        {
            movieProcessor = new MovieProcessor();
        }

        [HttpGet]
        public async Task<IActionResult> SearchByTitle([FromBody]string title)
        {
            try
            {
                string searchResults = await movieProcessor.SearchMovies(title);
                return Ok(searchResults);
            }
            catch (Exception ex)
            {
                return new JsonResult(ex.Message);
            }            
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute]int id)
        {
            try
            {
                string omdbMovie = await movieProcessor.GetMovieById(id);
                return Ok(omdbMovie);
            }
            catch (Exception ex)
            {
                return new JsonResult(ex.Message);
            }
        }
    }
}
