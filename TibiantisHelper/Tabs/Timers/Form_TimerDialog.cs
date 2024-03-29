﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TibiantisHelper
{
    public partial class Form_TimerDialog : Form
    {

        public string TimerName;
        public string Time;
        public int Multiplier;
        public bool AutoRestart;
        public bool TraybarFlash;

        public Form_TimerDialog(bool edit = false, string name = "", string time = "00:00:00", int multiplier = 0, bool autoRestart = false)
        {
            InitializeComponent();

            if (edit)
            {
                this.button1.Text = "Edit";
                this.Text = "Edit timer";
                this.textBox1.Text = name;
                this.textBox2.Text = time;
                this.numericUpDown1.Value = multiplier;
                this.checkBox1.Checked = autoRestart;
                this.checkBox2.Checked = TraybarFlash;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox1.Text))
            {
                TimeSpan time = new TimeSpan();
                var success = TimeSpan.TryParse(textBox2.Text, out time);

                if (success)
                {
                    if (time.TotalSeconds != 0)
                    {
                        this.TimerName = textBox1.Text;
                        this.Time = textBox2.Text;
                        this.Multiplier = Convert.ToInt32(numericUpDown1.Value);
                        this.AutoRestart = checkBox1.Checked;
                        this.TraybarFlash = checkBox2.Checked;

                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else
                        MessageBox.Show("Invalid time input, cannot be 0 seconds long", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                     
                }
                else
                    MessageBox.Show("Invalid time input, please use hh:mm:ss format (e.g. 00:14:30)", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
                MessageBox.Show("Cannot leave timer name blank", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            
        }
        
    }
}
