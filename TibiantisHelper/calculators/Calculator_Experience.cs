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
    public partial class Calculator_Experience : UserControl
    {
        Form_Main mainForm;

        public Calculator_Experience(Form_Main mainForm)
        {
            this.mainForm = mainForm;
            InitializeComponent();
        }

        private void CalculateExperience()
        {
            TimeSpan startTime, endTime;
            bool startTimeParsed, endTimeParsed;
            startTimeParsed = endTimeParsed = false;

            startTimeParsed = TimeSpan.TryParse(calculator_experience_textBox_startTime.Text, out startTime);
            endTimeParsed = TimeSpan.TryParse(calculator_experience_textBox_endTime.Text, out endTime);

            if (startTimeParsed & endTimeParsed & startTime.Days == 0 & endTime.Days == 0)
            {
                int startExp, endExp;
                startExp = endExp = 0;

                bool startExpParsed, endExpParsed;
                startExpParsed = endExpParsed = false;

                startExpParsed = int.TryParse(calculator_experience_textBox_startExp.Text, out startExp);
                endExpParsed = int.TryParse(calculator_experience_textBox_endExp.Text, out endExp);

                if (startExpParsed & endExpParsed)
                {
                    double totalExp = endExp - startExp;

                    if (totalExp > 0)
                    {
                        TimeSpan diff = new TimeSpan();

                        if (endTime.TotalSeconds < startTime.TotalSeconds)
                        {
                            TimeSpan endOfDay = new TimeSpan(1, 0, 0, 0);
                            endOfDay = endOfDay.Subtract(startTime);

                            diff = diff.Add(endOfDay);
                            diff = diff.Add(endTime);
                        }
                        else
                            diff = endTime.Subtract(startTime);

                        double expPerHour = (totalExp / diff.TotalSeconds) * 60 * 60;

                        string durationString = "";


                        if (diff.Hours != 0) durationString += string.Format("{0:D1}h", diff.Hours);
                        if (diff.Minutes != 0) durationString += string.Format("{0:D1}m", diff.Minutes);
                        if (diff.Seconds != 0) durationString += string.Format("{0:D1}s", diff.Seconds);

                        mainForm.calculator_textBox_result.Text = $"You gained {totalExp} experience over a duration of " +
                            $"{durationString} ({Math.Round(expPerHour, 1)} exp per hour)";


                    }
                }

            }

        }

        private void calculator_experience_button_startTime_Click(object sender, EventArgs e)
        {
            calculator_experience_textBox_startTime.Text = DateTime.Now.ToString("HH:mm:ss");
        }

        private void calculator_experience_button_endTime_Click(object sender, EventArgs e)
        {
            calculator_experience_textBox_endTime.Text = DateTime.Now.ToString("HH:mm:ss");
        }
        private void calculator_textBox_TextChanged(object sender, EventArgs e)
        {
            CalculateExperience();
        }

    }
}
