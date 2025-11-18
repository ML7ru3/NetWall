using FireNetCSharp.Controller.Interface;
using FireNetCSharp.Model;
using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace FireNetCSharp.Controller
{
    public class FirewallService : IFirewallService
    {

        private HttpRequest _http = new HttpRequest();
        private readonly string _server = Environment.GetEnvironmentVariable("FIREWALL_API_SERVER") ?? "https://localhost:5000";
        private readonly string _agentId = Environment.GetEnvironmentVariable("FIREWALL_AGENT_ID") ?? "agent-01";
        private readonly int _pollIntervalSeconds = int.TryParse(Environment.GetEnvironmentVariable("FIREWALL_POLL_INTERVAL_SECONDS"), out var interval) ? interval : 30;


        /// <summary>
        /// Start curling job on the server every poll interval seconds
        /// </summary>
        /// <returns></returns>
        public async Task Start()
        {
            while (true)
            {
                try
                {
                    var resp = await _http.GetAsync($"{_server}/api/agent/poll?agent_id={_agentId}");

                    if (resp.IsSuccessStatusCode)
                    {
                        var payload = await resp.Content.ReadAsStringAsync();
                        var data = JsonSerializer.Deserialize<PollResponse>(
                            payload,
                            new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
                        );

                        foreach (var job in data.Jobs)
                        {
                            Console.WriteLine($"Received job {job.JobId} action={job.Action}");
                            if (job.Action == "apply_rules")
                            {
                                var (success, message) = await ApplyRulesAtomic(job.Rules ?? Array.Empty<Rule>(), job.RollbackOnFailure);
                                var result = new JobResult { JobId = job.JobId, Success = success, Message = message };
                                
                                var json = JsonSerializer.Serialize(result);
                                var content = new StringContent(json, Encoding.UTF8, "application/json");

                                await _http.PostAsync($"{_server.TrimEnd('/')}/agent/result", content);
                                
                                Console.WriteLine($"Reported job {job.JobId} -> success={success} message={message}");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error in firewall service: {ex.Message}");
                }

                await Task.Delay(_pollIntervalSeconds * 1000);
            }
        }


        /// <summary>
        /// apply rules for windows automatically with rollback support
        /// </summary>
        /// <param name="rules"></param>
        /// <param name="rollbackOnFailure"></param>
        /// <returns></returns>
        private async Task<(bool success, string message)> ApplyRulesAtomic(Rule[] rules, bool rollbackOnFailure)
        {
            var isWindows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);

            if (!isWindows)
                return (false, "Unsupported OS");

            var snapshotPath = Path.Combine(Path.GetTempPath(), $"fw_snapshot_{Guid.NewGuid():N}.rules");

            try
            {
                // Export Windows firewall policy
                var exportArgs = $"advfirewall export \"{snapshotPath}\"";
                var ok = RunProcess("netsh", exportArgs, out var outp, out var err);
                if (!ok) return (false, $"failed to export firewall: {err}");
                

                // apply each rule
                foreach (var r in rules)
                {
                    if (!ValidateRule(r, out var reason)) throw new Exception($"invalid rule: {reason}");
                    bool applied;
                    
                    // windows: use netsh advfirewall or powershell. Use netsh advfirewall firewall add rule ...
                    var args = BuildNetshArgs(r);
                    applied = RunProcess("netsh", args, out var cout, out var cerr);
                    if (!applied) throw new Exception($"netsh failed: {cerr}");
                }

                return (true, "applied");
            }
            catch (Exception ex)
            {
                if (rollbackOnFailure)
                {
                    try
                    {
                        var importArgs = $"advfirewall import \"{snapshotPath}\"";
                        RunProcess("netsh", importArgs, out var o, out var e);
                    }
                    catch (Exception rollEx)
                    {
                        return (false, $"failed and rollback failed: {ex.Message} | rollback error: {rollEx.Message}");
                    }
                }

                return (false, $"failed: {ex.Message}");
            }
            finally
            {
                try { if (File.Exists(snapshotPath)) File.Delete(snapshotPath); } catch { }
            }
        }


        private bool RunProcess(string fileName, string arguments, out string stdout, out string stderr)
        {
            stdout = ""; stderr = "";
            var psi = new ProcessStartInfo
            {
                FileName = fileName,
                Arguments = arguments,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };
            try
            {
                using var p = Process.Start(psi)!;
                stdout = p.StandardOutput.ReadToEnd();
                stderr = p.StandardError.ReadToEnd();
                p.WaitForExit();
                return p.ExitCode == 0;
            }
            catch (Exception ex)
            {
                stderr = ex.Message;
                return false;
            }
        }

        private string BuildNetshArgs(Rule r)
        {
            // Example: advfirewall firewall add rule name="FromAgent-<guid>" dir=in action=allow protocol=TCP localport=22 remoteip=1.2.3.4
            var name = $"FromAgent-{Guid.NewGuid():N}";
            var dir = "in";
            var action = (r.Target?.ToUpperInvariant() == "ACCEPT" || r.Target?.ToUpperInvariant() == "ALLOW") ? "allow" : "block";
            var proto = r.Protocol?.ToUpperInvariant() ?? "ANY";
            var localport = r.Dport.HasValue ? $" localport={r.Dport.Value}" : "";
            var remoteip = !string.IsNullOrWhiteSpace(r.Src) ? $" remoteip={r.Src}" : "";
            var args = $"advfirewall firewall add rule name=\"{name}\" dir={dir} action={action} protocol={proto}{localport}{remoteip}";
            return args;
        }

        private bool ValidateRule(Rule r, out string reason)
        {
            if (string.IsNullOrWhiteSpace(r.Chain)) { reason = "chain empty"; return false; }
            if (string.IsNullOrWhiteSpace(r.Target)) { reason = "target empty"; return false; }
            if (r.Protocol != null && r.Protocol != "tcp" && r.Protocol != "udp" && r.Protocol != "icmp") { reason = "invalid protocol"; return false; }
            // simple validation; extend as needed
            reason = "";
            return true;
        }
    }
}
