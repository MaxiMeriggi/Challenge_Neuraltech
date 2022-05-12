using AutoMapper;
using ChallengeNeuraltech.DTO.Request;
using ChallengeNeuraltech.DTO.Response;
using ChallengeNeuraltech.Persistence.Model;
using ChallengeNeuraltech.Repository.Images;
using Microsoft.AspNetCore.Mvc;

namespace ChallengeNeuraltech.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImagesRepository _repository;
        private readonly IMapper _mapper;

        public ImagesController(IImagesRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<ActionResult> GetAllImagesAsync()
        {
            try
            {
                var image = await _repository.GetAllImages();

                var response = _mapper.Map<List<ImageResponse>>(image);

                return StatusCode(StatusCodes.Status200OK, response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpGet("{imageId}")]
        public async Task<ActionResult> GetImagesByIdAsync(int imageId)
        {
            try
            {
                var image = await _repository.GetImageById(imageId);

                if (image is null) return StatusCode(StatusCodes.Status404NotFound);

                var response = _mapper.Map<ImageResponse>(image);

                return StatusCode(StatusCodes.Status200OK, response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }


        [HttpPost]
        public async Task<ActionResult> AddImageAsync(ImageRequest imageRequest)
        {
            try
            {
                var imageToAdd = _mapper.Map<ImageModel>(imageRequest);

                await _repository.AddImage(imageToAdd);

                var response = _mapper.Map<ImageResponse>(imageToAdd);

                return StatusCode(StatusCodes.Status201Created, response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpDelete("{imageId}")]
        public async Task<ActionResult> DeleteImageAsync(int imageId)
        {
            try
            {
                var imageToDelete = await _repository.GetImageById(imageId);

                if (imageToDelete is null) return NotFound();

                await _repository.RemoveImage(imageId);

                return StatusCode(StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpPut]
        public async Task<ActionResult> UpdateImageAsync(ImageUpdateRequest imageUpdate)
        {
            try
            {
                var existingImage = await _repository.GetImageById(imageUpdate.Id);

                if (existingImage is null) return StatusCode(StatusCodes.Status404NotFound);

                var imageToUpdate = _mapper.Map<ImageModel>(imageUpdate);

                await _repository.UpdateImage(imageToUpdate);

                var response = _mapper.Map<ImageResponse>(imageToUpdate);

                return StatusCode(StatusCodes.Status201Created, response);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed at executing the request", ex);
            }
        }

        [HttpPost("{imageId}, {movieId}")]
        public async Task<ActionResult> AssignToMovie(int imageId, int movieId)
        {
            try
            {
                var imageToAssign = await _repository.GetImageById(imageId);

                if (imageToAssign is null) return StatusCode(StatusCodes.Status404NotFound);

                await _repository.AssignImage(imageId, movieId);

                var response = _mapper.Map<ImageResponse>(imageToAssign);

                return StatusCode(StatusCodes.Status200OK, response);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed at executing the request", ex);
            }
        }
    }
}
