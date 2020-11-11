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
    public partial class Form_LoginAlertAddDialog : Form
    {

        public string PlayerName;
        public string Alert;
        

        public Form_LoginAlertAddDialog()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.PlayerName = textBox1.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var diag = new OpenFileDialog();
            diag.RestoreDirectory = true;
            
            diag.Filter = "WAV files (*.wav)|*.wav";

            if (diag.ShowDialog() == DialogResult.OK)
            {
                textBox2.Text = Path.GetFileName(diag.FileName);
                this.Alert = diag.FileName;
            }

        }

        private void LoginTrackerDialog_Shown(object sender, EventArgs e)
        {
            textBox1.Focus();
        }
    }
}
