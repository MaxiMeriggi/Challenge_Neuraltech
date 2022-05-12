using ChallengeNeuraltech.Persistence.Model;

namespace ChallengeNeuraltech.Repository.Genres
{
    public interface IGenresRepository
    {
        Task<List<GenreModel>> GetAllGenres();
        Task<GenreModel?> GetGenreById(int genreId);
        Task<GenreModel> AddGenre(GenreModel genre);
        Task<GenreModel> UpdateGenre(GenreModel genre);
        Task RemoveGenre(int genreId);
        Task AssignGenre(int genreId, int movieId);
    }
}
