namespace ASP_API.Model.Public
{
    public class Batch
    {
        public int BatchId { get; set; }
        public string BatchName { get; set; } = string.Empty;
        public string StartedAt { get; set; } = string.Empty;
    }
}
