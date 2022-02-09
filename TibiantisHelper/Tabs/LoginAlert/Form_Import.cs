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
    public partial class Form_Import : Form
    {

        public bool ImportToExisting;
        public string ImportNewGroupName;
        public TrackedPlayerGroup ImportGroupDestination;   // Null on new
        public List<string> ImportPlayers;


        public Form_Import(List<string> players)
        {
            ImportPlayers = players;

            init();
        }

        public Form_Import(TrackedPlayerGroup group)
        {
            ImportPlayers = group.Players;

            init();

            textBox1.Text = group.Name;

            foreach (var g in comboBox1.Items)
                if ((string)g == group.Name)
                {
                    radioButton2.Checked = true;
                    comboBox1.SelectedItem = g;
                    break;
                }
        }


        private void init()
        {
            InitializeComponent();


            foreach (var p in ImportPlayers)
                listBox1.Items.Add(p);

            foreach (var g in Tab_LoginAlert.PlayerGroups)
                comboBox1.Items.Add(g.Name);
        }


        private void button1_Click(object sender, EventArgs e)
        {
            if (!ImportToExisting)
                if (Tab_LoginAlert.PlayerGroups.First(ss => ss.Name == textBox1.Text) != null)
                {
                    MessageBox.Show("Duplicate group name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

            ImportNewGroupName = textBox1.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
