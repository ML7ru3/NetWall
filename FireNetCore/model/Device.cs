using SharpPcap.LibPcap;
using System.Linq;

namespace FireNetCSharp
{
    /// <summary>
    /// device model
    /// </summary>
    public class Device
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string MacAddress { get; set; }
        public string IpAddress { get; set; }
        public string Netmask { get; set; }
        public string Broadcast { get; set; }
        public string LinkType { get; set; }

        public Device(LibPcapLiveDevice liveDev) 
        {
            var firstAddr = liveDev.Addresses.FirstOrDefault(a => a.Addr?.ipAddress != null);

            Name = liveDev.Description;
            Description = liveDev.Name;
            MacAddress = liveDev.MacAddress?.ToString() ?? "N/A";
            IpAddress = firstAddr?.Addr?.ipAddress?.ToString() ?? "N/A";
            Netmask = firstAddr?.Netmask?.ipAddress?.ToString() ?? "N/A";
            Broadcast = firstAddr?.Broadaddr?.ipAddress?.ToString() ?? "N/A";
            LinkType = liveDev.LinkType.ToString() ?? "N/A";
        }
    }
}
