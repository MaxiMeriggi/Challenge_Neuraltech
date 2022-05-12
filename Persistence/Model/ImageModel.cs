using ChallengeNeuraltech.Persistence.Common;
using System.ComponentModel.DataAnnotations;

namespace ChallengeNeuraltech.Persistence.Model
{
    public class ImageModel : IBaseModel
    {
        [Key]
        public int Id { get; set; }
        public string Route { get; set; }
        public string Weight { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public ICollection<MovieModel> Movies { get; set; }
    }
}
