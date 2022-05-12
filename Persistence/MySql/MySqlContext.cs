using ChallengeNeuraltech.Persistence.Model;
using Microsoft.EntityFrameworkCore;

namespace ChallengeNeuraltech.Persistence.MySql
{
    public class MySqlContext : DbContext
    {
        public DbSet<ActorModel> Actors { get; set; }
        public DbSet<GenreModel> Genres { get; set; }
        public DbSet<ImageModel> Images { get; set; }
        public DbSet<MovieModel> Movies { get; set; }
        public DbSet<ProducerModel> Producers { get; set; }
        public MySqlContext(DbContextOptions<MySqlContext> options) : base(options)
        {

        }
        
    }
}
