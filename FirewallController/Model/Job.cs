using System.Data;

namespace FirewallController.Model
{
    /// <summary>
    /// Job model 
    /// contains: 
    ///     jobId 
    ///     action 
    ///     rules[] 
    ///     rollbackOnFailure
    /// </summary>
    public class Job
    {
        public string JobId { get; set; } = default!;
        public string Action { get; set; } = "apply_rules";
        public Rule[] Rules { get; set; } = Array.Empty<Rule>();
        public bool RollbackOnFailure { get; set; } = true;
    }
}
