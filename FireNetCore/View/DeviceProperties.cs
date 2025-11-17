using System;
using System.Windows.Forms;

namespace FireNetCSharp.View
{
    public partial class DeviceProperties : Form
    {

        private readonly Device _device;
        public DeviceProperties(Device device)
        {
            InitializeComponent();
            _device = device;
            nameLabel.Text = _device.Name;
            descriptionLabel.Text = _device.Description;
            IpLabel.Text = _device.IpAddress;
            MacLabel.Text = _device.MacAddress;
            netmaskLabel.Text = _device.Netmask; 
            broadcastLabel.Text = _device.Broadcast;
            linkLabel.Text = _device.LinkType;
        }
    }
}
