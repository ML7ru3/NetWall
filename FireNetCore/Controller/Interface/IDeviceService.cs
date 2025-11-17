using SharpPcap;
using SharpPcap.LibPcap;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FireNetCSharp.Controller.Interface
{
    public interface IDeviceService
    {
        List<LibPcapLiveDevice> GetAllDeviceInfo();
    }
}
