using AutoMapper;
using ChallengeNeuraltech.DTO.Request;
using ChallengeNeuraltech.DTO.Response;
using ChallengeNeuraltech.Persistence.Model;
using ChallengeNeuraltech.Repository.Actors;
using Microsoft.AspNetCore.Mvc;

namespace ChallengeNeuraltech.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActorsController : ControllerBase
    {
        private readonly IActorsRepository _repository;
        private readonly IMapper _mapper;

        public ActorsController(IActorsRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<ActionResult> GetAllActorsAsync()
        {
            try
            {
                var actor = await _repository.GetAllActors();

                var response = _mapper.Map<List<ActorResponse>>(actor);

                return StatusCode(StatusCodes.Status200OK, response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpGet("{actorId}")]
        public async Task<ActionResult> GetActorsByIdAsync(int actorId)
        {
            try
            {
                var actor = await _repository.GetActorById(actorId);
                
                if (actor is null) return StatusCode(StatusCodes.Status404NotFound);
                
                var response = _mapper.Map<ActorResponse>(actor);
                
                return StatusCode(StatusCodes.Status200OK ,response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }


        [HttpPost]
        public async Task<ActionResult> AddActorAsync(ActorRequest actorRequest)
        {
            try
            {
                var actorToAdd = _mapper.Map<ActorModel>(actorRequest);

                await _repository.AddActor(actorToAdd);

                var response = _mapper.Map<ActorResponse>(actorToAdd);

                return StatusCode(StatusCodes.Status201Created, response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpDelete("{actorId}")]
        public async Task<ActionResult> DeleteActorAsync(int actorId)
        {
            try
            {
                var actorToDelete = await _repository.GetActorById(actorId);

                if (actorToDelete is null) return NotFound();

                await _repository.RemoveActor(actorId);

                return StatusCode(StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpPut]
        public async Task<ActionResult> UpdateActorAsync(ActorUpdateRequest actorUpdate)
        {
            try
            {
                var existingActor = await _repository.GetActorById(actorUpdate.Id);

                if (existingActor is null) return StatusCode(StatusCodes.Status404NotFound);

                var actorToUpdate = _mapper.Map<ActorModel>(actorUpdate);

                await _repository.UpdateActor(actorToUpdate);
                
                var response = _mapper.Map<ActorResponse>(actorToUpdate);

                return StatusCode(StatusCodes.Status201Created, response);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed at executing the request", ex);
            }
        }
        [HttpPost("{actorId}, {movieId}")]
        public async Task<ActionResult> AssignToMovie(int actorId, int movieId)
        {
            try
            {
                var actorToAssign = await _repository.GetActorById(actorId);

                if (actorToAssign is null) return StatusCode(StatusCodes.Status404NotFound);

                await _repository.AssignActor(actorId, movieId);

                var response = _mapper.Map<ActorResponse>(await _repository.GetActorById(actorId));

                return StatusCode(StatusCodes.Status200OK, response);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed at executing the request", ex);
            }
        }

    }
}
