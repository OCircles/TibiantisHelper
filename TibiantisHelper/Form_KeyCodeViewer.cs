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

namespace TibiantisHelper
{
    public partial class Form_KeyCodeViewer : Form
    {

        public string PlayerName;
        public string Alert;
        

        public Form_KeyCodeViewer()
        {
            InitializeComponent();
        }

        private void LoginTrackerDialog_Shown(object sender, EventArgs e)
        {
            textBox1.Focus();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            textBox1.Text = "Input: " + e.KeyCode.ToString() + Environment.NewLine;
            textBox1.Text += "Keycode: " + (uint)(e.KeyCode);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
