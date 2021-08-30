using System;
using System.Windows.Forms;

namespace TibiantisHelper
{
    public partial class Tab_Map : UserControl
    {
        public Control_MinimapViewer MinimapViewer;

        public Tab_Map()
        {
            InitializeComponent();

        }


        int serverStartTime = 553; // Placeholder I guess? This is 09:13 in minutes since 00:00

        private void control_MinimapViewer1_SectorSelected(object sender, SectorSelectedEventArgs e)
        {
            textBox1.Text = $"Assuming server start at 09:13, sector {e.Sector} refreshes at the following times:{Environment.NewLine}{Environment.NewLine}";

            int refresh = serverStartTime + (int)Math.Floor(((double)e.Sector - 1) / 8);
            DateTime time = new DateTime();
            time = time.AddMinutes(refresh);

            while (true)
            {
                if (time.Day > 1 && time.Hour > 8)
                    break;

                textBox1.Text += "\t" + time.Hour.ToString("D2") + ":" + time.Minute.ToString("D2") + Environment.NewLine;
                time = time.AddMinutes((Form_Main._miniMap.allSectorSize.Width * Form_Main._miniMap.allSectorSize.Height)/8);

            }

        }

        private void Tab_Map_Load(object sender, System.EventArgs e)
        {
            // The VS designer broke because MinimapViewer references Form_Main minimap so here we are

            MinimapViewer = new Control_MinimapViewer();
            MinimapViewer.Dock = DockStyle.Fill;
            MinimapViewer.SectorSelected += control_MinimapViewer1_SectorSelected;

            splitContainer2.Panel1.Controls.Add(MinimapViewer);

            trackBar1.Value = MinimapViewer.Framerate;
            updateFramerateText();
        }

        private void trackBar1_ValueChanged(object sender, System.EventArgs e)
        {
            MinimapViewer.Framerate = (byte)trackBar1.Value;
            updateFramerateText();
        }

        private void numericUpDown1_ValueChanged(object sender, System.EventArgs e)
        {
            MinimapViewer.SetLayer(decimal.ToInt32(numericUpDown1.Value));
        }

        private void checkBox1_CheckedChanged(object sender, System.EventArgs e)
        {
            MinimapViewer.DragControlEnabled = checkBox1.Checked;
        }

        private void updateFramerateText()
        {
            groupBox1.Text = $"Framerate ({trackBar1.Value} FPS)";
        }
    }
}
