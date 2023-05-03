using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using Implementations;
using Interfaces;

namespace RealtimeReader
{
    [SuppressMessage("ReSharper", "IdentifierTypo")]
    public partial class ReaderForm : Form
    {
        private StkAdapter _stkAdapter;
        private IDataReader _dataReader;
        private DataUpdater _dataUpdater;
        private ColumnData _columnData;
        
        private System.Windows.Forms.Timer _timer;
        private List<string> _aircraft;
        private bool _initialReadNeeded;

        private const string ScenarioFilter = "Scenario (*.sc)|*.sc|All Files (*.*)|*.*";
        private const string CsvFilter = "CSV (*.csv)|*.csv|All Files (*.*)|*.*";
        private const string XmlFilter = "XML (*.xml)|*.xml|All Files (*.*)|*.*";
        private const string FileMutexName = "TE_MUTEX";

        public ReaderForm()
        {
            InitializeComponent();

            _timer = new System.Windows.Forms.Timer();
            _initialReadNeeded = true;
            _aircraft = new List<string>();
            LoadIniFile();
        }

        private void DisposeComponents()
        {
            WriteIniFile();
            _stkAdapter?.Dispose();

            _dataUpdater?.Dispose();
            if (_dataReader is IDisposable disposable)
            {
                disposable.Dispose();
            }
        }

        private void LoadIniFile()
        {
            try
            {
                string dir = Directory.GetCurrentDirectory();
                string fil = Path.Combine(dir, "RealtimeReader.ini");
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
                                    if (name == "tbData")
                                        tbData.Text = reader.Value;
                                    if (name == "tbSave")
                                        tbSave.Text = reader.Value;
                                    if (name == "tbColumnData")
                                        tbColumnData.Text = reader.Value;
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
                string fil = Path.Combine(dir, "RealtimeReader.ini");
                if (File.Exists(fil))
                    File.Delete(fil);
                using (StreamWriter sw = new StreamWriter(fil))
                {
                    sw.WriteLine("<RealtimeReader>");
                    sw.WriteLine("<tbInput>" + tbInput.Text + "</tbInput>");
                    sw.WriteLine("<tbData>" + tbData.Text + "</tbData>");
                    sw.WriteLine("<tbSave>" + tbSave.Text + "</tbSave>");
                    sw.WriteLine("<tbColumnData>" + tbColumnData.Text + "</tbColumnData>");
                    sw.WriteLine("</RealtimeReader>");
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Writing .ini file problem\n" + e.Message, @"File not available", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnLoad_Click(object sender, EventArgs e)
        {
            // Startup STK and create a scenario
            try
            {
                _stkAdapter = new StkAdapter(tbInput.Text, false);

                _aircraft = _stkAdapter.GetAircraft();

                _columnData = new ColumnData();
                if (File.Exists(tbColumnData.Text))
                {
                    _columnData.LoadColumnData(tbColumnData.Text);
                }

                if (!_aircraft.Any())
                {
                    rbtnNewObject.Checked = true;
                    rbtnNewObject.Enabled = false;
                    rbtnExisting.Enabled = false;
                }
                else
                {
                    tbObjectName.Text = _aircraft[0];
                }

                btnSave.Enabled = true;
                rbtnExisting.Enabled = true;
                rbtnNewObject.Enabled = true;
                btnStartRead.Enabled = true;
                btnStopRead.Enabled = false;        // start/stop will alternate who is enabled
                btnManualRefresh.Enabled = true;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private int GetInterval()
        {
            var interval =  10000;
            if (int.TryParse(tbUpdate.Text, out var val))
            {
                interval = val * 1000;
            }

            return interval;
        }

        // called with (null, null)
        private void btnStartRead_Click(object sender, EventArgs e)
        {
            if (_dataUpdater == null)
                _dataUpdater = new DataUpdater(tbObjectName.Text, _stkAdapter);

            var interval = GetInterval();
            btnStartRead.Enabled = false;
            if (rdBtnRefresh.Checked)
                { btnStopRead.Enabled = true; }

            // start reading using a named Mutex
            if (_dataReader == null)
                _dataReader = new FileDataReader(tbData.Text, FileMutexName);

            var action = new Action<string[]>(lines =>
            {
                _dataUpdater.AddExternalData(lines, false, interval, _columnData);
                _stkAdapter.CreateDisplays(tbObjectName.Text, _columnData);
            });

            if (_initialReadNeeded)
            {
                if (rbtnNewObject.Checked)
                {
                    if (_stkAdapter.ObjectExists(tbObjectName.Text))
                    {
                        MessageBox.Show(@"aircraft with name " + tbObjectName.Text +
                                        @" already exists - choose a new name and Read again.", @"Name in use",
                                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                        btnStartRead.Enabled = true;
                        btnStopRead.Enabled = false;
                        return;
                    }

                    _stkAdapter.LoadNewObject(tbObjectName.Text);
                }
                rbtnExisting.Enabled = false;
                rbtnNewObject.Enabled = false;
                tbObjectName.Enabled = false;
                btnObject.Enabled = false;
                _stkAdapter.LoadMinimumEphemAttFiles(tbObjectName.Text);
                
                try
                {
                    _stkAdapter.CreateExternalData(tbObjectName.Text, _columnData);
                    _dataReader.ReadData(interval, action);
                    
                }
                catch (Exception exception)
                {
                    var msg = exception.Message;
                    if (msg.StartsWith("Command has failed"))
                        msg += "   " + _stkAdapter.LastCmd;
                    MessageBox.Show(msg, @"Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                // Create a timer to read the data file
                // Use a Mutex to handshake with the data writer
                _timer = new System.Windows.Forms.Timer {Interval = interval};
                _timer.Tick += OnTimerOnTick;
                _initialReadNeeded = false;
            }
            else
            {
                _timer.Interval = GetInterval();
                OnTimerOnTick(null, null);
            }

            if (rdBtnRefresh.Checked)
            {
                _timer.Start();
            }
        }

        // can be call with (null, null)
        private void OnTimerOnTick(object o, EventArgs args)
        {
            try
            {
                // if it ended up waiting for the mutex, there could be events stacked up
                Application.DoEvents();

                var myStop = false;
                if (_timer.Enabled)
                {
                    // if this takes longer than 5 sec we don't want them piling up
                    // so stop timer and restart when we are finished updating T&E
                    _timer.Stop();
                    myStop = true;
                }

                var action = new Action<string[]>(lines =>
                {
                    if (_dataUpdater.AddExternalData(lines, true, _timer.Interval, _columnData))
                    {
                        _stkAdapter.Refresh(tbObjectName.Text, _columnData);
                    }
                });
                _dataReader.ReadData(GetInterval(), action);

                if (myStop) {_timer.Start();}
            }
            catch (AbandonedMutexException)
            {
                _timer.Stop();
                MessageBox.Show(@"Writer process exited releasing the mutex.  Stopping...");
            }
            catch (Exception ex)
            {
                _timer.Stop();
                var msg = ex.Message;
                if (msg.StartsWith("Command has failed"))
                    msg += "   " + _stkAdapter.LastCmd;
                MessageBox.Show(msg, @"Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnStopRead_Click(object sender, EventArgs e)
        {
            btnStopRead.Enabled = false;
            btnStartRead.Enabled = true;
            _timer.Stop();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var info = new FileInfo(tbSave.Text);
            _stkAdapter.Save(info.FullName);
        }

        private void btnScenarioBrowse_Click(object sender, EventArgs e)
        {
            using (FileDialog dialog = new OpenFileDialog())
            {
                dialog.Filter = ScenarioFilter;
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    tbInput.Text = dialog.FileName;
                }
            }
        }

        private void btnInput_Click(object sender, EventArgs e)
        {
            using (FileDialog dialog = new OpenFileDialog())
            {
                dialog.Filter = CsvFilter;
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    tbData.Text = dialog.FileName;
                }
            }
        }

        private void btnBrowseColumnData_Click(object sender, EventArgs e)
        {
            using (FileDialog dialog = new OpenFileDialog())
            {
                dialog.Filter = XmlFilter;
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    tbColumnData.Text = dialog.FileName;
                }
            }
        }

        private void btnBrowseSave_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog dialog = new FolderBrowserDialog())
            {
                dialog.SelectedPath = tbSave.Text;
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    string file = dialog.SelectedPath;
                    tbSave.Text = file;
                }
            }
        }
        private void btnManualRefresh_Click(object sender, EventArgs e)
        {
            rdBtnNoRefresh.Checked = true;
            btnStartRead_Click(null, null);
        }

        private void rdBtnRefresh_CheckedChanged(object sender, EventArgs e)
        {
            if (!(sender is RadioButton rdBtn)) return;
            if (rdBtn.Checked)
            {
                btnStartRead.Enabled = true;
                btnStopRead.Enabled = false;
                tbUpdate.Enabled = true;
            }
            else
            {
                btnStartRead.Enabled = false;
                btnStopRead.Enabled = false;
                tbUpdate.Enabled = false;
                _timer.Enabled = false;     // if we are waiting between reads, then just stop the timer.
            }
        }

        private void rdBtnNoRefresh_CheckedChanged(object sender, EventArgs e)
        {
            if (!(sender is RadioButton rdBtn)) return;
            if (!rdBtn.Checked)
            {
                btnStartRead.Enabled = true;
                btnStopRead.Enabled = false;
                tbUpdate.Enabled = true;
            }
            else
            {
                if (_timer.Enabled)
                    {_timer.Stop();}
                btnStartRead.Enabled = false;
                btnStopRead.Enabled = false;
                tbUpdate.Enabled = false;
            }
        }

        private void rbtnExisting_CheckedChanged(object sender, EventArgs e)
        {
            if (!(sender is RadioButton btn) || !btn.Checked) { return;}
            
            tbObjectName.Enabled = false;
            btnObject.Enabled = true;
        }

        private void rbtnNewObject_CheckedChanged(object sender, EventArgs e)
        {
            if (!(sender is RadioButton btn) || !btn.Checked) {return;}
            
            tbObjectName.Enabled = true;
            btnObject.Enabled = false;
        }

        private void btnObject_Click(object sender, EventArgs e)
        {
            if (!_aircraft.Any())
            {
                MessageBox.Show(@"No aircraft found in scenario", @"Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var selection = new AircraftSelection(_aircraft) {StartPosition = FormStartPosition.CenterParent};
            if (selection.ShowDialog() == DialogResult.OK)
            {
                tbObjectName.Text = selection.SelectedAircraft;
            }
        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            string dir = Directory.GetCurrentDirectory();
            string fil = Path.Combine(dir, "RealtimeReader.txt");
            HelpForm help = new HelpForm();     // no using clause and I want it to stick around
            if (!File.Exists(fil))
            {
                MessageBox.Show(@"File not found: " + fil, @"File not found", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            else
            {
                help.LoadText(fil);
                help.StartPosition = FormStartPosition.CenterParent;
                help.Show();
            }
        }

        private void tbInput_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(tbInput, tbInput.Text);
        }

        private void tbColumnData_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(tbColumnData, tbColumnData.Text);
        }

        private void tbData_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(tbData, tbData.Text);
        }

        private void tbSave_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(tbSave, tbSave.Text);
        }
    }
}
