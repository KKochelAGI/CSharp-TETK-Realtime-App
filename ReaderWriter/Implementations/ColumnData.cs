using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Xml;

namespace Implementations
{
    public class ColumnData
    {
        public class ColumnInfo
        {
            public string Name;
            public string Mapping;
            public string Unit;
            public string Dimension;
            public int Index;
            public double Multiplier;

            public ColumnInfo()
            {
                Multiplier = 1.0;
            }
        }
        public class EphemerisData
        {
            public string Format;
            public List<ColumnInfo> Columns;

            public EphemerisData()
            {
                Columns = new List<ColumnInfo>();
            }
        }

        public class AttitudeData
        {
            public string Format;
            public List<ColumnInfo> Columns;

            public AttitudeData()
            {
                Columns = new List<ColumnInfo>();
            }
        }

        public class GraphData
        {
            public string Name;
            public ColumnInfo Column;
        }

        public class DataDisplayData
        {
            public string Name;
            public ColumnInfo Column;
        }

        public class Metric
        {
            public string Name;
            public ColumnInfo Column;
        }

        private EphemerisData _ephemerisData;
        private AttitudeData _attitudeData;
        private List<GraphData> _graphData;
        private List<DataDisplayData> _dataDisplayData;
        private List<Metric> _metricData;
        public ColumnData()
        {
            _ephemerisData = new EphemerisData();
            _attitudeData = new AttitudeData();
        }
        public void LoadColumnData(string file)
        {
            using (StreamReader sr = new StreamReader(file))
            {
                bool ephem = false;
                bool att = false;
                string name = null;
                ColumnInfo column = null;
                GraphData graph = null;
                DataDisplayData display = null;
                Metric metric = null;
                XmlReader reader = XmlReader.Create(sr);
                while (reader.Read())
                {
                    switch (reader.NodeType)
                    {
                        case XmlNodeType.Element:
                            name = reader.Name;
                            if (name == "Column")
                                column = new ColumnInfo();
                            if (name == "EphemerisData")
                                ephem = true;
                            if (name == "AttitudeData")
                                att = true;
                            if (name == "GraphCollection")
                                _graphData = new List<GraphData>();
                            if (name == "DataDisplayCollection")
                                _dataDisplayData = new List<DataDisplayData>();
                            if (name == "MetricCollection")
                                _metricData = new List<Metric>();
                            if (name == "Graph")
                                graph = new GraphData();
                            if (name == "DataDisplay")
                                display = new DataDisplayData();
                            if (name == "Metric")
                                metric = new Metric();
                            break;
                        case XmlNodeType.Text:
                            if (ephem && name == "Format")
                                _ephemerisData.Format = reader.Value;
                            if (att && name == "Format")
                                _attitudeData.Format = reader.Value;
                            if (name == "GraphName" && graph != null)
                                graph.Name = reader.Value;
                            if (name == "DisplayName" && display != null)
                                display.Name = reader.Value;
                            if (name == "MetricName" && metric != null)
                                metric.Name = reader.Value;
                            if (name == "Name" && column != null)
                                column.Name = reader.Value;
                            if (name == "Mapping" && column != null)
                                column.Mapping = reader.Value;
                            if (name == "Unit" && column != null)
                                column.Unit = reader.Value;
                            if (name == "Dimension" && column != null)
                                column.Dimension = reader.Value;
                            if (name == "Index" && column != null)
                                int.TryParse(reader.Value, out column.Index);
                            if (name == "Multiplier" && column != null)
                                double.TryParse(reader.Value, out column.Multiplier);
                            break;
                        case XmlNodeType.EndElement:
                            if (reader.Name == "Column" && column != null)
                            {
                                if (ephem)
                                    _ephemerisData.Columns.Add(column);
                                if (att)
                                    _attitudeData.Columns.Add(column);
                                if (graph != null)
                                    graph.Column = column;
                                if (display != null)
                                    display.Column = column;
                                if (metric != null)
                                    metric.Column = column;
                                column = null;
                            }

                            if (reader.Name == "EphemerisData")
                                ephem = false;
                            if (reader.Name == "AttitudeData")
                                att = false;
                            if (reader.Name == "Graph")
                            {
                                _graphData.Add(graph);
                                graph = null;
                            }
                            if (reader.Name == "DataDisplay")
                            {
                                _dataDisplayData.Add(display);
                                display = null;
                            }

                            if (reader.Name == "Metric")
                            {
                                _metricData.Add(metric);
                                metric = null;
                            }
                            break;
                    }
                }
            }
        }

        public void UpdateEmpheris(ref int timeNdx, ref int xNdx, ref double xMult, ref int yNdx, ref double yMult,
            ref int zNdx, ref double zMult)
        {
            if (_ephemerisData != null)
            {
                ColumnInfo col = _ephemerisData.Columns.FirstOrDefault(f => f.Mapping == "PositionTime");
                if (col != null)
                    timeNdx = col.Index;
                col = _ephemerisData.Columns.FirstOrDefault(f => f.Mapping == "X");
                if (col != null)
                {
                    xNdx = col.Index;
                    xMult = col.Multiplier;
                }

                col = _ephemerisData.Columns.FirstOrDefault(f => f.Mapping == "Y");
                if (col != null)
                {
                    yNdx = col.Index;
                    yMult = col.Multiplier;
                }

                col = _ephemerisData.Columns.FirstOrDefault(f => f.Mapping == "Z");
                if (col != null)
                {
                    zNdx = col.Index;
                    zMult = col.Multiplier;
                }
            }
        }

        public void UpdateAttitude(ref int yawNdx, ref double yawMult, ref int pitchNdx, ref double pitchMult,
            ref int rollNdx, ref double rollMult)
        {
            if (_attitudeData != null)
            {
                ColumnInfo col = _attitudeData.Columns.FirstOrDefault(f => f.Mapping == "Yaw");
                if (col != null)
                {
                    yawNdx = col.Index;
                    yawMult = col.Multiplier;
                }

                col = _attitudeData.Columns.FirstOrDefault(f => f.Mapping == "Pitch");
                if (col != null)
                {
                    pitchNdx = col.Index;
                    pitchMult = col.Multiplier;
                }

                col = _attitudeData.Columns.FirstOrDefault(f => f.Mapping == "Roll");
                if (col != null)
                {
                    rollNdx = col.Index;
                    rollMult = col.Multiplier;
                }
            }
        }

        public List<string> GetColumnDefinitions()
        {
            // The External Data needs the name of the column and the data dimension
            // These are done in the same order that GetDisplayValues uses
            List<string> defs = new List<string>();
            List<string> names = new List<string>();
            if (_metricData != null)
            {
                foreach (Metric metric in _metricData)
                {
                    if (!names.Contains(metric.Column.Name))
                    {
                        defs.Add(metric.Column.Name + " " + metric.Column.Dimension);
                        names.Add(metric.Column.Name);
                    }
                }
            }

            if (_graphData != null)
            {
                foreach (GraphData graphData in _graphData)
                {
                    if (!names.Contains(graphData.Column.Name))
                    {
                        defs.Add(graphData.Column.Name + " " + graphData.Column.Dimension);
                        names.Add(graphData.Column.Name);
                    }
                }
            }

            if (_dataDisplayData != null)
            {
                foreach (DataDisplayData displayData in _dataDisplayData)
                {
                    if (!names.Contains(displayData.Column.Name))
                    {
                        defs.Add(displayData.Column.Name + " " + displayData.Column.Dimension);
                        names.Add(displayData.Column.Name);
                    }
                }
            }

            return defs;
        }

        public Dictionary<string, string> GetGraphNames()
        {
            // the Graph command needs the name of the graph and name of the data column
            Dictionary<string, string> names = new Dictionary<string, string>();
            if (_graphData != null)
            {
                foreach (GraphData graphData in _graphData)
                {
                    names[graphData.Name] = graphData.Column.Name;
                }
            }

            return names;
        }

        public Dictionary<string, List<string>> GetDataDisplayNames()
        {
            // the DataDisplay command needs the name of the data display, name of the data column and unit.
            Dictionary<string, List<string>> names = new Dictionary<string, List<string>>();
            if (_dataDisplayData != null)
            {
                foreach (DataDisplayData data in _dataDisplayData)
                {
                    List<string> vals = new List<string>();
                    vals.Add(data.Column.Name);
                    vals.Add(data.Column.Unit);
                    names[data.Name] = vals;
                }
            }

            return names;
        }

        public string GetDisplayValues(string[] values)
        {
            // The External Data is expecting a value, so even if the TryParse fails
            // still pass a zero 
            // These are done in the same order that the GetColumnDefinitions add the names.
            StringBuilder str = new StringBuilder();
            List<string> names = new List<string>();
            if (_metricData != null)
            {
                foreach (Metric metric in _metricData)
                {
                    if (!names.Contains(metric.Column.Name))
                    {
                        double val = 0;
                        double.TryParse(values[metric.Column.Index], out val);
                        val *= metric.Column.Multiplier;
                        str.Append(" " + val);
                    }
                }
            }
            if (_graphData != null)
            {
                foreach (GraphData graphData in _graphData)
                {
                    if (!names.Contains(graphData.Column.Name))
                    {
                        double val = 0;
                        double.TryParse(values[graphData.Column.Index], out val);
                        val *= graphData.Column.Multiplier;
                        str.Append(" " + val);
                    }
                }
            }

            if (_dataDisplayData != null)
            {
                foreach (DataDisplayData displayData in _dataDisplayData)
                {
                    if (!names.Contains(displayData.Column.Name))
                    {
                        double val = 0;
                        double.TryParse(values[displayData.Column.Index], out val);
                        val *= displayData.Column.Multiplier;
                        str.Append(" " + val);
                    }
                }
            }

            return str.ToString();
        }
    }
}
