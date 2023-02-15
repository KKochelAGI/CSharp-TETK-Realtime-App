using System;
using System.IO;
using System.Windows.Forms;
using Implementations;
using Interfaces;
using Timer = System.Windows.Forms.Timer;

namespace RealTimeWriter
{
    public partial class WriterForm : Form
    {
        private Timer _timer;
        
        private IDataWriter _dataWriter;
        private const string CsvFilter = "CSV (*.csv)|*.csv|All Files (*.*)|*.*";
        private const int TimerIntervalMs = 2000;

        public WriterForm()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            // Read 500 lines at a time and append them to the output file
            //  using a named Mutex to synchronize with the reader.
            // It will open and close the file each time
            if (!File.Exists(tbInput.Text))
            {
                MessageBox.Show(@"File not found: " + tbInput.Text, @"File not found",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _dataWriter = new FileDataWriter(tbInput.Text,tbOutput.Text);
            _dataWriter.WriterFinished += DataWriterOnWriterFinished;
            _timer = new Timer {Interval = TimerIntervalMs};
            _timer.Tick += delegate
            {
                try
                {
                    _dataWriter.WriteData();
                }
                catch (Exception ex)
                {
                    _timer.Stop();
                    MessageBox.Show(ex.Message);
                }
            };
            _timer.Start();
        }

        private void DataWriterOnWriterFinished(object sender, EventArgs e)
        {
            _timer.Stop();
            MessageBox.Show(@"Finished", @"Finished writing", MessageBoxButtons.OK);
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            _timer.Stop();
            ((IDisposable)_dataWriter).Dispose();
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
    }
}
