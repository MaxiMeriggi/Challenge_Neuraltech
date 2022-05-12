using ChallengeNeuraltech.Persistence.Model;
using ChallengeNeuraltech.Persistence.MySql;
using ChallengeNeuraltech.Repository.Movies;
using Microsoft.EntityFrameworkCore;

namespace ChallengeNeuraltech.Repository.Images
{
    public class ImagesRepository : IImagesRepository
    {
        private readonly MySqlContext _context;
        private readonly IMoviesRepository _moviesRepository;

        public ImagesRepository(MySqlContext context, IMoviesRepository moviesRepository)
        {
            _context = context;
            _moviesRepository = moviesRepository;
        }

        public async Task<ImageModel> AddImage(ImageModel image)
        {
            try
            {
                _context.Images.Add(image);

                await _context.SaveChangesAsync();

                return image;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to insert the entity", ex);
            }
        }

        public async Task<ImageModel?> GetImageById(int imageId)
        {
            try
            {
                var image = await _context.Images.FirstOrDefaultAsync(image => image.Id == imageId);

                return image;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed at executing the request", ex);
            }


        }

        public async Task<List<ImageModel>> GetAllImages()
        {
            try
            {
                var images = await _context.Images.ToListAsync();
                return images;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed at executing the request", ex);
            }
        }

        public async Task RemoveImage(int imageId)
        {
            try
            {
                var index = await GetImageById(imageId);

                _context.Images.Remove(index);

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed at executing the request", ex);
            }
        }

        public async Task<ImageModel> UpdateImage(ImageModel image)
        {
            try
            {
                var imageToUpdate = await GetImageById(image.Id);

                imageToUpdate.Route = image.Route;
                imageToUpdate.Weight = image.Weight;
                imageToUpdate.Width = image.Width;
                imageToUpdate.Height = image.Height;

                await _context.SaveChangesAsync();

                return imageToUpdate;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed at executing the request", ex);
            }
        }

        public async Task AssignImage(int imageId, int movieId)
        {
            try
            {
                var movieToAssign = await _moviesRepository.GetMovieById(movieId);

                if (movieToAssign is null) throw new Exception("Movie not found.");

                var imageToAssign = await _context.Images.Include(p => p.Movies).FirstOrDefaultAsync(img => img.Id == imageId);

                imageToAssign.Movies.Add(movieToAssign);

                await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                throw new Exception("Failed at executing the request", ex);
            }
        }
    }
}
