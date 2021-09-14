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
    public partial class Tab_Items : UserControl
    {

        string itemSearch = "";

        public Tab_Items()
        {
            InitializeComponent();

            if (!DesignMode)
            {
                listView_items.UseFiltering = true;

                InitializeItemTrades();
                InitializeItemDrops();

                items_comboBox_itemCategory.SelectedIndex = 1;
            }
        }


        #region Item Columns

        private OLVColumn itemCol_Attack()
        {
            var atk = new OLVColumn();
            atk.Text = "Attack";
            atk.AspectGetter = delegate (object x) { return ((Item)x).GetAttributeValue("WeaponAttackValue"); };

            return atk;
        }
        private OLVColumn itemCol_AttackRanged()
        {
            var atk = new OLVColumn();
            atk.Text = "Attack";
            atk.AspectGetter = delegate (object x) { return ((Item)x).GetAttributeValue("ThrowAttackValue"); };

            return atk;
        }
        private OLVColumn itemCol_Range()
        {
            var range = new OLVColumn();
            range.Text = "Range";
            range.AspectGetter = delegate (object x) {
                int rangeValue = ((Item)x).GetAttributeValue("BowRange");
                if (rangeValue == 0) rangeValue = ((Item)x).GetAttributeValue("ThrowRange");
                return rangeValue;
            };

            return range;
        }
        private OLVColumn itemCol_WeaponDefense()
        {
            var def = new OLVColumn();
            def.Text = "Defense";
            def.AspectGetter = delegate (object x) { return ((Item)x).GetAttributeValue("WeaponDefendValue"); };
            return def;
        }


        private OLVColumn itemCol_Arm()
        {
            var armor = new OLVColumn();
            armor.Text = "Arm";
            armor.AspectGetter = delegate (object x) { return ((Item)x).GetAttributeValue("ArmorValue"); };

            return armor;
        }
        private OLVColumn itemCol_ShieldDefense()
        {
            var def = new OLVColumn();
            def.Text = "Defense";
            def.AspectGetter = delegate (object x) { return ((Item)x).GetAttributeValue("ShieldDefendValue"); };
            return def;
        }
        private OLVColumn itemCol_SkillModifier()
        {
            var skillMod = new OLVColumn();
            skillMod.Text = "+Skill";
            skillMod.AspectGetter = delegate (object x) { return ((Item)x).GetAttributeValue("SkillModification"); };
            skillMod.AspectToStringConverter = delegate (object x)
            {
                var skill = (int)x;

                if (skill == 0) return "N/A";
                else
                    return skill.ToString();
            };

            return skillMod;
        }

        private OLVColumn itemCol_RuneCharges()
        {
            var charges = new OLVColumn();
            charges.Text = "Charges";
            charges.AspectGetter = delegate (object x)
            {
                Item i = (Item)x;

                Rune r = DataReader.Runes.Where(oo => oo.ID == i.ID).FirstOrDefault();
                if (string.IsNullOrEmpty(r.Name)) return (byte)0;
                else return r.Charges;

            };
            charges.AspectToStringConverter = delegate (object x)
            {
                var i = (byte)x;

                if (i == 0) return "N/A";
                else return i.ToString();
            };

            return charges;
        }


        private OLVColumn itemCol_ItemName()
        {
            OLVColumn itemName = new OLVColumn();
            itemName.Text = "Name";
            itemName.FillsFreeSpace = true;
            itemName.AspectGetter = delegate (object x)
            {
                Item i = (Item)x;

                if (i.Flags.Contains("Rune"))
                {
                    Rune r = DataReader.Runes.Where(oo => oo.ID == i.ID).FirstOrDefault();
                    if (string.IsNullOrEmpty(r.Name)) return "Spell Rune " + i.ID + " (unused)";
                    return r.Name;
                }
                else
                    return i.Name;
            };
            itemName.ImageGetter = delegate (object x)
            {
                var entity = DataReader.ReadEntity(((Item)x).ID - 100);
                if (entity != null) return DataReader.ReadSprite(entity.sprites[0]);
                else return null;
            };

            return itemName;
        }

        private OLVColumn itemCol_Weight()
        {
            OLVColumn weight = new OLVColumn();
            weight.Text = "Weight";
            weight.AspectGetter = delegate (object x) { return ((double)((Item)x).GetAttributeValue("Weight")) / 100; };
            weight.AspectToStringFormat = "{0} oz";

            return weight;
        }


        private OLVColumn itemCol_FoodRegen()
        {
            var regen = new OLVColumn();
            regen.Text = "Regen";
            regen.AspectGetter = delegate (object x) { return ((Item)x).GetAttributeValue("Nutrition"); };
            regen.AspectToStringConverter = delegate (object x)
            {
                TimeSpan t = TimeSpan.FromSeconds(((double)((int)x * 2) / 10) * 60);

                string answer = string.Format("{0:D2}:{1:D2}",
                                t.Minutes,
                                t.Seconds);

                return answer;
            };

            return regen;
        }
        private OLVColumn itemCol_FoodNBP()
        {

            var nutritionByPrice = new OLVColumn();
            nutritionByPrice.Text = "P/R";
            nutritionByPrice.AspectGetter = delegate (object x) {
                var nutrition = ((Item)x).GetAttributeValue("Nutrition");

                // Find cheapers vendor price
                double price = 9999;

                foreach (var npc in DataReader.Npcs)
                    foreach (var t in npc.transactions)
                        if (t.ItemID == ((Item)x).ID && t.Type == NPC.TransactionType.Buy)
                            if (t.Price < price) price = t.Price;

                return Math.Round(price / nutrition, 2);
            };
            nutritionByPrice.AspectToStringConverter = delegate (object x)
            {
                var value = (double)x;

                if (value > 100) return "N/A";
                else return value.ToString();
            };
            nutritionByPrice.ToolTipText = "Cheapest vendor price / regen. Lower values mean it's more priceworthy";

            return nutritionByPrice;
        }
        private OLVColumn itemCol_FoodNBW()
        {

            var nutritionByWeight = new OLVColumn();
            nutritionByWeight.Text = "W/R";
            nutritionByWeight.AspectGetter = delegate (object x) {
                var weight = ((Item)x).GetAttributeValue("Weight");
                var nutrition = (double)((Item)x).GetAttributeValue("Nutrition");

                return Math.Round(weight / nutrition, 2);
            };
            nutritionByWeight.ToolTipText = "Weight / regen. Lower values mean better regen by weight";

            return nutritionByWeight;
        }

        private OLVColumn itemCol_ExpireTime()
        {
            var expiry = new OLVColumn();
            expiry.Text = "Duration";
            expiry.AspectGetter = delegate (object x) { return ((Item)x).GetAttributeValue("TotalExpireTime"); };
            expiry.AspectToStringConverter = delegate (object x)
            {
                var time = (int)x;

                if (time == 0) return "N/A";
                else
                {
                    TimeSpan t = TimeSpan.FromSeconds(time);
                    if (t.Hours != 0) return string.Format("{0:D2}:{1:D2}:{2:D2}", t.Hours, t.Minutes, t.Seconds);
                    else return string.Format("{0:D2}:{1:D2}", t.Minutes, t.Seconds);
                }
            };

            return expiry;
        }

        private OLVColumn itemCol_ContainerCapacity()
        {
            var capacity = new OLVColumn();
            capacity.Text = "Size";

            capacity.AspectGetter = delegate (object x) { return ((Item)x).GetAttributeValue("Capacity"); };

            return capacity;
        }

        #endregion

        private void PopulateItemList(string filter, OLVColumn[] columns = null, bool matchAnyFilter = false)
        {
            listView_items.Columns.Clear();

            listView_items.SmallImageList = new ImageList();
            listView_items.SmallImageList.ImageSize = new Size(32, 32);

            listView_items.Columns.Add(itemCol_ItemName());

            List<Item> items = new List<Item>();

            if (matchAnyFilter)
            {
                var split = filter.Split(',');

                foreach (var f in split)
                    items.AddRange(DataReader.GetItemsByFilter(DataReader.Items, f));

                items = items.Distinct().ToList();
            }
            else
                items.AddRange(DataReader.GetItemsByFilter(DataReader.Items, filter));

            if (columns != null)
                listView_items.Columns.AddRange(columns);

            listView_items.Columns.Add(itemCol_Weight());

            listView_items.Objects = items;
        }

        private void items_comboBox_itemCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            itemSearch = "";

            // Column headers + attribute names

            if (items_comboBox_itemCategory.Text == "All") PopulateItemList("Take");

            if (items_comboBox_itemCategory.Text == "Helmet") PopulateItemList("BodyPosition=1", new OLVColumn[] { itemCol_Arm() });
            if (items_comboBox_itemCategory.Text == "Armor") PopulateItemList("BodyPosition=4", new OLVColumn[] { itemCol_Arm() });
            if (items_comboBox_itemCategory.Text == "Legs") PopulateItemList("BodyPosition=7", new OLVColumn[] { itemCol_Arm() });
            if (items_comboBox_itemCategory.Text == "Boots") PopulateItemList("BodyPosition=8", new OLVColumn[] { itemCol_Arm() });
            if (items_comboBox_itemCategory.Text == "Necklace") PopulateItemList("BodyPosition=2", new OLVColumn[] { itemCol_Arm() });
            if (items_comboBox_itemCategory.Text == "Ring") PopulateItemList("BodyPosition=9", new OLVColumn[] { itemCol_SkillModifier(), itemCol_ExpireTime() });
            if (items_comboBox_itemCategory.Text == "Shield") PopulateItemList("Shield", new OLVColumn[] { itemCol_ShieldDefense() });


            if (items_comboBox_itemCategory.Text == "Axe") PopulateItemList("WeaponType=3", new OLVColumn[] { itemCol_Attack(), itemCol_WeaponDefense() });
            if (items_comboBox_itemCategory.Text == "Club") PopulateItemList("WeaponType=2", new OLVColumn[] { itemCol_Attack(), itemCol_WeaponDefense() });
            if (items_comboBox_itemCategory.Text == "Sword") PopulateItemList("WeaponType=1", new OLVColumn[] { itemCol_Attack(), itemCol_WeaponDefense() });
            if (items_comboBox_itemCategory.Text == "Distance") PopulateItemList("BowRange=*,ThrowRange=*", new OLVColumn[] { itemCol_AttackRanged(), itemCol_Range() }, true);
            if (items_comboBox_itemCategory.Text == "Ammo") PopulateItemList("AmmoType=*");

            if (items_comboBox_itemCategory.Text == "Equippable Container") PopulateItemList("BodyPosition=3", new OLVColumn[] { itemCol_ContainerCapacity() });
            if (items_comboBox_itemCategory.Text == "Runes") PopulateItemList("Rune", new OLVColumn[] { itemCol_RuneCharges() });
            if (items_comboBox_itemCategory.Text == "Light") PopulateItemList("Light,Expire,Take", new OLVColumn[] { itemCol_ExpireTime() });
            if (items_comboBox_itemCategory.Text == "Food") PopulateItemList("Food", new OLVColumn[] { itemCol_FoodRegen(), itemCol_FoodNBP(), itemCol_FoodNBW() });

        }
        private void items_comboBox_itemCategory_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                itemSearch = items_comboBox_itemCategory.Text.ToLower();

                PopulateItemList("Take");
                listView_items.ModelFilter = Items_BuildFilter();
            }
        }


        private void items_checkBox_hideExpiring_CheckedChanged(object sender, EventArgs e)
        {
            listView_items.ModelFilter = Items_BuildFilter();
        }


        private ModelFilter Items_BuildFilter()
        {
            ModelFilter filter = new ModelFilter(delegate (object x) {
                bool search, hideExpiring;
                search = hideExpiring = true;

                if (items_checkBox_hideExpiring.Checked)
                    hideExpiring = !((Item)x).Name.ToLower().Contains('(');

                if (!string.IsNullOrEmpty(itemSearch)) search = ((Item)x).Name.ToLower().Contains(itemSearch);

                return search && hideExpiring;

            });

            return filter;
        }

        private void listView_items_SelectionChanged(object sender, EventArgs e)
        {
            if (listView_items.SelectedObject != null)
            {

                Item selected = (Item)listView_items.SelectedObject;

                DisplayItemInfo(selected);


            }
        }

        public void DisplayItemInfo(Item item)
        {
            itemName_label.Text = item.Name;

            string itemInfo = "ID: " + item.ID;

            if (!string.IsNullOrEmpty(item.Description)) itemInfo += ",," + "--- Description ---,," + item.Description;

            itemInfo += ",,--- Flags ---,,";

            foreach (var flag in item.Flags) itemInfo += "  " + flag + ",";

            itemInfo += ",--- Attributes ---,,";

            foreach (var att in item.Attributes) itemInfo += "  " + att.Name + " = " + att.Value + ",";

            textBox1.Lines = itemInfo.Split(',');

            var entity = DataReader.ReadEntity(item.ID - 100);

            itemIcon.Image = DataReader.ReadSprite(entity.sprites[0]);


            List<ItemTrade> trades = new List<ItemTrade>();

            foreach (var npc in DataReader.Npcs)
            {
                foreach (var t in npc.transactions)
                {
                    if (t.ItemID == item.ID)
                    {
                        ItemTrade trade;

                        trade.npc = npc;
                        trade.transaction = t;

                        trades.Add(trade);
                    }
                }
            }

            List<ItemDrop> drops = new List<ItemDrop>();

            foreach (var mon in DataReader.Monsters)
            {
                foreach (var d in mon.Inventory)
                {
                    if (d.ItemID == item.ID)
                    {
                        ItemDrop drop;

                        drop.monster = mon;
                        drop.drop = d;

                        drops.Add(drop);
                    }
                }
            }

            listView_trades.Objects = trades;
            listView_itemDrops.Objects = drops;

        }

        private void InitializeItemTrades()
        {
            olvColumn7.AspectGetter = delegate (object x) { return ((ItemTrade)x).npc.Name; };
            olvColumn8.AspectGetter = delegate (object x) { return ((ItemTrade)x).transaction.Price; };
            olvColumn9.AspectGetter = delegate (object x) { return ((ItemTrade)x).npc.Location.Name; };

            olvColumn7.GroupKeyGetter =
            olvColumn8.GroupKeyGetter =
            olvColumn9.GroupKeyGetter = delegate (object x) {
                switch (((ItemTrade)x).transaction.Type)
                {
                    case NPC.TransactionType.Buy:
                        return "Buy from";
                    case NPC.TransactionType.Sell:
                        return "Sell to";
                }
                return "Error";
            };
        }
        private void InitializeItemDrops()
        {

            olvColumn10.AspectGetter = delegate (object x) { return ((ItemDrop)x).monster.Name; };
            olvColumn12.AspectGetter = delegate (object x) { return ((ItemDrop)x).drop.Amount; };
            olvColumn11.AspectGetter = delegate (object x) { return ((double)(((ItemDrop)x).drop.Rate) / 10); };
            olvColumn11.AspectToStringFormat = "{0}%";
        }


        private void listView_trades_ItemActivate(object sender, EventArgs e)
        {
            var trade = (ItemTrade)listView_trades.SelectedObject;

            var pos = trade.npc.Position;
            Utility.OpenInBrowser("https://tibiantis.info/library/map#" + pos.X + "," + pos.Y + "," + pos.Z + ",8");

        }
        private void listView_itemDrops_ItemActivate(object sender, EventArgs e)
        {
            var drop = (ItemDrop)listView_itemDrops.SelectedObject;

            Form_Main.Form.tabControl1.SelectedTab = Form_Main.Form.tabPage_monsters;
            Form_Main.Form.monsters_listView.SelectedObject = drop.monster;
        }




        private struct ItemTrade
        {
            public NPC npc;
            public NPC.Transaction transaction;
        }

        private struct ItemDrop
        {
            public Monster monster;
            public Monster.Drop drop;
        }

    }
}
