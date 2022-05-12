using AutoMapper;
using ChallengeNeuraltech.DTO.Request;
using ChallengeNeuraltech.DTO.Response;
using ChallengeNeuraltech.Persistence.Model;
using ChallengeNeuraltech.Repository.Genres;
using Microsoft.AspNetCore.Mvc;

namespace ChallengeNeuraltech.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly IGenresRepository _repository;
        private readonly IMapper _mapper;

        public GenresController(IGenresRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<ActionResult> GetAllGenresAsync()
        {
            try
            {
                var genre = await _repository.GetAllGenres();

                var response = _mapper.Map<List<GenreResponse>>(genre);

                return StatusCode(StatusCodes.Status200OK, response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpGet("{genreId}")]
        public async Task<ActionResult> GetGenresByIdAsync(int genreId)
        {
            try
            {
                var genre = await _repository.GetGenreById(genreId);

                if (genre is null) return StatusCode(StatusCodes.Status404NotFound);

                var response = _mapper.Map<GenreResponse>(genre);

                return StatusCode(StatusCodes.Status200OK, response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }


        [HttpPost]
        public async Task<ActionResult> AddGenreAsync(GenreRequest genreRequest)
        {
            try
            {
                var genreToAdd = _mapper.Map<GenreModel>(genreRequest);

                await _repository.AddGenre(genreToAdd);

                var response = _mapper.Map<GenreResponse>(genreToAdd);

                return StatusCode(StatusCodes.Status201Created, response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpDelete("{genreId}")]
        public async Task<ActionResult> DeleteGenreAsync(int genreId)
        {
            try
            {
                var genreToDelete = await _repository.GetGenreById(genreId);

                if (genreToDelete is null) return NotFound();

                await _repository.RemoveGenre(genreId);

                return StatusCode(StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpPut]
        public async Task<ActionResult> UpdateGenreAsync(GenreUpdateRequest genreUpdate)
        {
            try
            {
                var existingGenre = await _repository.GetGenreById(genreUpdate.Id);

                if (existingGenre is null) return StatusCode(StatusCodes.Status404NotFound);

                var genreToUpdate = _mapper.Map<GenreModel>(genreUpdate);

                await _repository.UpdateGenre(genreToUpdate);

                var response = _mapper.Map<GenreResponse>(genreToUpdate);

                return StatusCode(StatusCodes.Status201Created, response);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed at executing the request", ex);
            }
        }

        [HttpPost("{genreId}, {movieId}")]
        public async Task<ActionResult> AssignToMovie(int genreId, int movieId)
        {
            try
            {
                var genreToAssign = await _repository.GetGenreById(genreId);

                if (genreToAssign is null) return StatusCode(StatusCodes.Status404NotFound);

                await _repository.AssignGenre(genreId, movieId);

                var response = _mapper.Map<GenreResponse>(await _repository.GetGenreById(genreId));

                return StatusCode(StatusCodes.Status200OK, response);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed at executing the request", ex);
            }
        }
    }
    
}
