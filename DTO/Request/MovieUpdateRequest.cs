namespace ChallengeNeuraltech.DTO.Request
{
    public class MovieUpdateRequest
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Duration { get; set; }
        public double Rating { get; set; }
        public string BudgetUsd { get; set; }
    }
}
