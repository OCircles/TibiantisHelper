using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TibiantisHelper.Tabs
{
    public partial class Tab_Calculators : UserControl
    {

        Calculator_Production Calculator_Production;
        Calculator_Experience Calculator_Experience;


        public Tab_Calculators()
        {
            InitializeComponent();
        }

        private void Tab_Calculators_Load(object sender, EventArgs e)
        {
            if (DesignMode)
                return;

            Calculator_Production = new Calculator_Production(calculator_textBox_result);
            calculator_addPage("Production", Calculator_Production);


            Calculator_Experience = new Calculator_Experience(calculator_textBox_result);
            calculator_addPage("Experience", Calculator_Experience);
        }


        public void UpdateVocation()
        {
            if (Calculator_Production != null)
                Calculator_Production.UpdateProductionDropdown();
        }


        private void calculator_addPage(string title, UserControl cc)
        {
            var page = new TabPage();
            page.BackColor = Color.White;
            page.Controls.Add(cc);
            cc.Dock = DockStyle.Fill;
            calculator_tabControl.TabPages.Add(page);
            listBox.Items.Add(title);
        }

        private void listBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            calculator_tabControl.SelectedIndex = listBox.SelectedIndex;
        }

    }
}
