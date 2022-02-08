using BrightIdeasSoftware;
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
    public partial class Form_SpriteBrowser : Form
    {

        private List<int> Sprites;
        private int Selected = 0;
        Bitmap ShownImage;

        public Form_SpriteBrowser()
        {
            Sprites = new List<int>();

            InitializeComponent();

            ShownImage = new Bitmap(pictureBox1.Size.Width, pictureBox1.Size.Height);

            int sn = 0;

            bool reading = true;

            while (reading)
            {
                try
                {
                     var bmp = DataReader.ReadSprite(sn);

                    if (bmp != null)
                        Sprites.Add(sn);


                } catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    reading = false;
                }

                sn++;
            }

            fastObjectListView1.SmallImageList = new ImageList();
            fastObjectListView1.SmallImageList.ImageSize = new Size(32, 32);

            fastObjectListView1.Columns.Add(spriteCol());

            fastObjectListView1.Objects = Sprites;

        }

        private OLVColumn spriteCol()
        {
            OLVColumn spriteID = new OLVColumn();
            spriteID.Text = "ID";
            spriteID.FillsFreeSpace = true;
            spriteID.AspectGetter = delegate (object x)
            {
                int i = (int)x;

                return i;
            };
            spriteID.ImageGetter = delegate (object x)
            {
                int i = (int)x;

                return DataReader.ReadSprite(i);
            };

            return spriteID;
        }

        private void fastObjectListView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (fastObjectListView1.SelectedObjects.Count != 0)
            {
                int sprite = (int)fastObjectListView1.SelectedObjects[0];

                Selected = sprite;
                var bmp = DataReader.ReadSprite(sprite);
                ShownImage = new Bitmap(128,128);

                label1.Text = "Selected: " + Selected;

                using (Graphics g = Graphics.FromImage(ShownImage))
                {
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
                    g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half;

                    g.DrawImage(bmp, new Rectangle(Point.Empty, new Size(128,128)));
                }

                pictureBox1.Image = ShownImage;
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {

            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = "PNG image (*.png)|*.png";
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    DataReader.ReadSprite(Selected).Save(saveFileDialog1.FileName);
                } catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning);
                }
            }
        }
    }


}
