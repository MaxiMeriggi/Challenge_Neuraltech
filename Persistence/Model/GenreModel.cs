using ChallengeNeuraltech.Persistence.Common;
using System.ComponentModel.DataAnnotations;

namespace ChallengeNeuraltech.Persistence.Model
{
    public class GenreModel : IBaseModel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<MovieModel> Movies { get; set; }
    }
}
