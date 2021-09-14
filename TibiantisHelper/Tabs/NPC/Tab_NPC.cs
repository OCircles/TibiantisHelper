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
    public partial class Tab_NPC : UserControl
    {
        public Tab_NPC()
        {
            InitializeComponent();
        }
        private void Tab_Npcs_Load(object sender, EventArgs e)
        {
            if (DesignMode)
                return;

            olvColumn1.AspectGetter = delegate (object x) { return ((NPC)x).Name; };
            olvColumn2.AspectGetter = delegate (object x) { return ((NPC)x).Location.Name; };
            olvColumn3.AspectGetter = delegate (object x) { return ((NPC)x).Vendor; };
            olvColumn4.AspectGetter = delegate (object x) { return ((NPC)x).IsTeacher; };

            olvColumn3.AspectToStringConverter =
            olvColumn4.AspectToStringConverter = delegate (object x)
            {
                if ((bool)x) return "Yes";
                return "No";
            };

            listView_npcs.Objects = DataReader.Npcs;


            // Items list

            listView_npcItems.SmallImageList = new ImageList();
            listView_npcItems.SmallImageList.ImageSize = new Size(32, 32);

            olvColumn5.ImageGetter = delegate (object x) {
                var transaction = (NPC.Transaction)x;
                if (transaction.ItemID > 100)
                {
                    byte spriteIndex = 0;

                    Entity entity = DataReader.ReadEntity(transaction.ItemID - 100);

                    // If it's a vial
                    if (transaction.ItemID == 2874)
                        spriteIndex = (Items.LiquidContainers.GetLiquidContainerName((NPC.Transaction)x)).spriteIndex;

                    return DataReader.ReadSprite(entity.sprites[spriteIndex]);
                }
                else
                    return "Spell";
            };

            olvColumn5.AspectGetter = delegate (object x)
            {
                if (((NPC.Transaction)x).ItemID > 100)
                {
                    var item = DataReader.Items.Where(i => i.ID == ((NPC.Transaction)x).ItemID).FirstOrDefault();
                    if (item != null)
                    {
                        if (item.Flags.Contains("LiquidContainer"))
                        {
                            if (((NPC.Transaction)x).Data != 0)
                            {
                                var container = Items.LiquidContainers.GetLiquidContainerName((NPC.Transaction)x);
                                return $"{item.Name} ({container.liquid})";
                            }
;
                        }
                        return item.Name;
                    }
                    else
                        return "Removed post-7.4 item";
                }
                else
                {
                    Spell spell = DataReader.Spells.Where(s => s.ID == ((NPC.Transaction)x).ItemID).FirstOrDefault();
                    return spell.Name;
                }
            };

            olvColumn5.GroupKeyGetter = delegate (object x)
            {
                if (((NPC.Transaction)x).Type == NPC.TransactionType.Buy) return "Selling";
                else if (((NPC.Transaction)x).Type == NPC.TransactionType.Sell) return "Buying";
                else return "Teaching";
            };

            olvColumn6.AspectGetter = delegate (object x) { return ((NPC.Transaction)x).Price; };
        }


        private void PopulateNpcItemList(NPC npc)
        {
            if (npc != null)
            {
                List<NPC.Transaction> list = new List<NPC.Transaction>();

                list.AddRange(npc.GetTransactions(NPC.TransactionType.Buy));
                list.AddRange(npc.GetTransactions(NPC.TransactionType.Sell));
                list.AddRange(npc.GetTransactions(NPC.TransactionType.Spell));

                listView_npcItems.Objects = list;

                listView_npcItems.BuildList();
            }

        }
        private void listView_npcs_SelectionChanged(object sender, EventArgs e)
        {
            if (listView_npcs.SelectedObject != null)
            {
                label_npcName.Text = (listView_npcs.SelectedObject as NPC).Name;
                PopulateNpcItemList((NPC)listView_npcs.SelectedObject);
            }
        }
        private void listView_npcs_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var test = listView_npcs.HitTest(e.X, e.Y).Item;

                if (test != null)
                {
                    var npc = ((NPC)((OLVListItem)test).RowObject);

                    var menu = new ContextMenu();

                    var posD = new MenuItem("Location");
                    var posView = new MenuItem("View", (s, ee) => {
                        var pos = npc.Position;
                        Utility.OpenInBrowser("https://tibiantis.info/library/map#" + pos.X + "," + pos.Y + "," + pos.Z + ",8");
                    });

                    var posClip = new MenuItem("To clipboard", (s, ee) =>
                    {
                        var pos = npc.Position;
                        Utility.StringToClipboard("https://tibiantis.info/library/map#" + pos.X + "," + pos.Y + "," + pos.Z + ",8");
                    });


                    var file = new MenuItem("Open", (s, ee) => {
                        System.Diagnostics.Process.Start(npcFolder + "\\" + npc.Filename);
                        // Console.WriteLine(npcFolder + "\\" + npc.Filename);
                    });



                    posD.MenuItems.Add(posView);
                    posD.MenuItems.Add(posClip);

                    menu.MenuItems.Add(posD);
                    menu.MenuItems.Add(file);

                    menu.Show(listView_npcs, new Point(e.X, e.Y));



                }
            }




        }

        private void listView_npcItems_ItemActivate(object sender, EventArgs e)
        {
            if (listView_npcItems.SelectedObject != null)
            {
                var trade = (NPC.Transaction)listView_npcItems.SelectedObject;

                if (trade.Type != NPC.TransactionType.Spell)
                {
                    var item = DataReader.Items.Where(i => i.ID == trade.ItemID).FirstOrDefault();

                    if (item != null)
                    {
                        Form_Main.Form.tabControl1.SelectedTab = Form_Main.Form.tabPage_items;
                        Form_Main.Form.tab_Items1.DisplayItemInfo(item);
                    }
                }
                else
                {
                    Spell spell = DataReader.Spells.Where(s => s.ID == trade.ItemID).FirstOrDefault();

                    Form_Main.Form.tabControl1.SelectedTab = Form_Main.Form.tabPage_spells;
                    Form_Main.Form.tab_Spells1.listView_spells.SelectedObject = spell;
                }

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
