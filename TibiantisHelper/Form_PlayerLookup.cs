using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static TibiantisHelper.Utility;

namespace TibiantisHelper
{
    public partial class Form_PlayerLookup : Form
    {

        public string TimerName;
        public string Time;
        public int Multiplier;
        public bool AutoRestart;

        public Form_PlayerLookup()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox1.Text))
            {
                string[] info = await GetUserData(textBox1.Text);

                if (info != null)
                {
                    var result = new Form_PlayerLookupResult(info[0], info[1], info[2], info[3], info[4], info[5], info[6]);
                    result.ShowDialog();
                }
                else
                    MessageBox.Show($"Could not retrieve info on player \"{textBox1.Text}\", did you mistype it?", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
                MessageBox.Show("Cannot leave player name blank", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            
        }
        
    }
}
