using AutoMapper;
using ChallengeNeuraltech.DTO.Request;
using ChallengeNeuraltech.DTO.Response;
using ChallengeNeuraltech.Persistence.Model;
using ChallengeNeuraltech.Repository.Movies;
using Microsoft.AspNetCore.Mvc;

namespace ChallengeNeuraltech.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMoviesRepository _repository;
        private readonly IMapper _mapper;

        public MoviesController(IMoviesRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<ActionResult> GetAllMoviesAsync()
        {
            try
            {
                var movie = await _repository.GetAllMovies();

                var response = _mapper.Map<List<MovieResponse>>(movie);

                return StatusCode(StatusCodes.Status200OK, response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpGet("{movieId}")]
        public async Task<ActionResult> GetMoviesByIdAsync(int movieId)
        {
            try
            {
                var movie = await _repository.GetMovieById(movieId);

                if (movie is null) return StatusCode(StatusCodes.Status404NotFound);

                var response = _mapper.Map<MovieResponse>(movie);

                return StatusCode(StatusCodes.Status200OK, response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }


        [HttpPost]
        public async Task<ActionResult> AddMovieAsync(MovieRequest movieRequest)
        {
            try
            {
                var movieToAdd = _mapper.Map<MovieModel>(movieRequest);

                await _repository.AddMovie(movieToAdd);

                var response = _mapper.Map<MovieResponse>(movieToAdd);

                return StatusCode(StatusCodes.Status201Created, response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpDelete("{movieId}")]
        public async Task<ActionResult> DeleteMovieAsync(int movieId)
        {
            try
            {
                var movieToDelete = await _repository.GetMovieById(movieId);

                if (movieToDelete is null) return NotFound();

                await _repository.RemoveMovie(movieId);

                return StatusCode(StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpPut]
        public async Task<ActionResult> UpdateMovieAsync(MovieUpdateRequest movieUpdate)
        {
            try
            {
                var existingMovie = await _repository.GetMovieById(movieUpdate.Id);

                if (existingMovie is null) return StatusCode(StatusCodes.Status404NotFound);

                var movieToUpdate = _mapper.Map<MovieModel>(movieUpdate);

                var response = _mapper.Map<MovieResponse>(await _repository.UpdateMovie(movieToUpdate));

                return StatusCode(StatusCodes.Status201Created, response);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed at executing the request", ex);
            }
        }

    }
}
