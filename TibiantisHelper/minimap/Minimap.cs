using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace TibiantisHelper
{


    public class Minimap
    {
        private List<Bitmap> Subsections;


        private bool initialized;
        public bool Initialized
        {
            get { return IsInitialized(); }
            set { initialized = value; }
        }

        public Size MapSize = new Size(-1,-1);
        public Size TotalBitmapSize;


        private Point FirstSubsection = new Point(-1,-1);

        // Will have to be set manually depending on server in constructor, but I guess Tibiantis is the only one with floor reset so who cares..
        public Point firstSector = new Point(996,984);
        public Size allSectorSize = new Size(48, 48); // Defines sector dimensions, such as Tibiantis having 48x48 sectors


        public static string SavePath = "data\\minimap";


        public Minimap()
        {
            this.Subsections = new List<Bitmap>();

            var path = Minimap.SavePath + "\\map.xml";

            if (File.Exists(path))
                ReadXML(path);

        }

        public void ReadMapData(string mapDirectory)
        {
            if (!string.IsNullOrEmpty(mapDirectory))
            {
                short smallX, smallY;
                smallX = smallY = 5000;

                short bigX, bigY;
                bigX = bigY = 0;

                var mapFiles = Directory.GetFiles(mapDirectory, "*.map");

                if (mapFiles.Length != 0)
                {
                    foreach (var f in mapFiles)
                    {
                        var fn = Path.GetFileName(f);

                        short x = short.Parse(fn.Substring(0, 3));
                        short y = short.Parse(fn.Substring(3, 3));

                        if (x < smallX) smallX = x;
                        if (y < smallY) smallY = y;

                        if (x > bigX) bigX = x;
                        if (y > bigY) bigY = y;
                    }

                    this.FirstSubsection.X = smallX;
                    this.FirstSubsection.Y = smallY;

                    this.MapSize.Width = (bigX - smallX + 1);
                    this.MapSize.Height = (bigY - smallY + 1);

                    this.TotalBitmapSize.Width = this.MapSize.Width * 256;
                    this.TotalBitmapSize.Height = this.MapSize.Height * 256;

                    this.initialized = true;

                    Console.WriteLine("Initialized minimap " + this.TotalBitmapSize.Width);
                    return;
                }

                Console.WriteLine("Failed to initialize minimap (no map files found)");
            }
            Console.WriteLine("Failed to initialize minimap (directory null or empty)");


        }


        private bool IsInitialized()
        {
            this.initialized = true;

            for (int i = 0; i <= 15; i++)
                if (!File.Exists($"{Minimap.SavePath}\\{i}.png"))
                    this.initialized = false;

            if (MapSize.Width == -1 || MapSize.Height == -1 || FirstSubsection.X == -1 || FirstSubsection.Y == -1)
                this.initialized = false;

            return this.initialized;
        }

        public void SaveXML(string path)
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;

            using (var writer = XmlWriter.Create(path, settings))
            {
                writer.WriteStartDocument();

                writer.WriteStartElement("Minimap");

                writer.WriteStartElement("MapSize");
                writer.WriteString(MapSize.Width + "," + MapSize.Height);
                writer.WriteEndElement();


                writer.WriteStartElement("StartSector");
                writer.WriteString(FirstSubsection.X + "," + FirstSubsection.Y);
                writer.WriteEndElement();


                writer.WriteEndElement();


                writer.WriteEndDocument();
            }


        }

        private void ReadXML(string path)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(path);

            Size mapSize = new Size(-1, -1);
            Point firstSubsection = new Point(-1, -1);

            foreach (XmlNode node in xmlDoc.DocumentElement)
            {
                Console.WriteLine(node.Name);
                if (!String.IsNullOrEmpty(node.InnerText))
                {
                    switch (node.Name)
                    {
                        case "MapSize":
                            var ms = node.InnerText.Split(',');
                            mapSize = new Size(int.Parse(ms[0]), int.Parse(ms[1]));
                            break;
                        case "StartSector":
                            var ss = node.InnerText.Split(',');
                            firstSubsection = new Point(int.Parse(ss[0]), int.Parse(ss[1]));
                            break;
                    }
                }
            }

            this.initialized = true;

            if (mapSize.Width == -1 || mapSize.Height == -1 || firstSubsection.X == -1 || firstSubsection.Y == -1)
                this.initialized = false;

            this.TotalBitmapSize.Width = mapSize.Width * 256;
            this.TotalBitmapSize.Height = mapSize.Height * 256;

            if (!this.initialized)
                throw new FileLoadException("Could not read map data from " + path);

            this.MapSize = mapSize;
            this.FirstSubsection = firstSubsection;

        }


        public Point ImgPosToMapPos(Point position) { return new Point { X = position.X + (FirstSubsection.X * 256), Y = position.Y + (FirstSubsection.Y * 256) }; }
        public Point MapPosToImgPos(Point position) {
            Console.WriteLine(position.X + " " + position.Y);
            return new Point {
            X = position.X - (FirstSubsection.X * 256),
            Y = position.Y - (FirstSubsection.Y * 256)
        }; }

        public Bitmap ReadLayer(byte layer, string mapDirectory)
        {
            Bitmap bmp = null;

            if (initialized)
            {
                var subsectionCount = this.MapSize.Width * this.MapSize.Height;

                // Subsections must be prepared with proper size bitmaps beforehand because it will be written to by multiple threads
                for (int i = 0; i < subsectionCount; i++)
                    this.Subsections.Add(new Bitmap(256, 256));

                Parallel.For(0, this.MapSize.Height, y =>
                {
                    Parallel.For(0, this.MapSize.Width, x =>
                    {
                        int xx = x + this.FirstSubsection.X;
                        int yy = y + this.FirstSubsection.Y; ;

                        string s = xx.ToString("D3") + yy.ToString("D3") + layer.ToString("D2") + ".map";

                        this.Subsections[x + (this.MapSize.Width * y)] = ReadSubsection(mapDirectory + "\\" + s);
                    });
                });

                bmp = new Bitmap(this.MapSize.Width * 256, this.MapSize.Height * 256);

                using (Graphics gfx = Graphics.FromImage(bmp))
                    for (int y = 0; y < this.MapSize.Height; y++)
                        for (int x = 0; x < this.MapSize.Width; x++)
                        {
                            Point pnt = new Point(x * 256, y * 256);
                            gfx.DrawImage(this.Subsections[x + (y * this.MapSize.Width)], pnt);
                        }

                foreach (var a in this.Subsections)
                    a.Dispose();
            }



            return bmp;
        }

        private Bitmap ReadSubsection(string sectorFile)
        {
            // Using PixelFormat.Format24bppRgb doesn't seem to cause any visible difference :D lowers RAM pretty significantly
            Bitmap map = new Bitmap(256, 256,System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            // Just return black filled bitmap if file doesn't exist
            if (!File.Exists(sectorFile))
            {
                using (Graphics gfx = Graphics.FromImage(map))
                using (SolidBrush brush = new SolidBrush(Color.Black))
                    gfx.FillRectangle(brush, 0, 0, map.Width, map.Height);
                return map;
            }

            using (BinaryReader reader = new BinaryReader(File.OpenRead(sectorFile), Encoding.ASCII))
            {
                for (int x = 0; x < 256; x++)
                {
                    for (int y = 0; y < 256; y++)
                    {
                        byte current = reader.ReadByte();

                        int R = 51 * (current / 36);
                        int G = 51 * ((current / 6) % 6);
                        int B = 51 * (current % 6);

                        if (R > 255) R = 0;
                        if (G > 255) G = 0;
                        if (B > 255) B = 0;

                        Color c = Color.FromArgb(R, G, B);

                        map.SetPixel(x, y, c);

                        //if (y % 32 == 0 || x % 32 ==  0)
                        //    map.SetPixel(x, y, Color.Pink);


                        //if (y % 256 == 0 || x % 256 == 0)
                        //    map.SetPixel(x, y, Color.DarkRed);
                    }
                }
            }

            return map;
        }


    }
}
