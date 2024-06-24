namespace Basket.API.Models
{
    public class APIResponseDTO
    {
        public string? statusMessage { get; set; }
        public string? displayMessage { get; set; }
        public dynamic? responseBody { get; set; }
        public dynamic? supportMessage { get; set; }
    }
}
