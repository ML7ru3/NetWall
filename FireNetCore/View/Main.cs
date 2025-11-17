using FireNetCSharp.Controller;
using FireNetCSharp.Controller.Interface;
using FireNetCSharp.Model;
using FireNetCSharp.View;
using SharpPcap;
using SharpPcap.LibPcap;
using System;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace FireNetCSharp
{
    public partial class Main : Form
    {
        private IDeviceService _deviceService;
        private INetworkCaptureService _networkCaptureSerivice;
        private LibPcapLiveDevice _selectedDevice;
        private long numPackets = 0;

        private BindingList<PacketDetail> _packetList = new BindingList<PacketDetail>();
        private readonly ConcurrentQueue<PacketDetail> _packetQueue = new ConcurrentQueue<PacketDetail>();

        public Main(IDeviceService deviceService)
        {
            InitializeComponent();
            _deviceService = deviceService;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadDevices();
            packetCaptureGrid.DataSource = _packetList;
            packetCaptureGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            LoadDevices();
            InitializeGridChart();
        }

        private void LoadDevices()
        {
            try
            {
                var devices = _deviceService.GetAllDeviceInfo();

                cmbDevices.Items.Clear();

                if (devices.Count == 0)
                {
                    MessageBox.Show("No network devices found. Please install Npcap.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                foreach (var dev in devices)
                {
                    cmbDevices.Items.Add(dev.Description); // The name of the device
                }

                cmbDevices.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading devices: {ex.Message}");
            }
        }

        private void CmbDevices_SelectedIndexChanged(object sender, EventArgs e)
        {
            var devices = _deviceService.GetAllDeviceInfo();
            if (cmbDevices.SelectedIndex >= 0)
            {
                _selectedDevice = devices[cmbDevices.SelectedIndex];
            }
            _selectedDevice.Open();
            _networkCaptureSerivice = new NetworkCaptureService(_selectedDevice);
            _networkCaptureSerivice.PacketCaptured += NetworkCaptureService_PacketCaptured;
        }

        /// <summary>
        /// Handles the event when a network packet is captured.
        /// </summary>
        /// <remarks>This method enqueues the captured packet for further processing. It ensures that the
        /// operation is performed on the correct thread if required.</remarks>
        /// <param name="sender">The source of the event, typically the network capture service.</param>
        /// <param name="packet">The details of the captured packet to be processed.</param>
        private void NetworkCaptureService_PacketCaptured(object sender, PacketDetail packet)
        {
            if (!InvokeRequired) return;
            Invoke(new Action(() =>
            {
                _packetQueue.Enqueue(packet);
            }));
        }

        /// <summary>
        /// show all detail properties from selected device
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PropertiesClicked(object sender, EventArgs e)
        {
            if (_selectedDevice == null)
            {
                MessageBox.Show("Please select a device first.", "No Device Selected",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DeviceProperties deviceProperties = new DeviceProperties(new Device(_selectedDevice));
            deviceProperties.Show();
        }

        private void BtnCaptureSelected(object sender, EventArgs e)
        {
            if (_selectedDevice == null)
            {
                MessageBox.Show("Please select a device first.", "No Device Selected",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (startCaptureButton.Text == "Start Capturing")
            {
                _networkCaptureSerivice.StartCapturing();
                startCaptureButton.Text = "Stop Capturing";
                CapturingState(true);

                _updateTimer.Start();
                InitializeGridChart();
            }
            else
            {
                _networkCaptureSerivice.StopCapturing();
                startCaptureButton.Text = "Start Capturing";
                CapturingState(false);

                _updateTimer.Stop();
            }
        }

        /// <summary>
        /// update chart realtime
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateChart(object sender, EventArgs e)
        {
            if (_selectedDevice == null) return;

            while (_packetQueue.TryDequeue(out var packet))
            {
                _packetList.Add(packet);
                numPackets++;
                if (_packetList.Count > 970)
                    _packetList.RemoveAt(0);
            }

            packetCount.Text = $"The number of packets: {numPackets}";

            double downloadSpeed = _networkCaptureSerivice.GetDownloadStatistic();
            double uploadSpeed = _networkCaptureSerivice.GetUploadStatistic();

            // Limit points to last 60
            if (networkChart.Series["Download Speed"].Points.Count > 60)
            {
                networkChart.Series["Download Speed"].Points.RemoveAt(0);
                networkChart.Series["Upload Speed"].Points.RemoveAt(0);
            }

            string time = DateTime.Now.ToString("HH:mm:ss");
            networkChart.Series["Download Speed"].Points.AddXY(time, downloadSpeed);
            networkChart.Series["Upload Speed"].Points.AddXY(time, uploadSpeed);

            // Tooltip
            networkChart.Series["Download Speed"].ToolTip = $"#VALY Mbps at {time}";
            networkChart.Series["Upload Speed"].ToolTip = $"#VALY Mbps at {time}";
        }
        
        /// <summary>
        /// reset numPackets, networkChart and packetGrid when reset or initialize.
        /// </summary>
        private void InitializeGridChart()
        {
            networkChart.Series["Download Speed"].Points.Clear();
            networkChart.Series["Upload Speed"].Points.Clear();
            _packetList.Clear();
            numPackets = 0;
            packetCount.Text = string.Empty;
        }

        private void CapturingState(bool started)
        {
            cmbDevices.Enabled = !started;
            btnRefresh.Enabled = !started; 
        }

        /// <summary>
        /// user UI/UX when hover mouse over graph
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NetworkChart_MouseMove(object sender, MouseEventArgs e)
        {
            var pos = e.Location;
            var result = networkChart.HitTest(pos.X, pos.Y);
            if (result.ChartElementType == ChartElementType.DataPoint)
            {
                networkChart.Cursor = Cursors.Hand;
            }
            else
            {
                networkChart.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// show all application using network
        /// eg: browser, ....
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowDetailNetwork(object sender, EventArgs e)
        {
            
        }
    }
}
