namespace FireNetCSharp
{
    partial class Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title1 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.Windows.Forms.DataVisualization.Charting.Title title2 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.Windows.Forms.DataVisualization.Charting.Title title3 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.cmbDevices = new System.Windows.Forms.ComboBox();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.networkChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.propertiesButton = new System.Windows.Forms.Button();
            this.packetCaptureGrid = new System.Windows.Forms.DataGridView();
            this.startCaptureButton = new System.Windows.Forms.Button();
            this.firewallTab = new System.Windows.Forms.TabControl();
            this.networkTab = new System.Windows.Forms.TabPage();
            this.packetCount = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this._updateTimer = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.networkChart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.packetCaptureGrid)).BeginInit();
            this.firewallTab.SuspendLayout();
            this.networkTab.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmbDevices
            // 
            this.cmbDevices.FormattingEnabled = true;
            this.cmbDevices.Location = new System.Drawing.Point(478, 23);
            this.cmbDevices.Name = "cmbDevices";
            this.cmbDevices.Size = new System.Drawing.Size(247, 21);
            this.cmbDevices.TabIndex = 0;
            this.cmbDevices.SelectedIndexChanged += new System.EventHandler(this.CmbDevices_SelectedIndexChanged);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(1042, 23);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 23);
            this.btnRefresh.TabIndex = 1;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.BtnRefresh_Click);
            // 
            // networkChart
            // 
            chartArea1.Name = "ChartArea1";
            this.networkChart.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.networkChart.Legends.Add(legend1);
            this.networkChart.Location = new System.Drawing.Point(324, 59);
            this.networkChart.Name = "networkChart";
            this.networkChart.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Bright;
            this.networkChart.RightToLeft = System.Windows.Forms.RightToLeft.No;
            series1.BorderWidth = 3;
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.Legend = "Legend1";
            series1.Name = "Download Speed";
            series2.BorderWidth = 3;
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series2.Legend = "Legend1";
            series2.Name = "Upload Speed";
            this.networkChart.Series.Add(series1);
            this.networkChart.Series.Add(series2);
            this.networkChart.Size = new System.Drawing.Size(812, 527);
            this.networkChart.TabIndex = 3;
            title1.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            title1.Name = "internetSpeed";
            title1.Text = "Internet Speed";
            title2.Alignment = System.Drawing.ContentAlignment.TopLeft;
            title2.Name = "speed";
            title2.Text = "Speed (Mbps)";
            title3.Alignment = System.Drawing.ContentAlignment.BottomRight;
            title3.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Bottom;
            title3.Name = "time";
            title3.Text = "Time (s)";
            this.networkChart.Titles.Add(title1);
            this.networkChart.Titles.Add(title2);
            this.networkChart.Titles.Add(title3);
            this.networkChart.MouseHover += new System.EventHandler(this.ShowDetailNetwork);
            this.networkChart.MouseMove += new System.Windows.Forms.MouseEventHandler(this.NetworkChart_MouseMove);
            // 
            // propertiesButton
            // 
            this.propertiesButton.Location = new System.Drawing.Point(731, 21);
            this.propertiesButton.Name = "propertiesButton";
            this.propertiesButton.Size = new System.Drawing.Size(75, 23);
            this.propertiesButton.TabIndex = 4;
            this.propertiesButton.Text = "Properties";
            this.propertiesButton.UseVisualStyleBackColor = true;
            this.propertiesButton.Click += new System.EventHandler(this.PropertiesClicked);
            // 
            // packetCaptureGrid
            // 
            this.packetCaptureGrid.AllowUserToAddRows = false;
            this.packetCaptureGrid.AllowUserToDeleteRows = false;
            this.packetCaptureGrid.AllowUserToOrderColumns = true;
            this.packetCaptureGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.packetCaptureGrid.Location = new System.Drawing.Point(10, 59);
            this.packetCaptureGrid.Name = "packetCaptureGrid";
            this.packetCaptureGrid.Size = new System.Drawing.Size(308, 527);
            this.packetCaptureGrid.TabIndex = 5;
            // 
            // startCaptureButton
            // 
            this.startCaptureButton.Location = new System.Drawing.Point(356, 21);
            this.startCaptureButton.Name = "startCaptureButton";
            this.startCaptureButton.Size = new System.Drawing.Size(116, 23);
            this.startCaptureButton.TabIndex = 6;
            this.startCaptureButton.Text = "Start Capturing";
            this.startCaptureButton.UseVisualStyleBackColor = true;
            this.startCaptureButton.Click += new System.EventHandler(this.BtnCaptureSelected);
            // 
            // firewallTab
            // 
            this.firewallTab.Controls.Add(this.networkTab);
            this.firewallTab.Controls.Add(this.tabPage2);
            this.firewallTab.Dock = System.Windows.Forms.DockStyle.Fill;
            this.firewallTab.Location = new System.Drawing.Point(0, 0);
            this.firewallTab.Name = "firewallTab";
            this.firewallTab.SelectedIndex = 0;
            this.firewallTab.Size = new System.Drawing.Size(1150, 644);
            this.firewallTab.TabIndex = 7;
            // 
            // networkTab
            // 
            this.networkTab.Controls.Add(this.packetCount);
            this.networkTab.Controls.Add(this.packetCaptureGrid);
            this.networkTab.Controls.Add(this.cmbDevices);
            this.networkTab.Controls.Add(this.networkChart);
            this.networkTab.Controls.Add(this.propertiesButton);
            this.networkTab.Controls.Add(this.btnRefresh);
            this.networkTab.Controls.Add(this.startCaptureButton);
            this.networkTab.Location = new System.Drawing.Point(4, 22);
            this.networkTab.Name = "networkTab";
            this.networkTab.Padding = new System.Windows.Forms.Padding(3);
            this.networkTab.Size = new System.Drawing.Size(1142, 618);
            this.networkTab.TabIndex = 0;
            this.networkTab.Text = "Network";
            this.networkTab.UseVisualStyleBackColor = true;
            // 
            // packetCount
            // 
            this.packetCount.AutoSize = true;
            this.packetCount.Location = new System.Drawing.Point(10, 593);
            this.packetCount.Name = "packetCount";
            this.packetCount.Size = new System.Drawing.Size(0, 13);
            this.packetCount.TabIndex = 7;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1142, 618);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Firewall";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // _updateTimer
            // 
            this._updateTimer.Interval = 1000;
            this._updateTimer.Tick += new System.EventHandler(this.UpdateChart);
            // 
            // Main
            // 
            this.ClientSize = new System.Drawing.Size(1150, 644);
            this.Controls.Add(this.firewallTab);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FireNet";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.networkChart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.packetCaptureGrid)).EndInit();
            this.firewallTab.ResumeLayout(false);
            this.networkTab.ResumeLayout(false);
            this.networkTab.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbDevices;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.DataVisualization.Charting.Chart networkChart;
        private System.Windows.Forms.Button propertiesButton;
        private System.Windows.Forms.DataGridView packetCaptureGrid;
        private System.Windows.Forms.Button startCaptureButton;
        private System.Windows.Forms.TabControl firewallTab;
        private System.Windows.Forms.TabPage networkTab;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Timer _updateTimer;
        private System.Windows.Forms.Label packetCount;
    }
}

