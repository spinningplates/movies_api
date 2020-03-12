using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using MoviesAPI.Models;
using Newtonsoft.Json.Linq;

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

        [HttpPost]
        public async Task<IActionResult> SearchByTitle([FromBody]Search search)
        {
            try
            {
                JObject searchResults = await movieProcessor.SearchMovies(search.TitleSearch);

                return new JsonResult(searchResults);
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
