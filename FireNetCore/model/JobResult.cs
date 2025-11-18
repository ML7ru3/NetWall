using System;

namespace FireNetCSharp.Model
{
    public class JobResult
    {
        public string? JobId { get; set; }
        public bool Success { get; set; }
        public string? Message { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}
