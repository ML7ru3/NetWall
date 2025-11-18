using FireNetCSharp.Controller;
using FireNetCSharp.Controller.Interface;
using System;
using System.Windows.Forms;

namespace FireNetCSharp
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            IFirewallService firewallService = new FirewallService();
            IDeviceService deviceService = new DeviceService();

            firewallService.Start();

            Application.Run(new Main(deviceService));
        }
    }
}
