using System;

namespace FireNetCSharp.Model
{
    /// <summary>
    /// Packet model
    /// </summary>
    public class PacketDetail
    {
        public DateTime Time { get; set; }
        public string Source { get; set; }
        public string Destination { get; set; }
        public string Protocol { get; set; }
        public long Length { get; set; }
        public PacketDetail() { 
        }
    }
}
