using ChallengeNeuraltech.Persistence.Model;

namespace ChallengeNeuraltech.Repository.Movies
{
    public interface IMoviesRepository
    {
        Task<List<MovieModel>> GetAllMovies();
        Task<MovieModel?> GetMovieById(int movieId);
        Task<MovieModel> AddMovie(MovieModel movie);
        Task<MovieModel> UpdateMovie(MovieModel movie);
        Task RemoveMovie(int movieId);
    }
}
