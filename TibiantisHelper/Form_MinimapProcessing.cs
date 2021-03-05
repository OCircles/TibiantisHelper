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
    public partial class Form_MinimapProcessing : Form
    {
        private string mapDirectory;
        private List<Tuple<byte,string>> mapFiles;

        private string currentFileName = "";
        private short currentFile = 0;
        private short maxFiles = 0;
        private int totalFiles = 0;

        private short currentLayer = 0;
        private short minLayers = 0;
        private short maxLayers = 0;

        public Form_MinimapProcessing(string mapDirectory)
        {
            this.mapFiles = new List<Tuple<byte, string>>();
            this.mapDirectory = mapDirectory;
            
            var maps = Directory.GetFiles(mapDirectory, $"*.map").ToList();

            this.totalFiles = maps.Count();

            byte biggest = 0;
            byte smallest = 255;
            foreach ( var map in maps )
            {
                byte layer = byte.Parse(map.Substring(map.Length - 6, 2));
                mapFiles.Add(new Tuple<byte, string>(layer, map));

                if (layer < smallest) smallest = layer;
                if (layer > biggest) biggest = layer;
            }

            this.maxLayers = biggest;
            this.minLayers = smallest;

            InitializeComponent();

            progressBar1.Maximum = this.totalFiles;
        }

        public Bitmap ReadLayer(short layer)
        {
            var files = new List<string>();

            foreach (var file in mapFiles)
                if (file.Item1 == layer) files.Add(file.Item2);

            this.currentLayer = layer;
            this.currentFile = 0;
            this.maxFiles = (short)files.Count();

            int smallX, smallY;
            smallX = smallY = 5000;

            int bigX, bigY, bigZ;
            bigX = bigY = 0;

            foreach (var f in files)
            {
                var fn = Path.GetFileName(f);

                short x = short.Parse(fn.Substring(0, 3));
                short y = short.Parse(fn.Substring(3, 3));

                if (x < smallX) smallX = x;
                if (y < smallY) smallY = y;

                if (x > bigX) bigX = x;
                if (y > bigY) bigY = y;

            }

            int dx = (bigX - smallX + 1) * 256;
            int dy = (bigY - smallY + 1) * 256;

            Bitmap layerMap = new Bitmap(dx, dy);

            for (int y = 0; y < bigY - smallY + 1; y++)
            {
                for (int x = 0; x < bigX - smallX + 1; x++)
                {
                    var fn = $"{(smallX + x).ToString("D3")}{(smallY + y).ToString("D3")}{layer.ToString("D2")}.map";
                    var file = $"{this.mapDirectory}\\{fn}";

                    this.currentFile++;
                    this.currentFileName = fn;

                    this.progressBar1.Value++;

                    UpdateLabels();

                    Bitmap cBmp = null;
                    if (File.Exists(file))
                        cBmp = DataReader.ReadMinimap(file);

                    for (int px = 0; px < 256; px++)
                    {
                        int lx = (x * 255) + px;
                        for (int py = 0; py < 256; py++)
                        {
                            int ly = (y * 255) + py;

                            Color c = Color.Black;
                            if (cBmp != null)
                                c = cBmp.GetPixel(px, py);

                            if (lx < layerMap.Width && ly < layerMap.Height)
                                layerMap.SetPixel(lx, ly, c);
                        }

                    }
                }
            }

            return layerMap;
        }
    
        private void UpdateLabels()
        {
            this.label1.Text = $"Generating minimap images (Layer {this.currentLayer}/{this.maxLayers})";
            this.label2.Text = $"{this.currentFileName} ({this.currentFile}/{this.maxFiles})";

            this.label1.Update();
            this.label2.Update();
        }

        private void Form_MinimapProcessing_Shown(object sender, EventArgs e)
        {
            for (short i = this.minLayers; i < this.maxLayers; i++)
            {
                var bmp = ReadLayer(i);
                bmp.Save(i + ".png",System.Drawing.Imaging.ImageFormat.Png);
                bmp.Dispose();
            }
        }
    }
}
