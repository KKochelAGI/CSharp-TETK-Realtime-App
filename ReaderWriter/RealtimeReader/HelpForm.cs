using System.IO;
using System.Windows.Forms;

namespace RealtimeReader
{
    public partial class HelpForm : Form
    {
        public HelpForm()
        {
            InitializeComponent();
        }

        public void LoadText(string file)
        {
            if (File.Exists(file))
            {
                using (StreamReader sr = new StreamReader(file))
                {
                    tbHelp.Text = sr.ReadToEnd();
                    tbHelp.Select(1, 0);
                }
            }
        }
    }
}
