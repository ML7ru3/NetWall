namespace FirewallController.Model
{
    public class JobResult
    {
        public string JobId { get; set; } = default!;
        public bool Success { get; set; }
        public string? Message { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}
