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
    public partial class Form_MinimapParseDialog : Form
    {
        string OutputPath;
        string InputPath;

        public Form_MinimapParseDialog(string inputPath, string outputPath)
        {
            this.InputPath = inputPath;
            this.OutputPath = outputPath;

            InitializeComponent();

            this.BackColor = TibiantisHelper.Form_Main.mainBackcolor;
        }

        private void Run()
        {
            Directory.CreateDirectory(OutputPath);
            Bitmap buffer = new Bitmap(Form_Main._miniMap.allSectorSize.Width, Form_Main._miniMap.allSectorSize.Height);

            for (int i = 0; i <= 15; i++)
            {
                label1.Text = "Parsing layer " + i + "/15...";

                buffer = Form_Main._miniMap.ReadLayer((byte)i, this.InputPath);
                buffer.Save(OutputPath + "\\" + i + ".png");

                progressBar1.PerformStep();
                label1.Update();
            }

            this.Close();
        }

        private void Form_MinimapParseDialog_Shown(object sender, EventArgs e)
        {
            this.Run();
        }
    }
}
