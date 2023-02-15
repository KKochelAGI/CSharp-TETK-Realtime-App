using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;

namespace Implementations
{
    [SuppressMessage("ReSharper", "CommentTypo")]
    [SuppressMessage("ReSharper", "StringLiteralTypo")]
    [SuppressMessage("ReSharper", "IdentifierTypo")]
    public class DataUpdater : IDisposable
    {
        private readonly string _objectName;
        private readonly StkAdapter _stkAdapter;
        private bool _headerSkipped;
        private List<string> _ephemerisLines;
        private List<string> _attitudeLines;
        public DataUpdater(string objectName, StkAdapter stkAdapter)
        {
            _objectName = objectName;
            _stkAdapter = stkAdapter;
            _ephemerisLines = new List<string>();
            _attitudeLines = new List<string>();
        }

        public bool AddExternalData(string[] lines, bool updateEphemeris, int timeout, ColumnData columnData)
        {
            // open the data file and read a few columns
            // These are the default values for Firebird
            int timeNdx = 0, xNdx = 1, yNdx = 2, zNdx = 3;
            double xMult = 1.0, yMult = 1.0, zMult = 1000;
            int yawNdx = 4, pitchNdx = 5, rollNdx = 6;
            double yawMult = 1.0, pitchMult = 1.0, rollMult = 1.0;

            // If we found a .xml file use its values instead
            if (columnData != null)
            {
                columnData.UpdateEmpheris(ref timeNdx, ref xNdx, ref xMult, ref yNdx, ref yMult, ref zNdx, ref zMult);
                columnData.UpdateAttitude(ref yawNdx, ref yawMult, ref pitchNdx, ref pitchMult, ref rollNdx, ref rollMult);
            }
            var start = DateTime.Now;
            var lineCnt = 0;
            var ephemeris = new List<string>();
            var attitude = new List<string>();
            //List<string> cov = new List<string>();
            string epoch = null;

            var commands = new List<object>();     // note must use object not string

            foreach (var line in lines)
            {
                if (lineCnt++ < 2 && _headerSkipped == false) // skip the header - line 1 has column names, line 2 has units
                {
                    continue;
                }
                _headerSkipped = true;

                var values = line.Split(',');

                // Example file has time in ISOYD but the default for STK is UTCG
                // (this can be changed and this code could be simplified but .e wants time in ISOYD)
                var time = DateUtility.IsoYDToDateTime(values[timeNdx]);
                if (time == DateTime.MinValue)
                {
                    continue;
                }

                if (string.IsNullOrWhiteSpace(epoch))
                {
                    epoch = time.ToString("dd MMM yyyy HH:mm:ss.fff");
                    _stkAdapter.SetAnimationTime(epoch);
                }

                string cmd = _stkAdapter.CreateExternalDataCommand(_objectName, columnData, time, values);
                if (!string.IsNullOrWhiteSpace(cmd))
                    commands.Add(cmd);
                if (commands.Count > 100)
                {
                    _stkAdapter.ExecuteMultipleCommands(commands);
                    commands.Clear();
                }

                // T&E generates .e file with time in ISOYD
                if (double.TryParse(values[xNdx], out var lat) &&
                    double.TryParse(values[yNdx], out var lon) &&
                    double.TryParse(values[zNdx], out var alt))
                {
                    var lla = values[timeNdx] + " " + lat + " " + lon + " " + alt * 1000;
                    ephemeris.Add(lla);
                }

                if (double.TryParse(values[pitchNdx], out var pitch) &&
                    double.TryParse(values[rollNdx], out var roll) &&
                    double.TryParse(values[yawNdx], out var yaw))
                {
                    var att = values[timeNdx] + " " + yaw + " " + pitch + " " + roll;
                    attitude.Add(att);
                }
            }

            if (commands.Any())
            {
                _stkAdapter.ExecuteMultipleCommands(commands);
            }

            if (ephemeris.Any())
            {
                var path = _stkAdapter.GetScenarioDirectory();

                UpdateEphemeris(updateEphemeris, ephemeris, epoch, path);
                UpdateAttitude(updateEphemeris, attitude, path);
            }

            Trace.WriteLine("Lines: " + lineCnt + " " + (DateTime.Now - start).TotalSeconds + " sec");
            return lineCnt > 1;
        }

        public void Dispose()
        {
        }

        private void UpdateAttitude(bool updateEphemeris, List<string> attitude, string path)
        {
            var aFile = Path.Combine(path, _objectName + ".a");
            var aFileOld = aFile + ".old";
            if (File.Exists(aFile))
            {
                if (File.Exists(aFileOld))
                    File.Delete(aFileOld);
                File.Move(aFile, aFileOld);
                if (!updateEphemeris)
                {
                    _attitudeLines.Clear();
                    _attitudeLines.AddRange(_stkAdapter.GetAttitudeHeader());
                }
                using (var sw = new StreamWriter(aFile))
                {
                    for (int j=0; j<_attitudeLines.Count; ++j)
                    {
                        string line = _attitudeLines[j];
                        if (line != null && line.StartsWith("NumberOfAttitudePoints"))
                        {
                            var attitudePoints = line.Split(new[] {" "}, StringSplitOptions.RemoveEmptyEntries);
                            if (int.TryParse(attitudePoints[1], out var pnts))
                            {
                                if (updateEphemeris)
                                {
                                    pnts += attitude.Count;
                                }
                                else
                                {
                                    pnts = attitude.Count;
                                }

                                line = attitudePoints[0] + " " + pnts;
                                _attitudeLines[j] = line;
                            }
                        }

                        if (string.IsNullOrWhiteSpace(line))
                            continue;
                        sw.WriteLine(line);
                    }
                    // This example file uses yaw pitch roll, so the values from the .csv file 
                    //  can be written directly to the .a file - no conversion necessary
                    if (attitude.Any())
                    {
                        foreach (var a in attitude)
                        {
                            sw.WriteLine(a);
                        }
                    }
                    sw.WriteLine("END Attitude");
                }

                // add any new attitude to our in memory representation of the file
                if (attitude.Any())
                {
                    _attitudeLines.AddRange(attitude);
                }
                attitude.Clear();

                // Tell STK to reload the attitude file - STK connect command
                _stkAdapter.AddAttitude(_objectName, aFile);
            }
            else
            {
                throw new FileNotFoundException("File not found", aFile);
            }

        }

        private void UpdateEphemeris(bool updateEphemeris, List<string> ephemeris, string epoch, string path)
        {
            // Copy the Firebird.e file to Firebird.e.old
            // Create a new Firebird.e file and copy the lines from the .old
            // Make sure to write the new number of LLA lines into the header
            // Append the new LLA lines at the end

            var eFile = Path.Combine(path, _objectName + ".e");
            var eFileOld = eFile + ".old";
            if (File.Exists(eFile))
            {
                if (File.Exists(eFileOld))
                {
                    File.Delete(eFileOld);
                }

                File.Move(eFile, eFileOld);
                if (!updateEphemeris)
                {
                    _ephemerisLines.Clear();
                    _ephemerisLines.AddRange(_stkAdapter.GetEphemerisHeader(epoch));
                }
                using (var sw = new StreamWriter(eFile))
                {
                    for(int j=0; j<_ephemerisLines.Count; ++j)
                    {
                        string line = _ephemerisLines[j];
                        if (line == null)
                        {
                            continue;
                        }

                        if (line.StartsWith("NumberOfEphemerisPoints"))
                        {
                            var ephemPnts = line.Split(new[] {" "}, StringSplitOptions.RemoveEmptyEntries);
                            if (int.TryParse(ephemPnts[1], out var pnts))
                            {
                                if (updateEphemeris)
                                {
                                    pnts += ephemeris.Count;
                                }
                                else
                                {
                                    pnts = ephemeris.Count;
                                }

                                line = ephemPnts[0] + " " + pnts;
                                _ephemerisLines[j] = line;
                            }
                        }

                        if (string.IsNullOrWhiteSpace(line))
                        {
                            continue;
                        }
                        sw.WriteLine(line);
                    }
                    // This example file uses LLA, so the values from the .csv file 
                    //  can be written directly to the .e file - no conversion necessary
                    if (ephemeris.Any())
                    {
                        foreach (var e in ephemeris)
                        {
                            sw.WriteLine(e);
                        }
                    }
                    sw.WriteLine("END Ephemeris");
                }

                // add the new ephemeris to our in memory representation of the file
                if (ephemeris.Any())
                {
                    _ephemerisLines.AddRange(ephemeris);
                }
                ephemeris.Clear();

                // Tell STK to reload the ephemeris file - STK connect command
                _stkAdapter.SetStateFromFile(_objectName, eFile);
            }
            else
            {
                throw new FileNotFoundException("File not found", eFile);
            }

        }
    }
}
