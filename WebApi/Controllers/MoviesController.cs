using Microsoft.AspNetCore.Mvc;
using WebApi.Data;
using WebApi.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MoviesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/<MoviesController>
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            var movies = _context.Movies.ToList();
            return Ok(movies);
        }

        // GET api/<MoviesController>/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            var movieId = _context.Movies.Where(m => m.Id == id);

            if (movieId == null)
            {
                return NotFound();
            }

            return Ok(movieId);
        }

        // POST api/<MoviesController>
        [HttpPost]
        public IActionResult Post([FromBody] Movie movies)
        {
            _context.Movies.Add(movies);
            _context.SaveChanges();
         
            return StatusCode(201, movies);
        }

        // PUT api/<MoviesController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Movie movies)
        {
            var existingMovie = _context.Movies.Find(id);
            if (existingMovie == null)
            {
                return NotFound();
            }

            existingMovie.Title = movies.Title;
            existingMovie.RunningTime = movies.RunningTime;
            existingMovie.Genre = movies.Genre;

            _context.SaveChanges();


            return NoContent();
        }

        // DELETE api/<MoviesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var movie = _context.Movies.Find(id);

            if (movie == null)
            {
                NotFound();
            }

            _context.Movies.Remove(movie);
            _context.SaveChanges();

        }
    }
}
