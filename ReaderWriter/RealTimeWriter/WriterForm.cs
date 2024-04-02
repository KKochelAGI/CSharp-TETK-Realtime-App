using System;
using System.IO;
using System.Windows.Forms;
using Implementations;
using Interfaces;
using System.Xml;
using Timer = System.Windows.Forms.Timer;

namespace RealTimeWriter
{
    public partial class WriterForm : Form
    {
        private Timer _timer;
        
        private IDataWriter _dataWriter;
        private const string CsvFilter = "CSV (*.csv)|*.csv|All Files (*.*)|*.*";
        //private const int TimerIntervalMs = 1000; // the speed at which new data is read from the input file

        public WriterForm()
        {
            InitializeComponent();
            LoadIniFile();
        }

        private void DisposeComponents()
        {
            WriteIniFile();
        }

        private void LoadIniFile()
        {
            try
            {
                string dir = Directory.GetCurrentDirectory();
                string fil = Path.Combine(dir, "RealtimeWriter.ini");
                if (File.Exists(fil))
                {
                    using (StreamReader sr = new StreamReader(fil))
                    {
                        string name = null;
                        XmlReader reader = XmlReader.Create(sr);
                        while (reader.Read())
                        {
                            switch (reader.NodeType)
                            {
                                case XmlNodeType.Element:
                                    name = reader.Name;
                                    break;
                                case XmlNodeType.Text:
                                    if (name == "tbInput")
                                        tbInput.Text = reader.Value;
                                    if (name == "tbOutput")
                                        tbOutput.Text = reader.Value;
                                    break;
                                case XmlNodeType.EndElement:
                                    break;
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Reading .ini file problem\n" + e.Message, @"File not open",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void WriteIniFile()
        {
            try
            {
                string dir = Directory.GetCurrentDirectory();
                string fil = Path.Combine(dir, "RealtimeWriter.ini");
                if (File.Exists(fil))
                    File.Delete(fil);
                using (StreamWriter sw = new StreamWriter(fil))
                {
                    sw.WriteLine("<RealtimeWriter>");
                    sw.WriteLine("<tbInput>" + tbInput.Text + "</tbInput>");
                    sw.WriteLine("<tbOutput>" + tbOutput.Text + "</tbOutput>");
                    sw.WriteLine("</RealtimeWriter>");
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Writing .ini file problem\n" + e.Message, @"File not available",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            int TimerIntervalMs; // the speed at which new data is read from the input file

            if (!int.TryParse(tbDataRate.Text, out int dataRate))
            {
                MessageBox.Show(@"Data rate input is not a valid integer.", @"Input Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!int.TryParse(tbDataRefreshSpeed.Text, out int dataRefreshSpeed))
            {
                MessageBox.Show(@"Data refresh speed input is not a valid integer.", @"Input Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!int.TryParse(tbSpeedMultiplier.Text, out int speedMultiplier))
            {
                MessageBox.Show(@"Speed multiplier input is not a valid integer.", @"Input Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }




            // Read 500 lines at a time and append them to the output file
            //  using a named Mutex to synchronize with the reader.
            // It will open and close the file each time
            if (!File.Exists(tbInput.Text))
            {
                MessageBox.Show(@"File not found: " + tbInput.Text, @"File not found",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            TimerIntervalMs = dataRefreshSpeed * 1000 / speedMultiplier; // * 1000 so time is in milliseconds
            _dataWriter = new FileDataWriter(tbInput.Text,tbOutput.Text);
            _dataWriter.WriterFinished += DataWriterOnWriterFinished;
            _timer = new Timer {Interval = TimerIntervalMs};
            _timer.Tick += delegate
            {
                try
                {
                    _dataWriter.WriteData(dataRefreshSpeed, dataRate);
                }
                catch (Exception ex)
                {
                    _timer.Stop();
                    MessageBox.Show(ex.Message, "Exception");
                }
            };
            _timer.Start();
        }

        private void DataWriterOnWriterFinished(object sender, EventArgs e)
        {
            _timer.Stop();
            MessageBox.Show(@"Finished writing!", @"Real-time Writer", MessageBoxButtons.OK);
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            try
            {
                _timer.Stop();
                ((IDisposable)_dataWriter).Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " Writer was likely never started.", "Exception");
            }
        }

        private void btnInput_Click(object sender, EventArgs e)
        {
            using (FileDialog dialog = new OpenFileDialog())
            {
                dialog.Filter = CsvFilter;
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    tbInput.Text = dialog.FileName;
                }
            }
        }

        private void btnOutput_Click(object sender, EventArgs e)
        {
            using (FileDialog dialog = new OpenFileDialog())
            {
                dialog.Filter = CsvFilter;
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    tbOutput.Text = dialog.FileName;
                }
            }
        }

        private void tbInput_Hover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(tbInput, tbInput.Text);
        }

        private void tbOutput_Hover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(tbOutput, tbOutput.Text);
        }

    }
}
