using FireNetCSharp.Controller.Interface;
using SharpPcap;
using SharpPcap.LibPcap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FireNetCSharp.Controller
{
    internal class DeviceService : IDeviceService
    {
        private readonly CaptureDeviceList _deviceList;

        public DeviceService()
        {
            // Initialize the device list from SharpPcap
            _deviceList = CaptureDeviceList.Instance;
        }

        public List<LibPcapLiveDevice> GetAllDeviceInfo()
        {
            var devices = new List<LibPcapLiveDevice>();
            foreach (var dev in _deviceList)
            {
                if (dev is LibPcapLiveDevice liveDev)
                {
                    try
                    {
                        liveDev.Open(); // Required before accessign some fields
                        devices.Add(liveDev);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error loading {liveDev.Name}: {ex.Message}");
                    }
                    finally
                    {
                        if (liveDev.Opened)
                            liveDev.Close(); // Always close when done
                    }
                }
            }


            return devices;
        }
    }
}
