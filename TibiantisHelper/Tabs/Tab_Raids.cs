using BrightIdeasSoftware;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TibiantisHelper.Tabs
{
    public partial class Tab_Raids : UserControl
    {

        private DataReader DataReader;

        public Tab_Raids()
        {
            InitializeComponent();

        }

        private void Tab_Raids_Load(object sender, EventArgs e)
        {

            foreach (var raid in DataReader.raids)
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

                int newIndex = dataGridView1.Rows.Add(raid.Filename, timeString);

                dataGridView1.Rows[newIndex].Tag = raid;

            }

            dataGridView1_SelectionChanged(this,e);

        }


        private void AddRaidSpawn(DataReader.Raid.RaidSpawn spawn)
        {
            // Race Count Delay Spread Message Items Lifetime Position

            string race = spawn.Race.ToString();
            string count = spawn.Count.Item1 + "-" + spawn.Count.Item2;
            string delay = spawn.Delay.ToString();
            string spread = spawn.Spread.ToString();
            string message = spawn.Message;
            string itemDrops = spawn.ItemDrops.Count.ToString();
            string lifetime = spawn.Lifetime.ToString();
            string position = spawn.Position.X + "," + spawn.Position.Y + "," + spawn.Position.Z;

            ListViewItem item = new ListViewItem(new string[] { "", race, count, delay, spread, message, itemDrops, lifetime, position });
            item.Tag = spawn;
            listView1.Items.Add(item);
        }


        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedCells[0] != null)
            {
                var raid = (DataReader.Raid)dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Tag;

                // This gets called before grid is populated I think? Needs null check here
                if (raid != null)
                {
                    listView1.Items.Clear();

                    foreach (var spawn in raid.Spawns)
                        AddRaidSpawn(spawn);

                    for (int i = 1; i < listView1.Columns.Count; i++)
                        listView1.Columns[i].AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize);

                }


            }
        }
    }
}
