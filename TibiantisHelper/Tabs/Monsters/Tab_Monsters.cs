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
using static TibiantisHelper.DataReader;

namespace TibiantisHelper.Tabs
{
    public partial class Tab_Monsters : UserControl
    {
        public Tab_Monsters()
        {
            InitializeComponent();
        }

        private void Tab_Monsters_Load(object sender, EventArgs e)
        {
            if (DesignMode)
                return;

            listView_monsters.UseFiltering = true;

            // Monster list

            // Summon mana
            olvColumn18.AspectToStringConverter = delegate (object x)
            {
                var s = (int)x;

                if (s == 0) return "N/A";
                return s + " MP";
            };

            listView_monsters.Objects = DataReader.Monsters;

            // Monster drop list
            olvColumn21.AspectGetter = delegate (object x) {
                Item item = DataReader.Items.Where(i => i.ID == ((Monster.Drop)x).ItemID).FirstOrDefault();
                if (item != null)
                    return item.Name;
                else
                    return "Removed post-7.4 item";
            };
            olvColumn22.AspectGetter = delegate (object x) { return ((Monster.Drop)x).Amount; };
            olvColumn23.AspectGetter = delegate (object x) { return ((double)(((Monster.Drop)x).Rate) / 10); };
            olvColumn23.AspectToStringFormat = "{0}%";

            olvColumn24.AspectGetter = delegate (object x)
            {
                int id = ((Monster.Drop)x).ItemID;
                int cheapest = 0;
                foreach (NPC npc in DataReader.Npcs)
                {
                    foreach (NPC.Transaction transaction in npc.transactions)
                    {
                        if (transaction.ItemID == id)
                            if (transaction.Type == NPC.TransactionType.Sell && transaction.Price > cheapest) cheapest = transaction.Price;
                    }
                }
                return cheapest;
            };
        }



        private void listView_monsters_SelectionChanged(object sender, EventArgs e)
        {

            if (listView_monsters.SelectedObject != null)
            {

                Monster selected = (Monster)listView_monsters.SelectedObject;
                monsters_labelName.Text = selected.Name;
                listView_drops.Objects = selected.Inventory;

                try
                {
                    monsters_textBox.Lines = File.ReadAllLines(monsterFolder + "\\" + selected.Filename);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "File read error");
                }
            }
        }

        private void listView_drops_ItemActivate(object sender, EventArgs e)
        {
            var drop = (Monster.Drop)listView_drops.SelectedObject;
            Item item = DataReader.Items.Where(i => i.ID == drop.ItemID).FirstOrDefault();

            if (item != null)
            {
                Form_Main.Form.tabControl1.SelectedTab = Form_Main.Form.tabPage_items;
                Form_Main.Form.tab_Items1.DisplayItemInfo(item);
            }
        }


        private void checkBox_hideUniques_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_hideUniques.Checked)
                listView_monsters.ModelFilter = new ModelFilter(delegate (object x) { return !string.IsNullOrEmpty(((Monster)x).Article); });
            else
                listView_monsters.ModelFilter = null;
        }

        private void checkBox_showSummonLevel_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_showSummonLevel.Checked)
            {
                olvColumn18.AspectToStringConverter = delegate (object x)
                {
                    var s = (int)x;

                    if (s == 0) return "N/A";
                    return (Math.Ceiling(((decimal)s - 35) / 30) + 8).ToString();
                };
            }
            else
            {
                olvColumn18.AspectToStringConverter = delegate (object x)
                {
                    var s = (int)x;

                    if (s == 0) return "N/A";
                    return s + " MP";
                };
            }
            listView_monsters.SetObjects(listView_monsters.Objects);
        }

    }
}
