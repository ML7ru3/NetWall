using FirewallController.Model;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Concurrent;

namespace FirewallController.Controllers
{
    public class FirewallController : ControllerBase
    {
        public record JobRequest(string AgentId, string? Action, Rule[]? Rules, bool? RollbackOnFailure);

        private ConcurrentDictionary<string, ConcurrentQueue<Job>> _jobQueue = new ConcurrentDictionary<string, ConcurrentQueue<Job>>();
        private ConcurrentDictionary<string, JobResult> _jobResults = new ConcurrentDictionary<string, JobResult>();

        /// <summary>
        /// create a new job for an agent   
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost("api/admin/job")]
        public async Task<IResult> CreateJob([FromBody] JobRequest req)
        {
            if (string.IsNullOrWhiteSpace(req.AgentId)) return Results.BadRequest(new { error = "agent_id required" });

            var job = new Job
            {
                JobId = Guid.NewGuid().ToString("D"),
                Action = req.Action ?? "apply_rules",
                Rules = req.Rules ?? Array.Empty<Rule>(),
                RollbackOnFailure = req.RollbackOnFailure ?? true
            };

            var queue = _jobQueue.GetOrAdd(req.AgentId, _ => new ConcurrentQueue<Job>());
            queue.Enqueue(job);
            return Results.Created($"/admin/job/{job.JobId}", new { job_id = job.JobId });
        }

        /// <summary>
        /// Retrieves and dequeues jobs assigned to the specified agent.
        /// </summary>
        /// <remarks>This method checks the job queue for the specified agent and dequeues all available
        /// jobs. The returned jobs are removed from the queue. If the agent does not have an associated queue, an empty
        /// collection is returned.</remarks>
        /// <param name="agent_id">The unique identifier of the agent requesting jobs. This parameter cannot be null, empty, or consist only of
        /// whitespace.</param>
        /// <returns>An <see cref="IResult"/> containing a collection of jobs assigned to the agent. If no jobs are available,
        /// the collection will be empty. If the <paramref name="agent_id"/> is invalid, a bad request response is
        /// returned.</returns>
        [HttpGet("api/agent/poll")]
        public async Task<IResult> PollJob([FromQuery] string agent_id)
        {
            if (string.IsNullOrWhiteSpace(agent_id)) return Results.BadRequest(new { error = "agent_id required" });

            if (!_jobQueue.TryGetValue(agent_id, out var queue))
            {
                return Results.Ok(new { jobs = Array.Empty<Job>() });
            }

            var jobs = new List<Job>();
            while (queue.TryDequeue(out var j)) jobs.Add(j);

            return Results.Ok(new { jobs });
        }

        /// <summary>
        /// post job result from agent 
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        [HttpPost("api/agent/result")]
        public async Task<IResult> SubmitJobResult([FromBody] JobResult result)
        {
            if (result?.JobId == null) return Results.BadRequest(new { error = "job_id required" });
            _jobResults[result.JobId] = result;
            return Results.Ok(new { status = "ok" });
        }

        [HttpGet("api/admin/job/{job_id}")]
        public async Task<IResult> GetJobResult([FromRoute] string jobId)
        {
            if (_jobResults.TryGetValue(jobId, out var res)) return Results.Ok(res);
            return Results.Ok(new { status = "unknown" });
        }
    }
}
