using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static TibiantisHelper.Utility;

namespace TibiantisHelper
{
    public partial class Form_PlayerLookupResult : Form
    {

        private int longestText = 0;
        private int labelCount = 0;

        public Form_PlayerLookupResult(string name, string level, string vocation, string promotion, string guild, string guildID, string characters)
        {
            InitializeComponent();
            button_ok.Focus();

            string nameSafe = System.Net.WebUtility.UrlEncode(name);

            AddLabel(name, "https://tibiantis.online/?page=character&name=" + nameSafe);
            AddLabel($"{vocation} (Level {level})");

            if (guild == "None")
                AddLabel("No Guild");
            else
            {
                AddLabel($"Guild: {guild}", "https://tibiantis.online/?page=showguild&id=" + guildID);
            }

            AddLabel(promotion);

            if (!string.IsNullOrEmpty(characters))
            {
                AddLabel(" ");
                AddLabel("Other characters:");
                AddLabel(" ");

                var chrs = characters.TrimEnd(';').Split(';');

                foreach (var chr in chrs)
                {
                    var carr = chr.Split(',');

                    var cname = carr[0];
                    var clvl = carr[1];

                    AddLabel($"{cname} (Level {clvl})");
                }
            }


            foreach (Control con in this.Controls)
            {
                if (con is Label || con is LinkLabel)
                {
                    con.Width = longestText;
                }
            }

        }

        private void AddLabel(string text, string link = "")
        {
            // Tried using flowlayout but turns out it's awful so I guess we're doing it manually

            var lbl = new Label();
            if (link != "")
            {
                lbl = new LinkLabel(); // I can't believe this worked
                ((LinkLabel)lbl).LinkClicked += (e,a) => { Process.Start(link); };
            }

            lbl.Text = text;

            Graphics g = lbl.CreateGraphics();
            int width = (int)g.MeasureString(lbl.Text, lbl.Font).Width;
            g.Dispose();


            if (width > longestText)
                longestText = width;

            lbl.AutoSize = false;
            lbl.Width = longestText;
            lbl.Height = lbl.Font.Height + 2;

            var loc = new Point(13, 13 + (labelCount * lbl.Font.Height) + (labelCount * 2));

            labelCount++;


            this.Width = longestText + (13 * 3);
            this.Height = (13 * 4) + (labelCount * lbl.Font.Height) + (labelCount * 2) + (button_ok.Height*2);
            this.Controls.Add(lbl);

            lbl.Location = loc;
        }

        private void button_ok_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
