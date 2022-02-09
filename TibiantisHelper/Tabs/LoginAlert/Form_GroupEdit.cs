using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TibiantisHelper.Tabs.LoginAlert
{
    public partial class Form_GroupEdit : Form
    {

        public string GroupName = "";

        public Form_GroupEdit()
        {
            InitializeComponent();
        }
    
        public Form_GroupEdit(string name)
        {
            InitializeComponent();
            textBox1.Text = name;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GroupName = textBox1.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
