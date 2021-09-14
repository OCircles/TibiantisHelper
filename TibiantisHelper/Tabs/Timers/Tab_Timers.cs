using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TibiantisHelper.Properties;

namespace TibiantisHelper.Tabs
{
    public partial class Tab_Timers : UserControl
    {

        public static List<Control_Timer> Timers;

        public Tab_Timers()
        {
            Timers = new List<Control_Timer>();

            InitializeComponent();

            if (!string.IsNullOrEmpty(Settings.Default.TimerNotifSoundPath))
                textBox_soundPath.Text = Path.GetFileName(Settings.Default.TimerNotifSoundPath);
        }


        private void TimersPopulate()
        {
            foreach (var timer in Timers)
            {
                splitContainer.Panel2.Controls.Add(timer);
                timer.Dock = DockStyle.Top;
                timer.BackColor = splitContainer.Panel1.BackColor;
            }
        }

        public void TimersAdd(string name, string time, int multiplier, bool autoRestart, bool trayFlash)
        {
            Control_Timer timer = new Control_Timer(name, time, multiplier, autoRestart, trayFlash, Form_Main._traybarContainer);

            Timers.Add(timer);

            TimersPopulate();

            timer.deleteButton.Click += (s, ee) =>
            {
                timer.Stop();
                splitContainer.Panel2.Controls.Remove(timer);
                timer.Dispose();
            };
        }


        private void contextMenuStrip_presets_Opening(object sender, CancelEventArgs e)
        {


            contextMenuStrip_presets.Items.Clear();

            var runes = contextMenuStrip_presets.Items.Add("Runes");
            var runesSingle = (runes as ToolStripMenuItem).DropDownItems.Add("Single");
            var runesBackpack = (runes as ToolStripMenuItem).DropDownItems.Add("Backpack");

            foreach (var rune in DataReader.Runes)
            {

                DataReader.Spell spell = DataReader.Spells.Where(s => s.Name == rune.Name).FirstOrDefault();

                double regenSingle = (spell.Mana / Form_Main._selectedVocation.regenMP) * 60;
                double regenBp = ((spell.Mana * 20) / Form_Main._selectedVocation.regenMP) * 60;

                TimeSpan regenTime = TimeSpan.FromSeconds(regenSingle);

                var regenStringSingle = string.Format("{0:D2}:{1:D2}:{2:D2}", regenTime.Hours, regenTime.Minutes, regenTime.Seconds);

                regenTime = TimeSpan.FromSeconds(regenBp);

                var regenStringBp = string.Format("{0:D2}:{1:D2}:{2:D2}", regenTime.Hours, regenTime.Minutes, regenTime.Seconds);

                (runesSingle as ToolStripMenuItem).DropDownItems.Add(rune.Name, null, (s, ee) =>
                    TimersAdd($"{rune.Name} ({Form_Main._selectedVocation.Name})",
                    regenStringSingle, 1, false, false));

                (runesBackpack as ToolStripMenuItem).DropDownItems.Add(rune.Name, null, (s, ee) =>
                    TimersAdd($"BP of {rune.Name} ({Form_Main._selectedVocation.Name})",
                    regenStringBp, 1, false, false));

            }


            var conjuring = contextMenuStrip_presets.Items.Add("Conjuring");
            var conjuringSingle = (conjuring as ToolStripMenuItem).DropDownItems.Add("Stack");
            var conjuringBackpack = (conjuring as ToolStripMenuItem).DropDownItems.Add("Backpack");

            foreach (DataReader.Spell s in DataReader.Spells.Where(s => s.ProduceAmount != 0))
            {

                DataReader.Item item = DataReader.Items.Where(i => i.ID == s.ProduceID).FirstOrDefault();

                double castSingle = Math.Ceiling(((double)(1 * 100)) / (double)s.ProduceAmount);
                double castBp = Math.Ceiling(((double)(20 * 100)) / (double)s.ProduceAmount);


                double singleMana = castSingle * s.Mana;
                double regenSingle = (singleMana / Form_Main._selectedVocation.regenMP) * 60;

                double bpMana = castBp * s.Mana;
                double regenBp = (bpMana / Form_Main._selectedVocation.regenMP) * 60;

                TimeSpan regenTime = TimeSpan.FromSeconds(regenSingle);

                var regenStringSingle = string.Format("{0:D2}:{1:D2}:{2:D2}", regenTime.Hours, regenTime.Minutes, regenTime.Seconds);

                regenTime = TimeSpan.FromSeconds(regenBp);

                var regenStringBp = string.Format("{0:D2}:{1:D2}:{2:D2}", regenTime.Hours, regenTime.Minutes, regenTime.Seconds);


                (conjuringSingle as ToolStripMenuItem).DropDownItems.Add(item.Name, null, (ss, ee) =>
                    TimersAdd($"Stack of {item.Name} ({Form_Main._selectedVocation.Name})",
                    regenStringSingle, 1, false, false));

                (conjuringBackpack as ToolStripMenuItem).DropDownItems.Add(item.Name, null, (ss, ee) =>
                    TimersAdd($"BP of {item.Name} ({Form_Main._selectedVocation.Name})",
                    regenStringBp, 1, false, false));


            }

            contextMenuStrip_presets.Items.Add("-");

            contextMenuStrip_presets.Items.Add("Bedmage (01:40:00)", null, (ss, ee) => TimersAdd("Bedmage", "01:40:00", 1, false, true));

            contextMenuStrip_presets.Items.Add("Idle (00:15:00)", null, (ss, ee) => TimersAdd("Idle", "00:15:00", 1, true, false));

            contextMenuStrip_presets.Items.Add("Food (00:20:00)", null, (ss, ee) => TimersAdd("Food", "00:20:00", 1, false, false));
        }

        #region Sidebar
        private void button_addTimer_Click(object sender, EventArgs e)
        {

            Form_TimerDialog dialog = new Form_TimerDialog();

            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                TimersAdd(dialog.TimerName, dialog.Time, dialog.Multiplier, dialog.AutoRestart, dialog.TraybarFlash);
            }


        }
        private void button_presets_MouseClick(object sender, MouseEventArgs e)
        {
            contextMenuStrip_presets.Show((Control)sender, new Point(e.X, e.Y));
        }
        private void button_browseSound_Click(object sender, EventArgs e)
        {
            var diag = new OpenFileDialog();
            diag.RestoreDirectory = true;

            diag.Filter = "WAV files (*.wav)|*.wav";

            if (diag.ShowDialog() == DialogResult.OK)
            {
                textBox_soundPath.Text = Path.GetFileName(diag.FileName);
                Settings.Default.TimerNotifSoundPath = diag.FileName;
                Settings.Default.Save();
            }
        }
        
        
        private void checkBox_trayNotification_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.TimerNotifTray = checkBox_trayNotification.Checked;
            Settings.Default.Save();
        }
        private void checkBox_playSound_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.TimerNotifSound = checkBox_playSound.Checked;
            Settings.Default.Save();
        }
        private void checkBox_trayOpenOnClick_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.NotifTimerOpen = checkBox_trayOpenOnClick.Checked;
            Settings.Default.Save();
        }
        #endregion

    }
}
