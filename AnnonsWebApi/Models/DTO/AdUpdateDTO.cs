namespace AnnonsWebApi.Models.DTO
{
    public class AdUpdateDTO
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? TargetUrl { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
