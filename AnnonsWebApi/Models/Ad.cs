namespace AnnonsWebApi.Models
{
    public class Ad
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? TargetUrl { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
