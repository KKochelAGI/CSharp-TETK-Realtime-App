using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RealtimeReader
{
    public partial class AircraftSelection : Form
    {
        public string SelectedAircraft;
        public AircraftSelection(List<string> aircraft)
        {
            InitializeComponent();
            foreach (string air in aircraft)
                listBox1.Items.Add(air);
            if (aircraft.Any())
                listBox1.SelectedIndex = 0;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            SelectedAircraft = listBox1.SelectedItem.ToString();
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
