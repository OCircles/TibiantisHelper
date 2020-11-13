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

        public Timer timer;

        public string timerName;

        public int targetTimeBase;
        public int targetTime;
        public int currentTime;
        public int multiplier;

        private string targetTimeString;

        public ToolStripMenuItem deleteButton;


        private NotifyIcon notifier; // Lazy solution lol

        public Control_Timer(string name, string time, int multiplier, NotifyIcon notifier)
        {
            InitializeComponent();

            deleteButton = removeToolStripMenuItem;

            Init(name, time, multiplier, notifier);

        }

        private void Init(string name, string time, int multiplier, NotifyIcon notifier)
        {
            this.Stop();

            timer = this.timer1;
            this.notifier = notifier;
            this.timerName = name;

            this.label_name.Text = timerName;
            this.multiplier = multiplier;

            this.currentTime = 0;

            TimeSpan ts = new TimeSpan(int.Parse(time.Split(':')[0]),
                           int.Parse(time.Split(':')[1]),
                           int.Parse(time.Split(':')[2])
                           );


            this.targetTimeBase = (int)ts.TotalSeconds;
            this.targetTime = this.targetTimeBase;

            if (this.multiplier > 1) this.targetTime *= this.multiplier;

            this.targetTimeString = SecondsToTime(this.targetTime);

            if ( this.multiplier > 1 ) this.targetTimeString += " (" + SecondsToTime(this.targetTimeBase) + " x" + this.multiplier + ")";

            UpdateTime();

            progressBar1.Maximum = targetTime;

            this.Start();
        }

        private void UpdateTime()
        { 
            this.label_time.Text = SecondsToTime(this.currentTime) + " / " + this.targetTimeString;
            this.progressBar1.Value = currentTime;
        }

        public void Start()
        {

            if (this.currentTime == this.targetTime) this.currentTime = 0;

            this.Stop();
            this.timer1.Start();
        }

        public void Stop()
        {
            this.timer1.Stop();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.currentTime++;

            UpdateTime();

            if (this.currentTime >= this.targetTime) this.Finished();
        }

        private void Finished ()
        {

            Stop();

            if ( Settings.Default.TimerNotifTray )
            {
                notifier.BalloonTipTitle = "Tibiantis Timer";
                notifier.BalloonTipText = this.label_name.Text + " has finished";
                notifier.ShowBalloonTip(5000);
            }

            if ( Settings.Default.TimerNotifSound )
            {
                if (!string.IsNullOrEmpty(Settings.Default.TimerNotifSoundPath) && File.Exists(Settings.Default.TimerNotifSoundPath))
                {
                    SoundPlayer player = new SoundPlayer(Settings.Default.TimerNotifSoundPath);
                    player.Play();
                }

                
            }

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
            if ( this.currentTime < this.targetTime )
            {
                DialogResult dialogResult = MessageBox.Show("This timer has not yet finished. Are you sure you want to reset it?", "Reset timer", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dialogResult == DialogResult.No)
                {
                    return;
                } 
            }

            button_play.Text = "▶";
            timer1.Stop();

            this.currentTime = 0;


            UpdateTime();

        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form_TimerDialog dialog = new Form_TimerDialog(true, this.timerName, SecondsToTime(this.targetTimeBase), this.multiplier);

            if (dialog.ShowDialog(this) == DialogResult.OK)
            {

                Init(dialog.name, dialog.time, dialog.multiplier, this.notifier);


            }
        }

        private void button_misc_MouseDown(object sender, MouseEventArgs e)
        {
            contextMenuStrip1.Show((Control)sender, new Point(e.X, e.Y));
        }
    }
}
