namespace ChallengeNeuraltech.DTO.Request
{
    public class ImageUpdateRequest
    {
        public int Id { get; set; }
        public string Route { get; set; }
        public string Weight { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
    }
}
