using System;
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
    public partial class Form_DateTimePicker : Form
    {

        public DateTime SelectedTime;

        public Form_DateTimePicker()
        {
            InitializeComponent();
        }

        public void SetDateTime(DateTime dateTime)
        {
            dateTimePicker1.Value = dateTimePicker2.Value = dateTime;
        }

        private void Form_DateTimePicker_Deactivate(object sender, EventArgs e)
        {
            if (this.DialogResult == DialogResult.None)
                this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SelectedTime = new DateTime(
                dateTimePicker2.Value.Year,
                dateTimePicker2.Value.Month,
                dateTimePicker2.Value.Day,
                dateTimePicker1.Value.Hour,
                dateTimePicker1.Value.Minute,
                dateTimePicker1.Value.Second);

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Abort;
            this.Close();
        }
    }
}
