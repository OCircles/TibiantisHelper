using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TibiantisHelper
{
    public partial class Form_CurrentOnline : Form
    {
        private List<Form_Main.Player> Players;

        Color colorKnight = Color.LightCoral;
        Color colorPaladin = Color.SkyBlue;
        Color colorSorcerer = Color.DarkGray;
        Color colorDruid = Color.MediumSeaGreen;
        Color colorNovoc = Color.WhiteSmoke;

        public Form_CurrentOnline(List<Form_Main.Player> Players)
        {
            this.Players = Players;

            InitializeComponent();
        }

        private void CurrentOnlineForm_Shown(object sender, EventArgs e)
        {
            textBox1.Focus();
            AddAllUsers();
        }

        private void CurrentOnlineForm_Deactivate(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox1.Text))
            {
                listView1.Items.Clear();
                foreach (var p in Players)
                {
                    if (p.Name.Length >= textBox1.Text.Length)
                        if (p.Name.Substring(0, textBox1.Text.Length).Equals(textBox1.Text, StringComparison.OrdinalIgnoreCase))
                            AddPlayer(p);
                }
                listView1.Columns[1].AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
                listView1.Columns[3].Width = listView1.Width - listView1.Columns[1].Width - listView1.Columns[2].Width - 17;
            }
            else
                AddAllUsers();
        }

        private void AddAllUsers()
        {
            listView1.Items.Clear();
            foreach (var p in Players)
                AddPlayer(p);

            listView1.Columns[1].AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
            listView1.Columns[3].Width = listView1.Width - listView1.Columns[1].Width - listView1.Columns[2].Width - 17;
        }

        private void AddPlayer(Form_Main.Player player)
        {
            ListViewItem item = new ListViewItem(new string[] { "", player.Level.ToString(), player.Vocation, player.Name });
            item.Tag = player;
            ColorItem(item, player.Vocation);
            listView1.Items.Add(item);
        }

        private void ColorItem(ListViewItem item, string voc)
        {
            switch (voc)
            {
                case "EK":
                case "K":
                    item.BackColor = colorKnight;
                    break;
                case "RP":
                case "P":
                    item.BackColor = colorPaladin;
                    break;
                case "MS":
                case "S":
                    item.BackColor = colorSorcerer;
                    break;
                case "ED":
                case "D":
                    item.BackColor = colorDruid;
                    break;
                default:
                    item.BackColor = colorNovoc;
                    break;
            }
        }

        private void listView1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var item = listView1.GetItemAt(e.X, e.Y);
                if ( item != null )
                {
                    var player = (Form_Main.Player)item.Tag;
                    var ct = new ContextMenuStrip();

                    ct.Items.Add("Name to Clipboard", null, (s,ee) => { Utility.StringToClipboard(player.Name); });
                    ct.Items.Add("Add to Login Alert", null, (s, ee) => {
                        ((Form_Main)Owner).LoginTrackerAddPlayer(player.Name);
                        ((Form_Main)Owner).OpenLoginAlertTab();
                    });

                    ct.Show(Cursor.Position);
                }

            }
        }

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (listView1.SelectedItems[0] == null) return;

            var player = (Form_Main.Player)listView1.SelectedItems[0].Tag;
            string webSafe = System.Net.WebUtility.UrlEncode(player.Name);

            Utility.OpenInBrowser(@"https://tibiantis.online/?page=character&name=" + webSafe);

        }
    }
}
