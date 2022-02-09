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
    public partial class Form_IconSelect : Form
    {

        public int SelectedIcon;

        private int[] icons = { 
            207,    // White Checkers     
            3398,   // Game Master
            4197,   // Noob  
            3668,   // Hero
            500,    // Iron Helmet
            2244,   // Dwarfen Shield
            1929,   // Parchment
            417,    // Gold x100
            283,    // Skull
            492,    // Fire
            745,    // Blood
            2142,   // Moldy Cheese
            1547,   // Dead Human
            3606,   // Necromancer
            832,    // Orc WL
            2254,   // Ghoul
            2410,   // Troll
            2648,   // Rat
            4079,   // Cave Rat
            3152,   // Pig
            4094,   // Rabbit
            6094,   // Heart
            3383,   // Teddybear
            1647,   // Doll
            2160,   // Xmas Gift
            3254,   // Candy Cane
            4614    // Piggybank
        };


        List<Image> defaultIcons;
        Image lastImage;

        public Form_IconSelect()
        {
            InitializeComponent();

            defaultIcons = new List<Image>();
            lastImage = new Bitmap(32, 32);

            foreach (var icon in icons)
                defaultIcons.Add(DataReader.ReadSprite(icon));

            PopulateIcons();

            comboBox_icons.SelectedIndex = 0;
        }

        private void PopulateIcons()
        {
            var iconList = new List<Image>();

            iconList.AddRange(defaultIcons);
            iconList.Add(lastImage);

            ImageComboBox.DisplayImages(comboBox_icons, iconList);
        }

        private void Form_IconSelect_Deactivate(object sender, EventArgs e)
        {
            this.SelectedIcon = (int)numericUpDown1.Value;
            Console.WriteLine(SelectedIcon);
            this.Close();
        }

        private void comboBox_icons_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox_icons.SelectedIndex < icons.Length)
                numericUpDown1.Value = icons[comboBox_icons.SelectedIndex];
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            int val = (int)numericUpDown1.Value;

            var i = Array.IndexOf(icons, val);


            if (i == -1)
            {
                lastImage = DataReader.ReadSprite(val);
                PopulateIcons();
                comboBox_icons.SelectedIndex = comboBox_icons.Items.Count-1;
            }
            else
            {
                comboBox_icons.SelectedIndex = i;
            }


        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form_IconSelect_Load(object sender, EventArgs e)
        {
            this.Size = new Size(70, 89); // WHY DO I NEED THIS????????????
        }
    }
}
