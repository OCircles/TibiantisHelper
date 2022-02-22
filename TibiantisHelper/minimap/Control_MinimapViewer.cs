using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TibiantisHelper
{
    public partial class Control_MinimapViewer : UserControl
    {
        Bitmap Image;

        public List<HighlightedArea> HighlightedAreas;

        private bool _mapFocused = false;
        public bool MapFocused { get { return _mapFocused; } }

        public bool DragControlEnabled = true;

        bool r_isDragging;
        bool r_isSelectingSector;
        bool r_isMoving { get { return k_moveUp || k_moveDown || k_moveLeft || k_moveRight; } }

        public byte Framerate
        {
            get { return c_framerate; }
            set { timer1.Interval = 1000 / value; c_framerate = value; }
        }

        private byte c_framerate = 90;
        private const float c_keyMoveScale = 0.4f;
        private const float c_zoomMax = 5.6f;
        private const float c_zoomMin = 0.6f;
        private float c_zoomScale = 1.0f;
        public const float c_scrollValue = 0.4f; // Determines how much is zoomed per scroll
        private Color background = Color.FromArgb(30, 34, 36);

        PointF _lastMousePos;
        Point _lastCrosshair = new Point(-1, -1);
        Color _lastCrosshairColor;

        public DataReader.Point3D Position
        {
            get
            {
                var xy = Form_Main._miniMap.ImgPosToMapPos(new Point((int)p_Transform.X, (int)p_Transform.Y));
                return new DataReader.Point3D(xy.X, xy.Y, p_Layer);
            }
        }

        PointF p_Transform;
        int p_Layer = 7;


        private Matrix transform = new Matrix();
        Point s_firstSectorImagePoint;
        Point s_lastSectorImagePoint; // Dunno if this is needed but using this to avoid potential terrible bad optimization
        Size s_allSectorPixels;
        Point s_lastSector;


        List<Tuple<Point, int>> MapMarkerPositions;
        List<MapMarker> MapMarkers;


        public bool IsDrawing { get { return r_isMoving || r_isDragging || r_isSelectingSector; } }


        // Part of hacky solution to handle KeyUp and KeyDown events for the picturebox
        private void pictureBox1_GotFocus(object sender, EventArgs e) { _mapFocused = true; }
        private void pictureBox1_LostFocus(object sender, EventArgs e) { _mapFocused = false; }


        // Events
        [Browsable(true)]
        [Category("Action"), Description("Raised upon a valid sector being selected")]
        public event EventHandler<MinimapSectorSelectedEventArgs> SectorSelected;

        [Browsable(true)]
        [Category("Action"), Description("Raised upon map redrawing")]
        public event EventHandler Redraw;

        protected virtual void OnSectorSelected(MinimapSectorSelectedEventArgs e)
        {
            SectorSelected?.Invoke(this, e);
        }



        public Control_MinimapViewer() {
            InitializeComponent();

            MapMarkerPositions = new List<Tuple<Point, int>>();
            MapMarkers = new List<MapMarker>();
            HighlightedAreas = new List<HighlightedArea>();

            label1.BackColor = background;
            pictureBox1.BackColor = background;

            Framerate = c_framerate;
            pictureBox1.MouseWheel += pictureBox1_MouseWheel;

            s_firstSectorImagePoint = Form_Main._miniMap.MapPosToImgPos(new Point(Form_Main._miniMap.firstSector.X * 32, Form_Main._miniMap.firstSector.Y * 32));
            s_allSectorPixels = new Size(Form_Main._miniMap.allSectorSize.Width * 32, Form_Main._miniMap.allSectorSize.Height * 32);
            s_lastSectorImagePoint = new Point(s_firstSectorImagePoint.X + s_allSectorPixels.Width, s_firstSectorImagePoint.Y + s_allSectorPixels.Height);

            pictureBox1.GotFocus += pictureBox1_GotFocus;
            pictureBox1.LostFocus += pictureBox1_LostFocus;

            GetMarkers();

        }

        private void GetMarkers()
        {
            MapMarkerPositions.Clear();
            for (int i = 0; i < DataReader.Npcs.Count; i++)
            {
                var n = DataReader.Npcs[i];
                if (n.Position.Z == p_Layer)
                    MapMarkerPositions.Add(new Tuple<Point, int>(new Point(n.Position.X, n.Position.Y), i));
            }
        }

        private void DrawMapMarkers()
        {
            // This could be replaced with a K-D tree for better performance


            // Wow, I really need to make the minimap static...
            var topLeft = Form_Main._miniMap.ImgPosToMapPos(mouseToPos(new Point(0, 0)));
            var bottomLeft = Form_Main._miniMap.ImgPosToMapPos(mouseToPos(new Point(0, pictureBox1.Height)));
            var topRight = Form_Main._miniMap.ImgPosToMapPos(mouseToPos(new Point(pictureBox1.Width, 0)));

            ClearMapMarkers();

            pictureBox1.SuspendLayout();

            foreach (var m in MapMarkerPositions)
            {
                // Console.WriteLine(m.Item1.ToString());

                if (m.Item1.X >= topLeft.X &&
                    m.Item1.X <= topRight.X &&
                    m.Item1.Y >= topLeft.Y &&
                    m.Item1.Y <= bottomLeft.Y)
                {
                    Point[] tv = new Point[] { Form_Main._miniMap.MapPosToImgPos(m.Item1) };
                    transform.TransformPoints(tv);

                    var iconSize = 16;

                    var loc = new Point(
                        tv[0].X - (iconSize / 2),
                        tv[0].Y - (iconSize / 2)
                    );

                    MapMarker marker = new MapMarker(m.Item2);
                    MapMarkers.Add(marker);

                    pictureBox1.Controls.Add(marker);

                    marker.Location = loc;
                    marker.BringToFront();


                }
                pictureBox1.ResumeLayout();
            }

        }

        private void ClearMapMarkers()
        {
            for (int i = pictureBox1.Controls.Count - 1; i >= 0; --i)
                pictureBox1.Controls[i].Dispose();
        }



        private void Control_MinimapViewer_Load(object sender, EventArgs e)
        {
            if (DesignMode)
                return;

            if (Form_Main._miniMap.Initialized)
            {
                this.p_Transform = new PointF(Form_Main._miniMap.TotalBitmapSize.Width / 2, Form_Main._miniMap.TotalBitmapSize.Height / 2);
                SLoad();
            }
            MapFileCheck();
        }

        private void Control_Minimap_Resize(object sender, EventArgs e)
        {
            p_Transform = mouseToPos(new PointF(pictureBox1.Width / 2, pictureBox1.Height / 2));
            UpdateLoadPromptLocation();
        }


        public void SetHighlightedAreas(List<HighlightedArea> areas)
        {
            HighlightedAreas = areas;
            pictureBox1.Invalidate();
        }


        public void SetHighlightedAreas(HighlightedArea area)
        {
            var areas = new List<HighlightedArea>();
            areas.Add(area);
            SetHighlightedAreas(areas);
        }

        public void SLoad()
        {
            if (Form_Main._miniMap.Initialized)
            {
                var path = $"{Minimap.SavePath}\\{this.p_Layer}.png";

                if (File.Exists(path))
                {
                    var imgHandle = this.Image;
                    this.Image = new Bitmap(path);

                    if (imgHandle != null)
                        imgHandle.Dispose(); // Seeing the profiler RAM after this makes me a happy boy

                    ZoomPosition(p_Transform, this.c_zoomScale);

                    pictureBox1.Invalidate();
                    GetMarkers();
                    DrawMapMarkers();
                }
            }
            MapFileCheck();
        }

        public void Unload()
        {
            if (Image != null)
                this.Image.Dispose();
        }


        public void SetLayer(int layer)
        {
            if (layer > 15 || layer < 0)
                return;

            this.p_Layer = layer;
            this.SLoad();
        }
        public void SetZoom(float zoom)
        {
            if (DesignMode)
                return;

            c_zoomScale = zoom;
            ZoomPosition(p_Transform, zoom);
        }


        #region Drawing

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (!r_isDragging)
            {
                var k_moveViewport = new PointF();

                if (k_moveDown || k_moveLeft || k_moveRight || k_moveUp)
                {
                    var step = c_keyMoveScale / c_zoomScale * timer1.Interval;

                    if (k_moveUp) k_moveViewport.Y -= step;
                    if (k_moveDown) k_moveViewport.Y += step;
                    if (k_moveLeft) k_moveViewport.X -= step;
                    if (k_moveRight) k_moveViewport.X += step;
                }

                if (k_moveViewport.X != 0 || k_moveViewport.Y != 0)
                    ZoomPosition(new PointF(p_Transform.X + k_moveViewport.X, p_Transform.Y + k_moveViewport.Y), c_zoomScale);
            }

            pictureBox1.Invalidate();
        }
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.InterpolationMode = InterpolationMode.NearestNeighbor;
            g.PixelOffsetMode = PixelOffsetMode.Half;

            if (this.Image != null)
            {
                g.Transform = transform;

                var ch_x = (int)p_Transform.X;
                var ch_y = (int)p_Transform.Y;

                if (ch_x >= 0 && ch_x < Image.Width && ch_y >= 0 && ch_y < Image.Height)
                {
                    if (_lastCrosshair.X != -1)
                    {
                        Image.SetPixel(_lastCrosshair.X, _lastCrosshair.Y, _lastCrosshairColor);
                    }

                    _lastCrosshairColor = Image.GetPixel(ch_x, ch_y);
                    _lastCrosshair.X = ch_x;
                    _lastCrosshair.Y = ch_y;

                    Image.SetPixel(ch_x, ch_y, Color.FromArgb(150, 255, 0, 0));

                }

                e.Graphics.DrawImage(Image, new Point(0, 0));

                // Highlighting sector
                if (r_isSelectingSector)
                    using (SolidBrush brush = new SolidBrush(Color.FromArgb(100, 255, 255, 255)))
                        e.Graphics.FillRectangle(brush, new Rectangle(
                            s_firstSectorImagePoint.X + s_lastSector.X * 32,
                            s_firstSectorImagePoint.Y + s_lastSector.Y * 32,
                            32, 32
                            ));



                if (HighlightedAreas.Count != 0)
                    foreach (var area in HighlightedAreas)
                        using (SolidBrush brush = new SolidBrush(Color.FromArgb(150, 255, 255, 255)))
                            if (area.Position.Z == p_Layer)
                            {
                                e.Graphics.FillRectangle(brush, new Rectangle(Form_Main._miniMap.MapPosToImgPos(
                                    new Point(
                                        area.Position.X - area.Size.Width,
                                        area.Position.Y - area.Size.Height)),
                                    new Size(
                                        1+area.Size.Width*2,
                                        1+area.Size.Height *2
                                        )
                                    ));
                            }

                e.Graphics.Transform.Invert();
                e.Graphics.DrawImage(Properties.Resources._16save, 25,25);

                Redraw?.Invoke(this, EventArgs.Empty);
            }

        }

        #endregion


        #region UI

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show($"- Browse to your Tibiantis\\map directory{Environment.NewLine}" +
                $"- Select any *.map file inside",
                "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

            using (var fbd = new OpenFileDialog())
            {
                fbd.Title = "Find Tibia map folder";
                fbd.Filter = "Minimap file|*.map";

                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.FileName) && Path.GetExtension(fbd.FileName) == ".map")
                {
                    var path = Path.GetDirectoryName(fbd.FileName);

                    Form_Main._miniMap.ReadMapData(path);
                    Form_Main._miniMap.SaveXML(Minimap.SavePath + "\\map.xml");

                    var frm = new Form_MinimapParseDialog(path, Minimap.SavePath).ShowDialog();

                    this.p_Transform = new PointF(Form_Main._miniMap.TotalBitmapSize.Width / 2, Form_Main._miniMap.TotalBitmapSize.Height / 2);
                    SLoad();
                }
            }
        }
        private void UpdateLoadPromptLocation()
        {
            var labelMargin = 20;

            // Just centers the label and button that shows when map files are not found
            button1.Location =
                new Point(
                    (this.Width / 2) - (button1.Width / 2),
                    (this.Height / 2) - (button1.Height / 2)
                );

            label1.Location = new Point(0, button1.Location.Y - button1.Height - labelMargin);
            label1.Width = pictureBox1.Width; // Dunno why it doesn't autosize when it has been anchored to the right edge, guess we fix manually
        }
        private void MapFileCheck()
        {
            if (!Form_Main._miniMap.Initialized)
            {
                label1.Visible = button1.Visible = true;
                if (this.Image != null) {
                    var imgHandle = this.Image;
                    this.Image = null;
                    imgHandle.Dispose();
                }
            }
            else
                label1.Visible = button1.Visible = false;

            UpdateLoadPromptLocation();
        }


        #endregion


        #region Keyboard Input

        // Because the picturebox class doesn't have KeyUp and KeyDown events I just forward them from Form_Main.cs if picturebox has focus
        public void pictureBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control)
                r_isSelectingSector = timer1.Enabled = true;

            switch (e.KeyCode)
            {
                case Keys.Add:
                    ZoomScroll(true);
                    break;
                case Keys.Subtract:
                    ZoomScroll(false);
                    break;


                case Keys.PageUp:
                    SetLayer(p_Layer - 1);
                    break;
                case Keys.PageDown:
                    SetLayer(p_Layer + 1);
                    break;


                case Keys.R:
                    if (e.Shift)
                        SetLayer(p_Layer - 1);
                    else
                        ZoomScroll(true);
                    break;
                case Keys.F:
                    if (e.Shift)
                        SetLayer(p_Layer + 1);
                    else
                        ZoomScroll(false);
                    break;


                case Keys.Up:
                case Keys.W:
                    k_moveUp = true;
                    break;
                case Keys.Down:
                case Keys.S:
                    k_moveDown = true;
                    break;
                case Keys.Left:
                case Keys.A:
                    k_moveLeft = true;
                    break;
                case Keys.Right:
                case Keys.D:
                    k_moveRight = true;
                    break;
            }

            if (IsDrawing)
                timer1.Enabled = true;
        }
        public void pictureBox1_KeyUp(object sender, KeyEventArgs e)
        {

            if (!e.Control && r_isSelectingSector)
                r_isSelectingSector = false;

            switch (e.KeyCode)
            {
                case Keys.Up:
                case Keys.W:
                    k_moveUp = false;
                    break;
                case Keys.Down:
                case Keys.S:
                    k_moveDown = false;
                    break;
                case Keys.Left:
                case Keys.A:
                    k_moveLeft = false;
                    break;
                case Keys.Right:
                case Keys.D:
                    k_moveRight = false;
                    break;
            }

            if (!IsDrawing)
            {
                timer1.Stop();
                pictureBox1.Invalidate();
            }

        }
        private void pictureBox1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            // PictureBox eats arrow key inputs because it's dumb. Send them over manually, again...
            // I'm not sure what Microsoft thinks they're solving by not having KeyDown/KeyUp????????

            switch (e.KeyCode)
            {
                case Keys.Up:
                case Keys.Down:
                case Keys.Left:
                case Keys.Right:
                    pictureBox1_KeyDown(sender, new KeyEventArgs(e.KeyData));
                    break;
            }
        }

        bool k_moveUp, k_moveDown, k_moveLeft, k_moveRight;

        #endregion

        #region Mouse Input

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            pictureBox1.Focus();

            if (Image == null)
                return;

            if (e.Button == MouseButtons.Left)
            {

                if (DragControlEnabled)
                {
                    if (r_isSelectingSector)
                    {
                        var args = new MinimapSectorSelectedEventArgs();
                        args.Sector = s_lastSector.X + (s_lastSector.Y * Form_Main._miniMap.allSectorSize.Width) + 1;
                        OnSectorSelected(args);
                    }

                    r_isDragging = true;
                    _lastMousePos = e.Location;

                    var p = mouseToPos(e.Location);

                    var t = Form_Main._miniMap.ImgPosToMapPos(p);

                    timer1.Start();
                } else
                {
                    var p = mouseToPos(e.Location);

                    var t = Form_Main._miniMap.ImgPosToMapPos(p);

                    // Click to move instead of smooth dragging because WinForms performance is awful with this, used like 17% of my CPU..
                    // I also want to keep it simple and not include stuff like OpenGL or DirectX, so this is it.
                    ZoomPosition(p, c_zoomScale);
                    FixViewport();
                    pictureBox1.Invalidate();
                }


            }
            else if (e.Button == MouseButtons.Right)
            {
                // Dropdown shenanigans go here

                var ctx = new ContextMenu();

                ctx.MenuItems.Add(new MenuItem("Placeholder"));

                ctx.Show(pictureBox1, e.Location);
            }
        }
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (Image == null)
                return;

            if (r_isSelectingSector)
            {
                var truePos = mouseToPos(e.Location);

                if (truePos.X > s_firstSectorImagePoint.X && truePos.X < s_lastSectorImagePoint.X &&
                    truePos.Y > s_firstSectorImagePoint.Y && truePos.Y < s_lastSectorImagePoint.Y)
                {
                    var adjust_X = truePos.X - s_firstSectorImagePoint.X;
                    var adjust_Y = truePos.Y - s_firstSectorImagePoint.Y;

                    var sec_X = (int)Math.Floor((double)adjust_X / 32);
                    var sec_Y = (int)Math.Floor((double)adjust_Y / 32);

                    if (sec_X != s_lastSector.X || sec_Y != s_lastSector.Y)
                    {
                        var sec = sec_X + (sec_Y * Form_Main._miniMap.allSectorSize.Width) + 1;
                        s_lastSector.X = sec_X;
                        s_lastSector.Y = sec_Y;
                    }
                }
            }



            if (r_isDragging)
            {
                timer_markerDrawTimer.Stop();

                if (MapMarkers.Count != 0)
                    ClearMapMarkers();

                var xx = (_lastMousePos.X - e.X) / c_zoomScale;
                var yy = (_lastMousePos.Y - e.Y) / c_zoomScale;


                p_Transform.X += xx;
                p_Transform.Y += yy;

                _lastMousePos = e.Location;

                transform.Translate(-xx, -yy);
            }
        }
        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (Image == null)
                return;

            if (r_isDragging)
            {

                if (!IsDrawing)
                    timer1.Stop();

                r_isDragging = false;

                FixViewport();
                timer_markerDrawTimer.Start();

                pictureBox1.Invalidate();

            }


        }
        private void pictureBox1_MouseWheel(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            pictureBox1.Focus();
            if (pictureBox1.Focused == true && e.Delta != 0)
                ZoomScroll(e.Delta > 0);
        }

        #endregion


        #region Transform stuff
        public void CenterToPoint(Point point, int layer)
        {
            // Takes actual Tibia coordinates

            if (!Form_Main._miniMap.Initialized)
                return;

            if (layer != this.p_Layer)
                SetLayer(layer);

            ZoomPosition(Form_Main._miniMap.MapPosToImgPos(point), c_zoomScale);
        }

        private void FixViewport()
        {
            // Keep viewport locked within image

            var topLeft = mouseToPos(new Point(0, 0));
            var bottomLeft = mouseToPos(new Point(0, pictureBox1.Height));
            var topRight = mouseToPos(new Point(pictureBox1.Width, 0));

            float margin_X = 0;
            float margin_Y = 0;

            float fix_X = 0;
            float fix_Y = 0;

            if (c_zoomScale < 1)
                margin_X = margin_Y = 256;

            if (pictureBox1.Width > (Image.Width * c_zoomScale)) margin_X += pictureBox1.Width - (Image.Width * c_zoomScale);
            if (pictureBox1.Height > (Image.Height * c_zoomScale)) margin_Y += pictureBox1.Height - (Image.Height * c_zoomScale);

            if (topLeft.X < -margin_X) fix_X += topLeft.X + margin_X;
            if (topRight.X > Image.Width + margin_X) fix_X += topRight.X - (Image.Width + margin_X);


            if (topLeft.Y < -margin_Y) fix_Y += topLeft.Y + margin_Y;
            if (bottomLeft.Y > Image.Height + margin_Y) fix_Y += bottomLeft.Y - (Image.Height + margin_Y);


            transform.Translate(fix_X, fix_Y);
            p_Transform.X -= fix_X;
            p_Transform.Y -= fix_Y;
        }
        private void ZoomPosition(PointF point, float zoom = 1f)
        {
            timer_markerDrawTimer.Stop();

            p_Transform = point;
            c_zoomScale = zoom;

            float pw = pictureBox1.Width / 2;
            float ph = pictureBox1.Height / 2;

            float dx = p_Transform.X * c_zoomScale;
            float dy = p_Transform.Y * c_zoomScale;

            transform.Reset();

            transform.Translate(-dx, -dy);
            transform.Translate(pw, ph);

            transform.Scale(c_zoomScale, c_zoomScale);


            FixViewport();
            pictureBox1.Invalidate();


            if (MapMarkers.Count != 0)
            {
                ClearMapMarkers();
                timer_markerDrawTimer.Enabled = true;
            }
        }

        private void timer_markerDrawTimer_Tick(object sender, EventArgs e)
        {
            DrawMapMarkers();
            timer_markerDrawTimer.Enabled = false;
        }

        private void ZoomScroll(bool zoomIn)
        {
            var newScale = Math.Min(Math.Max(
                (c_zoomScale + (zoomIn ? c_scrollValue : -c_scrollValue)),
                c_zoomMin), c_zoomMax);
            if (c_zoomScale != newScale)
                ZoomPosition(p_Transform, newScale);
        }

        private Point mouseToPos(PointF mousePoint)
        {
            PointF[] tv = new PointF[] { mousePoint };

            Matrix transformRev = transform.Clone();
            transformRev.Invert();

            transformRev.TransformPoints(tv);

            return new Point { X = (int)Math.Floor(tv[0].X), Y = (int)Math.Floor(tv[0].Y) };
        }
        #endregion



        public class MapMarker : Button
        {
            int npc = -1;

            public MapMarker(int npc)
            {
                this.npc = npc;

                this.TabStop = false;
                this.FlatStyle = FlatStyle.Flat;
                this.FlatAppearance.BorderSize = 0;
                this.FlatAppearance.MouseDownBackColor = Color.FromArgb(100, 150, 150, 150);
                this.FlatAppearance.MouseOverBackColor = Color.FromArgb(100, 255, 255, 255);
                this.FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255);
                this.BackColor = Color.FromArgb(0, 0, 0, 0);

                this.Size = new Size(16, 16);

                string tipVoc = "NPC";

                this.Image = Properties.Resources.map_helmet;
                if (DataReader.Npcs[npc].Vendor)
                {
                    this.Image = Properties.Resources.map_coin;
                    tipVoc = "Vendor";
                }
                if (DataReader.Npcs[npc].IsTeacher)
                {
                    this.Image = Properties.Resources.map_book;
                    tipVoc = "Teacher";
                    if (DataReader.Npcs[npc].Vendor)
                        tipVoc += "/Vendor";
                }

                string tipText = DataReader.Npcs[npc].Name + " (" + DataReader.Npcs[npc].Location.Name + ")" +
                    Environment.NewLine +
                    Environment.NewLine +
                    tipVoc +
                    Environment.NewLine +
                    DataReader.Npcs[npc].Position.X + "," + DataReader.Npcs[npc].Position.Y + "," + DataReader.Npcs[npc].Position.Z;

                ToolTip tip = new ToolTip();
                tip.SetToolTip(this, tipText);
                tip.AutomaticDelay = 0;

            }

            public Point GetClientPoint(Matrix transform)
            {
                var markerPos = new Point(DataReader.Npcs[npc].Position.X, DataReader.Npcs[npc].Position.Y);

                Point[] tv = new Point[] { Form_Main._miniMap.MapPosToImgPos(markerPos) };
                transform.TransformPoints(tv);

                var iconSize = 16;

                var loc = new Point(
                    tv[0].X - (iconSize / 2),
                    tv[0].Y - (iconSize / 2)
                );

                return loc;
        }
    
    }


    }


    public class HighlightedArea {
        public DataReader.Point3D Position;
        public Size Size;
    }

    public class MinimapSectorSelectedEventArgs : EventArgs
    {
        public int Sector { get; set; }
    }

}
