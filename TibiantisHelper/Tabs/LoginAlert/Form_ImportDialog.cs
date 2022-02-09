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
    public partial class Form_ImportDialog : Form
    {

        // This is extra spaghetti but I'm in a hurry, sry :(

        public bool ImportToExisting;
        public string ImportNewGroupName;
        public TrackedPlayerGroup ImportGroupDestination;   // Null on new
        public List<string> ImportPlayers;


        public Form_ImportDialog(List<string> players, TrackedPlayerGroup destination = null)
        {
            ImportPlayers = players;
            if (destination != null)
                ImportGroupDestination = destination;

            init();
        }

        public Form_ImportDialog(TrackedPlayerGroup group, TrackedPlayerGroup destination = null)
        {
            ImportPlayers = group.Players;
            if (destination != null)
                ImportGroupDestination = destination;

            init();

            textBox1.Text = group.Name;

            SelectInCombobox(group.Name);
        }

        private void SelectInCombobox(string target)
        {
            foreach (var g in comboBox1.Items)
                if ((string)g == target)
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

            if (comboBox1.Items.Count != 0)
            {
                comboBox1.SelectedIndex = 0;
                if (ImportGroupDestination != null)
                {
                    SelectInCombobox(ImportGroupDestination.Name);
                    radioButton2.Checked = true;
                }
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            ImportToExisting = radioButton2.Checked;

            if (!ImportToExisting)
            {
                if (Tab_LoginAlert.PlayerGroups.Exists(ss => ss.Name == textBox1.Text))
                {
                    MessageBox.Show("Duplicate group name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                ImportGroupDestination = null;
            }
            else
            {
                var grp = Tab_LoginAlert.PlayerGroups.FirstOrDefault(ss => ss.Name == comboBox1.Text);
                ImportGroupDestination = grp;
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
