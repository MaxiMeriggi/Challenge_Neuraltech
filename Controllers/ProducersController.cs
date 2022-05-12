using AutoMapper;
using ChallengeNeuraltech.DTO.Request;
using ChallengeNeuraltech.DTO.Response;
using ChallengeNeuraltech.Persistence.Model;
using ChallengeNeuraltech.Repository.Producers;
using Microsoft.AspNetCore.Mvc;

namespace ChallengeNeuraltech.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProducersController : ControllerBase
    {
        private readonly IProducersRepository _repository;
        private readonly IMapper _mapper;

        public ProducersController(IProducersRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<ActionResult> GetAllProducersAsync()
        {
            try
            {
                var producer = await _repository.GetAllProducers();

                var response = _mapper.Map<List<ProducerResponse>>(producer);

                return StatusCode(StatusCodes.Status200OK, response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpGet("{producerId}")]
        public async Task<ActionResult> GetProducersByIdAsync(int producerId)
        {
            try
            {
                var producer = await _repository.GetProducerById(producerId);

                if (producer is null) return StatusCode(StatusCodes.Status404NotFound);

                var response = _mapper.Map<ProducerResponse>(producer);

                return StatusCode(StatusCodes.Status200OK, response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }


        [HttpPost]
        public async Task<ActionResult> AddProducerAsync(ProducerRequest producerRequest)
        {
            try
            {
                var producerToAdd = _mapper.Map<ProducerModel>(producerRequest);

                await _repository.AddProducer(producerToAdd);

                var response = _mapper.Map<ProducerResponse>(producerToAdd);

                return StatusCode(StatusCodes.Status201Created, response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpDelete("{producerId}")]
        public async Task<ActionResult> DeleteProducerAsync(int producerId)
        {
            try
            {
                var producerToDelete = await _repository.GetProducerById(producerId);

                if (producerToDelete is null) return NotFound();

                await _repository.RemoveProducer(producerId);

                return StatusCode(StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpPut]
        public async Task<ActionResult> UpdateProducerAsync(ProducerUpdateRequest producerUpdate)
        {
            try
            {
                var existingProducer = await _repository.GetProducerById(producerUpdate.Id);

                if (existingProducer is null) return StatusCode(StatusCodes.Status404NotFound);

                var producerToUpdate = _mapper.Map<ProducerModel>(producerUpdate);

                await _repository.UpdateProducer(producerToUpdate);

                var response = _mapper.Map<ProducerResponse>(producerToUpdate);

                return StatusCode(StatusCodes.Status201Created, response);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed at executing the request", ex);
            }
        }

        [HttpPost("{producerId}, {movieId}")]
        public async Task<ActionResult> AssignToMovie(int producerId, int movieId)
        {
            try
            {
                var producerToAssign = await _repository.GetProducerById(producerId);

                if (producerToAssign is null) return StatusCode(StatusCodes.Status404NotFound);

                await _repository.AssignProducer(producerId, movieId);

                var response = _mapper.Map<ProducerResponse>(producerToAssign);

                return StatusCode(StatusCodes.Status200OK, response);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed at executing the request", ex);
            }
        }
    }
}
