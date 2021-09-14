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
using static TibiantisHelper.DataReader;

namespace TibiantisHelper.Tabs
{
    public partial class Tab_Spells : UserControl
    {
        public Tab_Spells()
        {
            InitializeComponent();
        }

        private void Tab_Spells_Load(object sender, EventArgs e)
        {
            if (DesignMode)
                return;

            spells_comboBox_vocation.SelectedIndex = 0;
            spells_comboBox_group.SelectedIndex = 0;
            spells_comboBox_type.SelectedIndex = 0;
            spells_comboBox_premium.SelectedIndex = 0;

            listView_spells.UseFiltering = true;


            olvColumn32.AspectGetter = delegate (object x)
            {
                Spell s = (Spell)x;
                string classes = "";

                if (s.VocKnight) classes += "K,";
                if (s.VocPaladin) classes += "P,";
                if (s.VocSorcerer) classes += "S,";
                if (s.VocDruid) classes += "D";

                if (classes[classes.Length - 1] == ',') classes = classes.Substring(0, classes.Length - 1);

                return classes;
            };


            olvColumn27.AspectToStringConverter = delegate (object x) {
                byte type = (byte)x;
                if (type == 1) return "Rune";
                return "Instant";
            };

            olvColumn36.AspectToStringConverter = delegate (object x) {
                byte group = (byte)x;
                if (group == 1) return "Support";
                if (group == 2) return "Attack";
                return "Healing";
            };

            olvColumn28.AspectToStringConverter = delegate (object x)
            {
                bool prem = (bool)x;
                if (prem) return "Yes";
                return "No";
            };

            listView_spells.Objects = DataReader.Spells;
        }


        private void comboBox_filters_SelectedIndexChanged(object sender, EventArgs e)
        {
            listView_spells.ModelFilter = new ModelFilter(delegate (object x) {
                bool vocFilter, groupFilter, typeFilter, premiumFilter;
                vocFilter = groupFilter = typeFilter = premiumFilter = false;
                Spell s = (Spell)x;

                switch (spells_comboBox_vocation.SelectedIndex)
                {
                    case 0:
                        vocFilter = true;
                        break;
                    case 1:
                        vocFilter = s.VocKnight;
                        break;
                    case 2:
                        vocFilter = s.VocPaladin;
                        break;
                    case 3:
                        vocFilter = s.VocSorcerer;
                        break;
                    case 4:
                        vocFilter = s.VocDruid;
                        break;
                }

                switch (spells_comboBox_group.SelectedIndex)
                {
                    case 0:
                        groupFilter = true;
                        break;
                    default:
                        groupFilter = s.Group == spells_comboBox_group.SelectedIndex - 1;
                        break;
                }

                switch (spells_comboBox_type.SelectedIndex)
                {
                    case 0:
                        typeFilter = true;
                        break;
                    default:
                        typeFilter = s.Type == spells_comboBox_type.SelectedIndex - 1;
                        break;
                }


                switch (spells_comboBox_premium.SelectedIndex)
                {
                    case 0:
                        premiumFilter = true;
                        break;
                    case 1:
                        premiumFilter = s.Premium == true;
                        break;
                    case 2:
                        premiumFilter = s.Premium == false;
                        break;
                }


                return vocFilter & groupFilter & typeFilter & premiumFilter;
            });



        }

        private void listView_spells_SelectionChanged(object sender, EventArgs e)
        {
            if (listView_spells.SelectedObject != null)
            {
                Spell s = (Spell)listView_spells.SelectedObject;

                spells_label.Text = s.Name;

                List<NPC> npcs = new List<NPC>();

                foreach (NPC n in DataReader.Npcs)
                {
                    foreach (var t in n.transactions)
                        if (t.Type == NPC.TransactionType.Spell && t.ItemID == s.ID) npcs.Add(n);
                }

                listView_npcs.Objects = npcs;

            }
        }

        private void listView_npcs_ItemActivate(object sender, EventArgs e)
        {
            var npc = (NPC)listView_npcs.SelectedObject;

            var pos = npc.Position;
            Utility.OpenInBrowser("https://tibiantis.info/library/map#" + pos.X + "," + pos.Y + "," + pos.Z + ",8");
        }

    }
}
