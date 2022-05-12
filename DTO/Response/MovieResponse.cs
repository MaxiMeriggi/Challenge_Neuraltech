namespace ChallengeNeuraltech.DTO.Response
{
    public class MovieResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Duration { get; set; }
        public double Rating { get; set; }
        public string BudgetUsd { get; set; }
        public ICollection<ActorResponse> Actor { get; set; }
        public ICollection<GenreResponse> Genre { get; set; }
        public ICollection<ImageResponse> Image { get; set; }
        public ICollection<ProducerResponse> Producer { get; set; }
    }
}
