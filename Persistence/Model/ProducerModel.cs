using ChallengeNeuraltech.Persistence.Common;
using System.ComponentModel.DataAnnotations;

namespace ChallengeNeuraltech.Persistence.Model
{
    public class ProducerModel : IBaseModel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime FundationTime { get; set; }
        public ICollection<MovieModel> Movies { get; set; }
    }
}
