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
        private List<string> Players;

        public Form_CurrentOnline(List<string> Players)
        {
            this.Players = Players;

            InitializeComponent();
        }

        private void CurrentOnlineForm_Shown(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            listBox1.Items.AddRange(this.Players.ToArray());
        }

        private void CurrentOnlineForm_Deactivate(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
