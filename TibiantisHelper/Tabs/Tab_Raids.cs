using BrightIdeasSoftware;
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
using System.Xml;

namespace TibiantisHelper.Tabs
{
    public partial class Tab_Raids : UserControl
    {

        const string Filename = "Raids.xml";
        public List<TrackedRaid> TrackedRaids;


        public Tab_Raids()
        {
            TrackedRaids = new List<TrackedRaid>();

            InitializeComponent();

        }

        private void Tab_Raids_Load(object sender, EventArgs e)
        {

            if (DesignMode)
                return;

            control_MinimapViewer1.SetZoom(3.5f);

            ReadXML();

            foreach (var raid in DataReader.Raids)
            {
                var tt = new TimeSpan(0, 0, raid.Interval);

                var timeString = tt.Days + " days";

                // All of the cipsoft raids are in full day intervals, but adding this in case someone adapts this for another server
                if (tt.Hours != 0 || tt.Minutes != 0 || tt.Seconds != 0)
                {
                    var addonString = "";
                    if (tt.Seconds != 0) addonString = tt.Seconds.ToString("D2") + "s";
                    if (tt.Minutes != 0) addonString = tt.Minutes.ToString("D2") + "m" + addonString;
                    if (tt.Hours != 0) addonString = tt.Hours + "h" + addonString;

                    timeString = timeString + " and " + addonString;
                }

                int newIndex = dataGridView1.Rows.Add(CleanRaidName(raid.Filename), timeString);

                dataGridView1.Rows[newIndex].Tag = raid;

                int findIndex = TrackedRaids.FindIndex(i => i.Raid == raid);
                if (findIndex != -1)
                {
                    dataGridView1.Rows[newIndex].Cells[2].Tag = TrackedRaids[findIndex];
                    UpdateRowTime(newIndex);
                }
            }

            dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[2].ReadOnly = true;
            dataGridView1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[3].ReadOnly = true;

            dataGridView1_SelectionChanged(this, e);
            listView1.Items[0].Selected = true;
        }


        #region File I/O

        public void SaveXML()
        {
            try
            {
                _saveXML();
            }
            catch (Exception ex)
            {
                switch (MessageBox.Show(ex.Message, "Could not save tracked raids data", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Error))
                {
                    case DialogResult.Abort:
                        return;
                    case DialogResult.Retry:
                        SaveXML();
                        break;
                }
            }
        }

        private void _saveXML()
        {
            if (TrackedRaids.Count == 0)
            {
                if (File.Exists(Filename))
                    File.Delete(Filename);

                return;
            }

            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;

            using (var writer = XmlWriter.Create(Filename, settings))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("Raids");

                foreach (var r in TrackedRaids)
                {
                    writer.WriteStartElement("Raid");

                    writer.WriteStartElement("Filename");
                    writer.WriteString(r.Raid.Filename);
                    writer.WriteEndElement();

                    writer.WriteStartElement("LastSeen");
                    writer.WriteString(r.LastSeen.ToFileTime().ToString());
                    writer.WriteEndElement();

                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
                writer.WriteEndDocument();
            }
        }

        public void ReadXML()
        {
            if (File.Exists(Filename))
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(Filename);

                foreach (XmlNode node in xmlDoc.DocumentElement)
                {
                    DateTime lastSeen = new DateTime();
                    string file = "";

                    foreach (XmlNode data in node.ChildNodes)
                    {
                        if (!String.IsNullOrEmpty(node.InnerText))
                        {
                            switch (data.Name)
                            {
                                case "Filename":
                                    file = data.InnerText;
                                    break;
                                case "LastSeen":
                                    long ticks = long.Parse(data.InnerText);
                                    lastSeen = DateTime.FromFileTimeUtc(ticks);
                                    break;
                            }
                        }
                    }

                    int findIndex = DataReader.Raids.FindIndex(i => i.Filename == file);
                    if (findIndex != -1)
                        TrackedRaids.Add(new TrackedRaid(DataReader.Raids[findIndex], lastSeen));

                }
            }


        }

        #endregion



        void SelectRaid(DataReader.Raid raid)
        {
            if (raid != null)
            {

                int findIndex = TrackedRaids.FindIndex(i => i.Raid == raid);
                if (findIndex != -1)
                {
                    var tracked = TrackedRaids[findIndex];
                    label_lastSeen.Text = tracked.LastSeen.ToShortDateString() + " " + tracked.LastSeen.ToShortTimeString();
                    var next = tracked.Next();
                    label_nextRaid.Text = next.ToShortDateString() + " " + next.ToShortTimeString();

                    var remaining = next - DateTime.Now;
                    label_remaining.Text = $"{TimeString(remaining.Days, remaining.Hours, remaining.Minutes, remaining.Seconds)}";
                }
                else
                {
                    label_lastSeen.Text = label_nextRaid.Text = label_remaining.Text = "";
                }

                PopulateRaidSpawns(raid);

                textBox1.Text = File.ReadAllText(DataReader.monsterFolder + "\\" + raid.Filename);

            }
            

        }

        void PopulateRaidSpawns(DataReader.Raid raid)
        {
            listView1.Items.Clear();

            label_raidName.Text = raid.Filename;

            foreach (var spawn in raid.Spawns)
                AddRaidSpawn(spawn);

            for (int i = 1; i < listView1.Columns.Count; i++)
                listView1.Columns[i].AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize);

        }

        void UpdateRowTime(int row)
        {
            TrackedRaid raid = (TrackedRaid)dataGridView1.Rows[row].Cells[2].Tag;

            if (raid == null)
            {
                dataGridView1.Rows[row].Cells[2].Value =
                    dataGridView1.Rows[row].Cells[3].Value
                    = null;

                dataGridView1.Rows[row].Cells[3].Style.BackColor = Color.White;

                return;
            }

            dataGridView1.Rows[row].Cells[2].Value = raid.LastSeen;
            var next = raid.Next();
            dataGridView1.Rows[row].Cells[3].Value = next;

            if ((next - DateTime.Now).Days == 0)
                dataGridView1.Rows[row].Cells[3].Style.BackColor = Color.PaleVioletRed;
        }
    



        private void AddRaidSpawn(DataReader.Raid.RaidSpawn spawn)
        {
            // Race Count Delay Spread Message Items Lifetime Position

            string race = spawn.Race.ToString();
            string count = spawn.Count.Item1.ToString();
            if (spawn.Count.Item1 != spawn.Count.Item2)
                count += "-" + spawn.Count.Item2;
            string delay = spawn.Delay.ToString();
            string spread = spawn.Spread.ToString();
            string message = spawn.Message;
            string itemDrops = spawn.ItemDrops.Count.ToString();
            string lifetime = spawn.Lifetime.ToString();
            string position = spawn.Position.X + "," + spawn.Position.Y + "," + spawn.Position.Z;

            ListViewItem item = new ListViewItem(new string[] { "", race, count, delay, spread, itemDrops, lifetime, position, message });
            item.Tag = spawn;
            listView1.Items.Add(item);
        }


        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedCells[0] != null)
                SelectRaid((DataReader.Raid)dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Tag);
        }


        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 2)
            {
                var cell = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];
                var raid = (DataReader.Raid)dataGridView1.Rows[e.RowIndex].Tag;

                TrackedRaid trackedRaid = null;
                if (cell.Tag != null)
                    trackedRaid = (TrackedRaid)cell.Tag;



                var timePicker = new Form_DateTimePicker();
                timePicker.StartPosition = FormStartPosition.Manual;
                timePicker.Location = Cursor.Position;

                if (trackedRaid != null)
                    timePicker.SetDateTime(trackedRaid.LastSeen);

                timePicker.FormClosing += (s, ee) =>
                {
                    switch (timePicker.DialogResult)
                    {
                        case DialogResult.OK:
                            if (trackedRaid == null)
                            {
                                trackedRaid = new TrackedRaid(raid, timePicker.SelectedTime);
                                cell.Tag = trackedRaid;
                                TrackedRaids.Add(trackedRaid);
                            }
                            else
                                trackedRaid.LastSeen = timePicker.SelectedTime;

                            UpdateRowTime(e.RowIndex);
                        break;

                        case DialogResult.Abort:
                            if (trackedRaid != null)
                            {
                                cell.Tag = null;
                                TrackedRaids.Remove(trackedRaid);
                                UpdateRowTime(e.RowIndex);
                            }

                        break;
                    }

                    dataGridView1.Invalidate();

                    if (timePicker.DialogResult != DialogResult.None)
                        SaveXML();
                };

                timePicker.Show();

            }
        }

        string CleanRaidName(string fileName)
        {
            string raidName = fileName.Substring(0, fileName.Length - 4);
            raidName = char.ToUpper(raidName[0]) + raidName.Substring(1);

            return raidName;
        }

        string TimeString(int days, int hours, int minutes, int seconds)
        {
            var timeString = days + " days";

            // All of the cipsoft raids are in full day intervals, but adding this in case someone adapts this for another server
            if (hours != 0 || minutes != 0 || seconds != 0)
            {
                var addonString = "";
                if (seconds != 0) addonString = seconds.ToString("D2") + "s";
                if (minutes != 0) addonString = minutes.ToString("D2") + "m" + addonString;
                if (hours != 0) addonString = hours + "h" + addonString;

                timeString = timeString + " and " + addonString;
            }

            return timeString;
        }

        public class TrackedRaid
        {
            public DataReader.Raid Raid;
            public DateTime LastSeen;

            public TrackedRaid(DataReader.Raid raid, DateTime dateTime)
            {
                this.Raid = raid;
                LastSeen = dateTime;
            }

            public TrackedRaid(DataReader.Raid raid, string dateString)
            {
                this.Raid = raid;
                LastSeen = DateTime.Parse(dateString, System.Globalization.CultureInfo.InvariantCulture);
            }

            public DateTime Next()
            {
                var time = LastSeen.AddSeconds(Raid.Interval);

                while (time.CompareTo(DateTime.Now) == -1)
                    time = time.AddSeconds(Raid.Interval);

                return time;
            }
        }

        private void listView1_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            var spawn = (DataReader.Raid.RaidSpawn)e.Item.Tag;

            control_MinimapViewer1.CenterToPoint(new Point(spawn.Position.X, spawn.Position.Y), spawn.Position.Z);
        }
    }
}
