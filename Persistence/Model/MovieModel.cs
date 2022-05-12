using ChallengeNeuraltech.Persistence.Common;
using System.ComponentModel.DataAnnotations;

namespace ChallengeNeuraltech.Persistence.Model
{
    public class MovieModel : IBaseModel
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Duration { get; set; }
        public double Rating { get; set; }
        public string BudgetUsd { get; set; }
        public ICollection<ActorModel> Actor { get; set; }
        public ICollection<GenreModel> Genre { get; set; }
        public ICollection<ImageModel> Image { get; set; }
        public ICollection<ProducerModel> Producer { get; set; }
    }
}
