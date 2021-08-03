using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TibiantisHelper.Properties;
using System.Media;
using System.IO;

namespace TibiantisHelper
{
    public partial class Control_Timer : UserControl
    {
        public Timer TimerComponent;
        public string TimerName;
        
        public int TargetTimeBase;
        public int TargetTime;
        public int CurrentTime;
        public int Multiplier;
        public bool AutoRestart;
        public bool TraybarFlash;

        private string targetTimeString;

        public ToolStripMenuItem deleteButton;


        private Form_Main.TraybarContainer container; // Lazy solution lol

        public Control_Timer(string name, string time, int multiplier, bool autoRestart, bool trayFlash, Form_Main.TraybarContainer container)
        {
            InitializeComponent();

            deleteButton = removeToolStripMenuItem;

            Init(name, time, multiplier, autoRestart, trayFlash, container);

        }

        private void Init(string name, string time, int multiplier, bool autoRestart, bool trayFlash, Form_Main.TraybarContainer container)
        {
            this.Stop();

            this.TimerComponent = this.timer1;

            this.container = container;
            this.TimerName = name;

            this.label_name.Text = TimerName;
            this.Multiplier = multiplier;

            this.AutoRestart = autoRestart;
            this.TraybarFlash = trayFlash;

            this.CurrentTime = 0;

            TimeSpan ts = new TimeSpan(int.Parse(time.Split(':')[0]),
                           int.Parse(time.Split(':')[1]),
                           int.Parse(time.Split(':')[2])
                           );


            this.TargetTimeBase = (int)ts.TotalSeconds;
            this.TargetTime = this.TargetTimeBase;

            if (this.Multiplier > 1) this.TargetTime *= this.Multiplier;

            this.targetTimeString = SecondsToTime(this.TargetTime);

            if ( this.Multiplier > 1 ) this.targetTimeString += " (" + SecondsToTime(this.TargetTimeBase) + " x" + this.Multiplier + ")";

            UpdateTime();

            progressBar1.Maximum = TargetTime;

            this.Start();
        }

        private void UpdateTime()
        { 
            this.label_time.Text = SecondsToTime(this.CurrentTime) + " / " + this.targetTimeString;
            this.progressBar1.Value = CurrentTime;
        }

        public void Start()
        {

            if (this.CurrentTime == this.TargetTime) this.CurrentTime = 0;
            UpdateTime();

            this.Stop();
            this.timer1.Start();
        }

        public void Stop()
        {
            this.timer1.Stop();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.CurrentTime++;

            UpdateTime();

            if (this.CurrentTime >= this.TargetTime) this.Finished();
        }

        private void Finished ()
        {

            this.Stop();

            if ( Settings.Default.TimerNotifTray )
            {
                Form_Main.Tray_ShowBubble(Form_Main.TrayBubbleBehaviour.Timer, 5000, "Tibiantis Timer", this.label_name.Text + " has finished", ToolTipIcon.Info);
            }

            if ( Settings.Default.TimerNotifSound )
            {
                if (!string.IsNullOrEmpty(Settings.Default.TimerNotifSoundPath) && File.Exists(Settings.Default.TimerNotifSoundPath))
                {
                    SoundPlayer player = new SoundPlayer(Settings.Default.TimerNotifSoundPath);
                    player.Play();
                }
            }

            if (this.TraybarFlash)
                this.container.flashTimer.Enabled = true;

            if (this.AutoRestart)
                this.Start();

        }


        private string SecondsToTime(int secs) 
        {
            TimeSpan t = TimeSpan.FromSeconds(secs);

            string answer = string.Format("{0:D2}:{1:D2}:{2:D2}",
                            t.Hours + t.Days*24,
                            t.Minutes,
                            t.Seconds);

            return answer;
        }

        private void button_play_Click(object sender, EventArgs e)
        {
            if ( timer1.Enabled )
            {
                this.Stop();
                button_play.Text = "▶";
            } else
            {
                this.Start();
                button_play.Text = "⏸"; 
            }
        }

        private void button_stop_Click(object sender, EventArgs e)
        {
            if ( this.CurrentTime < this.TargetTime )
            {
                DialogResult dialogResult = MessageBox.Show("This timer has not yet finished. Are you sure you want to reset it?", "Reset timer", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dialogResult == DialogResult.No)
                {
                    return;
                } 
            }

            button_play.Text = "▶";
            timer1.Stop();

            this.CurrentTime = 0;

            if (this.TraybarFlash)
            {
                this.TraybarFlash = false;
                bool keepFlashing = false;

                foreach ( var t in Form_Main._timers )
                {
                    if (t.TraybarFlash && t.CurrentTime >= t.TargetTime)
                        keepFlashing = true;
                }

                this.TraybarFlash = true;

                if (!keepFlashing)
                {
                    this.container.flashTimer.Stop();
                    this.container.notifyIcon.Icon = Resources.IconTrayGreen;
                    this.container.flashIsRed = false;
                }

            }


            UpdateTime();

        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form_TimerDialog dialog = new Form_TimerDialog(true, this.TimerName, SecondsToTime(this.TargetTimeBase), this.Multiplier, this.AutoRestart);

            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                Init(dialog.TimerName, dialog.Time, dialog.Multiplier, dialog.AutoRestart, dialog.TraybarFlash, this.container);
            }
        }

        private void button_misc_MouseDown(object sender, MouseEventArgs e)
        {
            contextMenuStrip1.Show((Control)sender, new Point(e.X, e.Y));
        }
    }
}
