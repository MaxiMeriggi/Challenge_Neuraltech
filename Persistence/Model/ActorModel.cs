using ChallengeNeuraltech.Persistence.Common;
using System.ComponentModel.DataAnnotations;

namespace ChallengeNeuraltech.Persistence.Model
{
    public class ActorModel : IBaseModel
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public ICollection<MovieModel> Movies { get; set; }
    }
}
