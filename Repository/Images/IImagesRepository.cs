using ChallengeNeuraltech.Persistence.Model;

namespace ChallengeNeuraltech.Repository.Images
{
    public interface IImagesRepository
    {
        Task<List<ImageModel>> GetAllImages();
        Task<ImageModel?> GetImageById(int imageId);
        Task<ImageModel> AddImage(ImageModel image);
        Task<ImageModel> UpdateImage(ImageModel image);
        Task RemoveImage(int imageId);
        Task AssignImage(int imageId, int movieId);
    }
}
