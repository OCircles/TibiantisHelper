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
    public partial class Form_AccountAddDialog : Form
    {

        public string AccountName;
        public string AccountLogin;
        public DateTime AccountPremium;
        public DateTime AccountHouse;



        public Form_AccountAddDialog()
        {
            InitializeComponent();
        }

        public Form_AccountAddDialog(string name, string login, DateTime premium, DateTime house)
        {
            InitializeComponent();
            this.Text = "Edit account";
            button1.Text = "Edit";

            textBox1.Text = name;
            textBox2.Text = login;

            if (premium.CompareTo(dateTimePicker1.Value) != -1)
                dateTimePicker1.Value = premium;

            if (house.CompareTo(dateTimePicker2.Value) != -1)
                dateTimePicker2.Value = house;

            DateTime blank = new DateTime();

            if (premium != blank) dateTimePicker1.Checked = true;
            if (house != blank) dateTimePicker2.Checked = true;

        }

        private void LoginTrackerDialog_Shown(object sender, EventArgs e)
        {
            textBox1.Focus();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if ( !string.IsNullOrEmpty(textBox1.Text) && !string.IsNullOrEmpty(textBox2.Text) )
            {
                this.AccountName = textBox1.Text;
                this.AccountLogin = textBox2.Text;

                if (dateTimePicker1.Checked)
                    this.AccountPremium = dateTimePicker1.Value;

                if (dateTimePicker2.Checked)
                    this.AccountHouse = dateTimePicker2.Value;

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Name or Account was left blank!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

    }
}
