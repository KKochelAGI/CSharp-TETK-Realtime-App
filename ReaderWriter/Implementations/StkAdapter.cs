
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using AGI.STKObjects;
using AGI.STKUtil;
using AGI.Ui.Application;

namespace Implementations
{
    [SuppressMessage("ReSharper", "StringLiteralTypo")]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class StkAdapter : IDisposable
    {
        private string _cmd;
        private AgUiApplication _uiApp;
        private readonly IAgStkObjectRoot _stkRoot;
        private readonly string _scenarioName;

        public string LastCmd
        {
            get { return _cmd; }
        }
        public StkAdapter(string scenarioName, bool giveUserControl)
        {
            _scenarioName = scenarioName;

            _uiApp = new AgUiApplication();
            _uiApp.LoadPersonality("STK");
            _uiApp.Visible = true;
            if (giveUserControl)
                {_uiApp.UserControl = true;}
            _stkRoot = _uiApp.Personality2 as IAgStkObjectRoot;
            _stkRoot?.LoadScenario(scenarioName);
        }

        public void CreateExternalData(string objectName, ColumnData columnData)
        {
            // this may fail if group not there
            try
            {
                _cmd = "ExternalData */Aircraft/" + objectName + " DeleteGroup \"My Data\"";
                _stkRoot.ExecuteCommand(_cmd);
            }
            catch (Exception)
            {  /* do nothing */ }

            // setup an ExternalData with STK.  This will allow the graph to update more quickly
            // ExternalData can have multiple columns per group and multiple groups
            List<string> columnDefinitions = columnData.GetColumnDefinitions();
            if (columnDefinitions.Any())
            {
                _cmd = "ExternalData */Aircraft/" + objectName + " AddGroup \"My Data\" " + columnDefinitions.Count +
                       " " + string.Join(" ", columnDefinitions);
                _stkRoot.ExecuteCommand(_cmd);
            }

            // Set the interpolation order to zero so that STK only returns the values we give it
            List<object> cmds = new List<object>();
            foreach (string col in columnDefinitions)
            {
                string[] names = col.Split(' ');
                cmds.Add("ExternalData */Aircraft/" + objectName + " SetInterpOrder \"My Data\" " + names[0] + " 0");
            }
            ExecuteMultipleCommands(cmds);

            // Point T&E at the ExternalData
            if (columnDefinitions.Any())
            {
                _cmd = "TE_AnalysisObject * Add Name \"" + objectName +
                       "\" FromSTK timeStep \"0.5\" scalars \"aircraft: User Supplied Data\\My Data\"";
                _stkRoot.ExecuteCommand(_cmd);
            }
        }

        public void CreateDisplays(string objectName, ColumnData columnData)
        {
            // Create the T&E graphs
            Dictionary<string,string> graphs = columnData.GetGraphNames();
            if (graphs.Any())
            {
                foreach (var kvp in graphs)
                {
                    // Suggested user has graph pre-configured with attributes desired
                    // If so, then this create will fail and we'll try a modify just to point to the value
                    try
                    {
                        _cmd = "TE_Graph * Add Name \"" + kvp.Key + "\" AnalysisObject \"" + objectName +
                               "\" GraphXY Segment Color \"Orange\"" + " DataElement \"" + kvp.Value + "\" XIsTime";
                        _stkRoot.ExecuteCommand(_cmd);
                    }
                    catch (Exception)
                    {
                        _cmd = "TE_Graph * Modify Name \"" + kvp.Key + "\" AnalysisObject \"" + objectName +
                               "\" GraphXY Segment \"1\"  DataElement \"" + kvp.Value + "\"";
                        _stkRoot.ExecuteCommand(_cmd);
                    }
                }
            }

            Dictionary<string, List<string>> dataDisplays = columnData.GetDataDisplayNames();

            if (dataDisplays.Any())
            {
                foreach (var kvp in dataDisplays)
                {
                    // Suggested user has data display pre-configured with attributes desired
                    // If so, then this create will fail and we'll try a modify just to point to the value
                    try
                    {
                        _cmd = "TE_DataDisplay * Add Name \"" + kvp.Key + "\" AnalysisObject \"" + objectName +
                               "\" Numeric DataElement \"" + kvp.Value[0] + "," + kvp.Value[0] + "," + kvp.Value[1] + ", ON\"";
                        _stkRoot.ExecuteCommand(_cmd);
                    }
                    catch (Exception)
                    {
                        _cmd = "TE_DataDisplay * Modify Name \"" + kvp.Key + "\" AnalysisObject \"" + objectName +
                               "\" Numeric DataElement \"1," + kvp.Value[0] + "," + kvp.Value[0] + "," + kvp.Value[1] + ", ON\"";
                        _stkRoot.ExecuteCommand(_cmd);
                    }
                }
            }
        }

        public List<string> GetAircraft()
        {
            var collection = _stkRoot.CurrentScenario.Children.GetElements(AgESTKObjectType.eAircraft);

            return (from IAgStkObject obj in collection select obj.InstanceName).ToList();
        }

        public bool ObjectExists(string objectName)
        {
            return _stkRoot.ObjectExists("Aircraft/" + objectName);
        }

        public void LoadNewObject(string objectName)
        {
            // create a new aircraft and create the .e and .a files
            _stkRoot.CurrentScenario.Children.New(AgESTKObjectType.eAircraft, objectName);
        }

        public void LoadMinimumEphemAttFiles(string objectName)
        { 
            var resTime = _stkRoot.ExecuteCommand("GetAnimTime *");
            var eTime = resTime[0].Replace("\"", "");
            var dt = DateTime.Parse(eTime);
            var eTimeIso1 = DateUtility.DateTimeToIsoyd(dt);
            var eTimeIso2 = DateUtility.DateTimeToIsoyd(dt.AddSeconds(1));
            var info = new FileInfo(_scenarioName);

            var eFile = Path.Combine(info.DirectoryName ?? "", objectName + ".e");
            if (File.Exists(eFile))
            {
                File.Delete(eFile);
            }
            
            using (var sw = new StreamWriter(eFile))
            {
                List<string> lines = GetEphemerisHeader(eTime);
                foreach (string line in lines)
                    sw.WriteLine(line);
                sw.WriteLine(eTimeIso1 + "   21.011297 -157.809737   5861.66");
                sw.WriteLine(eTimeIso2 + "   21.012052 -157.809765   5859.52");
                sw.WriteLine("END Ephemeris");
            }
            _cmd = "SetState */Aircraft/" + objectName + " FromFile \"" + eFile + "\"";
            _stkRoot.ExecuteCommand(_cmd);
            var aFile = Path.Combine(info.DirectoryName ?? "", objectName + ".a");
            if (File.Exists(aFile))
            {
                File.Delete(aFile);
            }
            using (var sw = new StreamWriter(aFile))
            {
                List<string> lines = GetAttitudeHeader();
                foreach (string line in lines)
                    sw.WriteLine(line);
                sw.WriteLine(eTimeIso1 + " -0.878906 -2.276917   0.027466");
                sw.WriteLine(eTimeIso2 + " -0.752563 -2.565308   0.043945");
                sw.WriteLine("END Attitude");
            }
            AddAttitude(objectName, aFile);
        }

        public List<string> GetEphemerisHeader(string eTime)
        {
            List<string> lines = new List<string>();
            lines.Add("stk.v.10.0");
            lines.Add("BEGIN Ephemeris");
            lines.Add("NumberOfEphemerisPoints   2");
            lines.Add("InterpolationMethod Lagrange");
            lines.Add("InterpolationOrder   1");
            lines.Add("ScenarioEpoch   " + eTime);
            lines.Add("DistanceUnit Meters");
            lines.Add("CentralBody Earth");
            lines.Add("CoordinateAxes Custom Fixed CentralBody/ Earth");
            lines.Add("TimeFormat ISO-YD");
            lines.Add("EphemerisMSLLLATimePos");
            return lines;
        }

        public List<string> GetAttitudeHeader()
        {
            List<string> lines = new List<string>();
            lines.Add("stk.v.10.0");
            lines.Add("BEGIN Attitude");
            lines.Add("NumberOfAttitudePoints 2");
            lines.Add("InterpolationOrder   1");
            lines.Add("CentralBody Earth");
            lines.Add("CoordinateAxes ICRF");
            lines.Add("Sequence             321");
            lines.Add("TimeFormat ISO-YD");
            lines.Add("AttitudeTimeEulerAngles");
            return lines;
        }
        public void AddAttitude(string objectName, string fileName)
        {
            _cmd = "AddAttitude */Aircraft/" + objectName + " File \"" + fileName + "\"";
            _stkRoot.ExecuteCommand(_cmd);
        }

        public void Refresh(string objectName, ColumnData columnData)
        {
            // only graphs need refreshing
            // we are updating the animation time with each new set of data
            // T&E will automatically update any data display with the new values

            // This is a 2.0.1 T&E command
            //cmd = "TE_Graph * Refresh Name \"Graph1\" AnalysisObject \"Firebird\"";

            // This is a 1.7 T&E command that doesn't change anything but will cause a refresh
            List<object> cmds = new List<object>();
            Dictionary<string, string> graphNames = columnData.GetGraphNames();
            if (graphNames.Any())
            {
                foreach (var kvp in graphNames)
                {
                    cmds.Add("TE_Graph * Modify Name \"" + kvp.Key + "\" AnalysisObject \"" + objectName +
                             "\" Segment \"1\" DataElement \"" + kvp.Value + "\"");
                }

                ExecuteMultipleCommands(cmds);
            }
        }

        public void Save(string directoryName)
        {
            _cmd = "Save / * \"" + directoryName + "\"";
            _stkRoot.ExecuteCommand(_cmd);
        }

        public void SaveAs(string fileName)
        {
            _stkRoot.SaveAs(fileName);
        }

        public void SetStateFromFile(string objectName, string fileName)
        {
            _cmd = "SetState */Aircraft/" + objectName + " FromFile \"" + fileName + "\"";
            _stkRoot.ExecuteCommand(_cmd);
        }

        public string GetScenarioDirectory()
        {
            _cmd = "GetDirectory / Scenario";
            var cmdResult = _stkRoot.ExecuteCommand(_cmd);
            return cmdResult[0];
        }

        public void SetAnimationTime(string epoch)
        {
            _cmd = "SetAnimation * CurrentTime \"" + epoch + "\"";
            _stkRoot.ExecuteCommand(_cmd);
        }

        public string CreateExternalDataCommand(string objectName, ColumnData columnData, DateTime time, string[] values)
        {
            string num = columnData.GetDisplayValues(values);
            if (string.IsNullOrWhiteSpace(num))
                return null;
            return "ExternalData */Aircraft/" + objectName + " AddData \"My Data\" \"" +
                time.ToString("dd MMM yyyy HH:mm:ss.fff") + "\" " + num;
        }

        public void ExecuteMultipleCommands(List<object> commands)
        {
            // It is much faster to send multiple commands at once
            if (commands.Any())
            { _stkRoot.ExecuteMultipleCommands(commands.ToArray(), AgEExecMultiCmdResultAction.eContinueOnError);}
        } 

        #region IDisposable Support
        private bool _disposedValue; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                if (_uiApp != null && !_uiApp.UserControl)
                {
                    if (_stkRoot?.CurrentScenario != null)
                    {
                        _stkRoot.CloseScenario();
                    }

                    _uiApp.Quit();
                    _uiApp = null;
                }

                _disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        ~StkAdapter()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(false);
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
