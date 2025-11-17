using FireNetCSharp.Controller.Interface;
using FireNetCSharp.Model;
using PacketDotNet;
using SharpPcap;
using SharpPcap.LibPcap;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FireNetCSharp.Controller
{
    public class NetworkCaptureService(LibPcapLiveDevice device) : INetworkCaptureService
    {
        private RawCapture rawPacket;
        private LibPcapLiveDevice _device = device;
        public event EventHandler<PacketDetail> PacketCaptured;

        private long _downloadBytes = 0;
        private long _uploadBytes = 0;

        public Task StartCapturing()
        {
            _downloadBytes = 0;
            _uploadBytes = 0;

            _device.OnPacketArrival += new PacketArrivalEventHandler(OnPacketArrival); // Packet handler
            _device.StartCapture();
            return Task.CompletedTask;
        }

        public Task StopCapturing()
        {
            _device.StopCapture();
            return Task.CompletedTask;  
        }

        public double GetDownloadStatistic()
        {
            double mbps = (_downloadBytes * 8.0) / (1024 * 1024); // bytes → mega bits
            _downloadBytes = 0;
            return Math.Round(mbps, 2);
        }

        public double GetUploadStatistic()
        {
            double mbps = (_uploadBytes * 8.0) / (1024 * 1024); // bytes → mega bits
            _uploadBytes = 0;
            return Math.Round(mbps, 2);
        }

        /// <summary>
        /// Packet handler and mto mapper to PacketDetail
        /// Also update download bytes and upload bytes each packets received
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnPacketArrival(object sender, PacketCapture e)
        {
            try
            {
                rawPacket = e.GetPacket();
                PacketDetail newPacket = new PacketDetail();
                
                newPacket.Time = rawPacket.Timeval.Date;

                var packet = PacketDotNet.Packet.ParsePacket(rawPacket.LinkLayerType, rawPacket.Data);
                var ipPacket = packet.Extract<IPPacket>();

                if (ipPacket != null)
                {
                    newPacket.Source = ipPacket.SourceAddress.ToString();
                    newPacket.Destination = ipPacket.DestinationAddress.ToString();
                    newPacket.Length = ipPacket.TotalLength;
                    newPacket.Protocol = ipPacket.Protocol.ToString();

                    if (newPacket.Source == _device.Addresses.FirstOrDefault().Addr.ipAddress.ToString())
                    {
                        _uploadBytes += newPacket.Length;
                    }
                    else
                    {
                        _downloadBytes += newPacket.Length;
                    }

                    // Event for captured packet to datagrid
                    PacketCaptured?.Invoke(this, newPacket);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing packet: {ex.Message}");
            }
        }
    }
}
