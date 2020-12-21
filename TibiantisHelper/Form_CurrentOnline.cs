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

        public Form_CurrentOnline(List<Form_Main.Player> Players)
        {
            this.Players = Players;

            InitializeComponent();
        }

        private void CurrentOnlineForm_Shown(object sender, EventArgs e)
        {
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
                listBox1.Items.Clear();
                foreach (var p in Players)
                {
                    if (p.Name.Length >= textBox1.Text.Length)
                        if (p.Name.Substring(0, textBox1.Text.Length).Equals(textBox1.Text,StringComparison.OrdinalIgnoreCase))
                            listBox1.Items.Add(GeneratePlayerString(p));
                }
            }
            else
                AddAllUsers();
        }

        private string GeneratePlayerString(Form_Main.Player p)
        {
            string parenthesis = $"({p.Level}, {p.Vocation})";
            var length = 10;
            return parenthesis.PadRight(length).Substring(0, length) + p.Name; 
        }

        private void AddAllUsers()
        {
            listBox1.Items.Clear();
            foreach (var p in Players)
                listBox1.Items.Add(GeneratePlayerString(p));
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            string name = listBox1.SelectedItem.ToString().Split(')')[1].Substring(1);
            string webSafe = System.Net.WebUtility.UrlEncode(name);

            Utility.OpenInBrowser(@"https://tibiantis.online/?page=character&name=" + webSafe);
        }

        private void listBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                Point pt = new Point(e.X, e.Y);
                var index = listBox1.IndexFromPoint(pt);
                if ( index >= 0 )
                {
                    listBox1.SelectedIndex = index;

                    string name = listBox1.SelectedItem.ToString().Split(')')[1].Substring(1);

                    var ct = new ContextMenuStrip();

                    ct.Items.Add("Name to Clipboard", null, (s,ee) => { Utility.StringToClipboard(name); });
                    ct.Items.Add("Add to Login Alert", null, (s, ee) => {
                        ((Form_Main)Owner).LoginTrackerAddPlayer(name);
                        ((Form_Main)Owner).OpenLoginAlertTab();
                    });

                    ct.Show(Cursor.Position);
                }
            }
        }
    }
}
