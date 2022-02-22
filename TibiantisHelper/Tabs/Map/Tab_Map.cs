using System;
using System.Windows.Forms;

namespace TibiantisHelper
{
    public partial class Tab_Map : UserControl
    {
        public Tab_Map()
        {
            InitializeComponent();

        }


        int serverStartTime = 553; // Placeholder I guess? This is 09:13 in minutes since 00:00

        private void control_MinimapViewer1_SectorSelected(object sender, MinimapSectorSelectedEventArgs e)
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
            if (DesignMode)
                return;
            
            control_MinimapViewer1.SectorSelected += control_MinimapViewer1_SectorSelected;
            control_MinimapViewer1.Redraw += (ss, ee) => OnMinimapRedraw();

            splitContainer2.Panel1.Controls.Add(control_MinimapViewer1);

            trackBar1.Value = control_MinimapViewer1.Framerate;
            updateFramerateText();
        }

        private void OnMinimapRedraw()
        {
            var pos = control_MinimapViewer1.Position;
            label_position.Text = $"X: {pos.X} Y: {pos.Y} Z: {pos.Z}";
        }

        private void trackBar1_ValueChanged(object sender, System.EventArgs e)
        {
            control_MinimapViewer1.Framerate = (byte)trackBar1.Value;
            updateFramerateText();
        }

        private void numericUpDown1_ValueChanged(object sender, System.EventArgs e)
        {
            control_MinimapViewer1.SetLayer(decimal.ToInt32(numericUpDown1.Value));
        }

        private void checkBox1_CheckedChanged(object sender, System.EventArgs e)
        {
            control_MinimapViewer1.DragControlEnabled = checkBox1.Checked;
        }

        private void updateFramerateText()
        {
            groupBox1.Text = $"Framerate ({trackBar1.Value} FPS)";
        }
    }
}
