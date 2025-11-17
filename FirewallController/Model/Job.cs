using System.Data;

namespace FirewallController.Model
{
    public class Job
    {
        public string JobId { get; set; } = default!;
        public string Action { get; set; } = "apply_rules";
        public Rule[] Rules { get; set; } = Array.Empty<Rule>();
        public bool RollbackOnFailure { get; set; } = true;
    }
}
