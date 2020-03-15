using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MoviesAPI.Data;
using MoviesAPI.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Globalization;

namespace MoviesAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class MoviesController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly MovieProcessor movieProcessor;

        public MoviesController(AppDbContext db)
        {
            _db = db;
            movieProcessor = new MovieProcessor();
        }

        [HttpGet]
        public IActionResult GetMovies()
        {
            try
            {
                return Ok(_db.Movies.ToList());
            }
            catch (Exception ex)
            {
                return new JsonResult(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetMovie([FromRoute] int id)
        {
            try
            {
                var movie = _db.Movies.FirstOrDefault(n => n.Id == id);

                if (movie == null)
                {
                    return NotFound();
                }
                return Ok(movie);
            }
            catch (Exception ex)
            {
                return new JsonResult(ex.Message);
            }
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

        [HttpPost]
        public async Task<IActionResult> AddMovie([FromBody] Movie objMovie)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return new JsonResult("Error while creating new entry.");
                }

                Movie updatedMovieData = await movieProcessor.GetMovieById(objMovie.imdbId);
                _db.Movies.Add(updatedMovieData);
                await _db.SaveChangesAsync();
                return new JsonResult("Movie was added.");
            }
            catch (Exception ex)
            {
                return new JsonResult(ex.Message);
            }
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> UpdateMovie([FromRoute]int id, [FromBody] Movie objMovie)
        {
            try
            {
                if (objMovie == null || id != objMovie.Id)
                {
                    return new JsonResult("Entry was not found.");
                }

                _db.Movies.Update(objMovie);
                await _db.SaveChangesAsync();
                return new JsonResult("Entry was updated.");
            }
            catch (Exception ex)
            {
                return new JsonResult(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult CategorySearch([FromBody] ValueSearch valueSearch)
        {
            try
            {

                string fieldName = valueSearch.CategoryName.ToString();
                TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
                fieldName = textInfo.ToTitleCase(fieldName);
                string lowervalue = valueSearch.CategoryValue.ToLower();
                
                IEnumerable<Movie> result = _db.Movies.AsEnumerable()
                                             .Where(n => n.GetType()
                                                          .GetProperty(fieldName)
                                                          .GetValue(n, null)
                                                          .ToString()
                                                          .ToLower()
                                                          .Contains(lowervalue));

                return Ok(result);
            }
            catch (Exception ex)
            {
                return new JsonResult(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie([FromRoute] int id)
        {
            try
            {
                var findMovie = await _db.Movies.FindAsync(id);

                if (findMovie == null)
                {
                    return NotFound();
                }

                _db.Movies.Remove(findMovie);
                await _db.SaveChangesAsync();

                return new JsonResult("Entry was successfully deleted.");
            }
            catch (Exception ex)
            {
                return new JsonResult(ex.Message);
            }
        }
    }
}
