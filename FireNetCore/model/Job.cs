
namespace FireNetCSharp.Model
{
    public class Job
    {
        public string? JobId { get; set; }
        public string? Action { get; set; }
        public Rule[]? Rules { get; set; }
        public bool RollbackOnFailure { get; set; } = true;
    }
}
