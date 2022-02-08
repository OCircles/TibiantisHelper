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
    public partial class Form_AddPlayerGroupDialog : Form
    {

        public string GroupName;

        public Form_AddPlayerGroupDialog()
        {
            InitializeComponent();
        }

        private void button_add_Click(object sender, EventArgs e)
        {
            this.GroupName = textBox1.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

    }
}
