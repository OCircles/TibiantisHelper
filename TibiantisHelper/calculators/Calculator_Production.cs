using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static TibiantisHelper.DataReader;

namespace TibiantisHelper
{
    public partial class Calculator_Production : UserControl
    {
        Form_Main mainForm;

        public Calculator_Production(Form_Main mainForm)
        {
            this.mainForm = mainForm;
            InitializeComponent();

            // Foods
            List<Item> foods = mainForm._dataReader.items.Where(i => i.Flags.Contains("Food")).OrderBy(i => i.Name).ToList();
            foreach (var f in foods) calculator_production_comboBox_food.Items.Add(f.Name);
            calculator_production_comboBox_food.SelectedIndex = 0;

            // Runes
            UpdateProductionDropdown();
            CalculateProduction();
        }

        string calculator_production_addTimer_time = "";
        string calculator_production_addTimer_title = "";

        public void CalculateProduction()
        {

            if (calculator_production_comboBox_rune.Text != "N/A")
            {
                bool num1, num2;
                num1 = num2 = false;

                int in1, in2;
                in1 = in2 = 0;

                num1 = int.TryParse(calculator_production_textBox_amountSingle.Text, out in1);
                num2 = int.TryParse(calculator_production_textBox_amountBackpack.Text, out in2);

                if (string.IsNullOrEmpty(calculator_production_textBox_amountSingle.Text))
                {
                    num1 = true;
                    in1 = 0;
                }

                if (string.IsNullOrEmpty(calculator_production_textBox_amountBackpack.Text))
                {
                    num2 = true;
                    in2 = 0;
                }

                if (num1 && num2)
                {
                    if (in1 + in2 > 0)
                    {
                        Item food = mainForm._dataReader.items.Where(f => f.Name == calculator_production_comboBox_food.Text).FirstOrDefault();
                        Rune rune = mainForm._dataReader.runes.Where(r => r.Name == calculator_production_comboBox_rune.Text).FirstOrDefault();
                        Spell spell = mainForm._dataReader.spells.Where(s => s.Name == calculator_production_comboBox_rune.Text).FirstOrDefault();

                        string itemName = rune.Name;

                        int amount = in1 + (in2 * 20);
                        double casts = amount;

                        if (spell.Type == 0)
                        {
                            // For arrows/bolts, adjust amount for stacks
                            itemName = mainForm._dataReader.items.Where(i => i.ID == spell.ProduceID).FirstOrDefault().Name;

                            casts = Math.Ceiling((double)(amount * 100) / spell.ProduceAmount);
                        }


                        double totalMana = casts * spell.Mana;

                        double regenSec = (totalMana / Form_Main._selectedVocation.regenMP) * 60;
                        double foodSec = ((double)((int)food.GetAttributeValue("Nutrition") * 2) / 10) * 60;
                        double foodDiv = Math.Ceiling(regenSec / foodSec);


                        TimeSpan regenTime = TimeSpan.FromSeconds(regenSec);

                        string regenString = "";

                        if (regenTime.Hours != 0) regenString += string.Format("{0:D1}h", regenTime.Hours + (regenTime.Days * 24));
                        if (regenTime.Minutes != 0) regenString += string.Format("{0:D1}m", regenTime.Minutes);
                        if (regenTime.Seconds != 0) regenString += string.Format("{0:D1}s", regenTime.Seconds);

                        calculator_production_addTimer_time = string.Format("{0:D2}:{1:D2}:{2:D2}", regenTime.Hours + (regenTime.Days * 24), regenTime.Minutes, regenTime.Seconds); ;
                        calculator_production_addTimer_title = itemName;


                        // Assemble result in textbox. The arrows/bolts stuff makes this a huge mess lol

                        mainForm.calculator_textBox_result.Text = $"A {Form_Main._selectedVocation.Name} producing {itemName} (";

                        string parenthesis = "";

                        if (spell.Type == 1)
                            parenthesis = $"{casts}x";
                        else
                        {
                            parenthesis += $"{amount} stack";
                            if (amount != 1) parenthesis += "s";
                        }
                        calculator_production_addTimer_title += $" {parenthesis} ({Form_Main._selectedVocation.Name})";


                        mainForm.calculator_textBox_result.Text += parenthesis;

                        mainForm.calculator_textBox_result.Text += $") takes {regenString} and uses {foodDiv}x {food.Name} (" +
                            (((double)food.GetAttributeValue("Weight")) / 100) * foodDiv + "oz)";


                        var unitsString = "";
                        if (spell.ProduceAmount != 0) unitsString = $", generating {spell.ProduceAmount} units each time";


                        mainForm.calculator_textBox_result.Text += Environment.NewLine + Environment.NewLine;
                        mainForm.calculator_textBox_result.Text +=
                            $"Casting \"{spell.Words}\" {casts} times" +
                            $"{unitsString} for a total of " +
                            $"{totalMana} mana ({spell.Mana} mana per cast)";

                    }
                    else
                    {
                        mainForm.calculator_textBox_result.Text = "Please input production amount above";
                    }
                }
                else
                {
                    mainForm.calculator_textBox_result.Text = "Could not parse input";
                }
            }
        }
        public void UpdateProductionDropdown()
        {
            string oldSelection = calculator_production_comboBox_rune.Text;

            calculator_production_comboBox_rune.Items.Clear();

            List<Spell> spells = mainForm._dataReader.spells.Where(s => s.ProduceID != 0).OrderBy(s => s.Name).ToList();

            foreach (var s in spells)
            {
                bool add = false;
                if (mainForm.header_vocation_comboBox.SelectedIndex == 0 && s.VocKnight) add = true;
                if (mainForm.header_vocation_comboBox.SelectedIndex == 1 && s.VocPaladin) add = true;
                if (mainForm.header_vocation_comboBox.SelectedIndex == 2 && s.VocSorcerer) add = true;
                if (mainForm.header_vocation_comboBox.SelectedIndex == 3 && s.VocDruid) add = true;

                if (add)
                    calculator_production_comboBox_rune.Items.Add(s.Name);
            }
            if (calculator_production_comboBox_rune.Items.Count == 0) calculator_production_comboBox_rune.Items.Add("N/A");

            int oldIndex = calculator_production_comboBox_rune.Items.IndexOf(oldSelection);

            if (oldIndex != -1)
                calculator_production_comboBox_rune.SelectedIndex = oldIndex;
            else
                calculator_production_comboBox_rune.SelectedIndex = 0;
        }

        private void calculator_production_button_addTimer_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(calculator_production_addTimer_time))
            {
                mainForm.TimersAdd(calculator_production_addTimer_title, calculator_production_addTimer_time, 0, false);
                mainForm.tabControl1.SelectTab("Timers");
            }
        }


        private void calculator_production_textBox_TextChanged(object sender, EventArgs e)
        {
            CalculateProduction();
        }

        private void calculator_production_textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar);
        }

        private void calculator_production_comboBox_food_SelectedIndexChanged(object sender, EventArgs e)
        {
            CalculateProduction();
        }

        private void calculator_production_comboBox_rune_SelectedIndexChanged(object sender, EventArgs e)
        {
            CalculateProduction();
        }






    }
}
