using BrightIdeasSoftware;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Media;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using TibiantisHelper.Properties;
using TibiantisHelper.Tabs;
using static TibiantisHelper.DataReader;
using static TibiantisHelper.Utility;

namespace TibiantisHelper
{
    public partial class Form_Main : Form
    {

        public static Form_Main Form; // Just a lazy way for user components to access stuff


        public static List<Control_Timer> _timers;
        public static List<Vocation> _vocations;
        public static List<Player> _currentlyOnline;

        public static SelectedVocation _selectedVocation;

        public static Minimap _miniMap = new Minimap(); 

        public DataReader _dataReader;

        static string file_accounts = "Accounts.xml";
        static string file_loginAlert = "TrackedPlayers.xml";

        public static TraybarContainer _traybarContainer;

        static string webpage_whoIsOnline = "https://tibiantis.online/?page=WhoIsOnline";

        public static Color mainBackcolor = Color.FromArgb(251, 234, 208);

        public Form_Main()
        {
            Form = this;

            _timers = new List<Control_Timer>();
            _vocations = new List<Vocation>();
            _dataReader = new DataReader();
            _currentlyOnline = new List<Player>();

            if (Settings.Default.UpgradeRequired)
            {
                Settings.Default.Upgrade();
                Settings.Default.UpgradeRequired = false;
                Settings.Default.Save();
            }


            InitializeComponent();

            PostInitialize();

            // control_MinimapViewer1.Minimap = _miniMap;

            this.BackColor = mainBackcolor;

            // Maybe move on to having all 7.72 data files embedded at some point:

            // Assembly assem = this.GetType().Assembly;
            // var resources = assem.GetManifestResourceNames();

            // foreach (var r in resources) Console.WriteLine(r);


        }

        private void PostInitialize()
        {

            _traybarContainer.flashTimer = timerTrayFlash;
            _traybarContainer.notifyIcon = notifyIcon1;
            _traybarContainer.notifyIcon.Icon = Resources.IconTrayGreen;

            InitializeVocations();


            Tray_BuildContextMenu();



            // Vocation
            header_vocation_comboBox.SelectedIndex = Settings.Default.Vocation;
            header_vocation_promo_checkBox.Checked = Settings.Default.Promotion;


        }



        #region Form

        private async void Form1_Load(object sender, EventArgs e)
        {
            await GetNetworkStuff();
        }
        
        // Minimize behaviour here
        private void Form1_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                if (Settings.Default.MinimizeToTray)
                {
                    if (Settings.Default.NotifOnMinimizeToTray)
                    {
                        _traybarContainer.notifyIcon.BalloonTipTitle = "Tibiantis Helper";
                        _traybarContainer.notifyIcon.BalloonTipIcon = ToolTipIcon.Info;
                        _traybarContainer.notifyIcon.BalloonTipText = $"Minimized to taskbar{Environment.NewLine}{Environment.NewLine}" +
                            "(Click this if you don't want to see this message anymore)";

                        Tray_ShowBubble(TrayBubbleBehaviour.MuteMinimize, 5000);
                    }
                    this.ShowInTaskbar = false;
                }

            }
            else if (this.WindowState == FormWindowState.Normal)
            {
                // Refresh sidebar in Accounts tab
                tab_Accounts.UpdateSelectedInfo();
                this.ShowInTaskbar = true;
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if ( Settings.Default.MinimizeOnExit )
            {
                e.Cancel = true;
                this.WindowState = FormWindowState.Minimized;
            }

        }

        // Suspend reworking control layouts while resizing, huge lag otherwise
        protected override void OnResizeBegin(EventArgs e)
        {
            SuspendLayout();
            base.OnResizeBegin(e);
        }
        protected override void OnResizeEnd(EventArgs e)
        {
            ResumeLayout();
            base.OnResizeEnd(e);
        }


        // Hacky solution to forward KeyUp and KeyDown events to minimap
        private void Form_Main_KeyDown(object sender, KeyEventArgs e)
        {
            if (((Tab_Map)tabPage_map.Controls[0]).control_MinimapViewer1 == null)
            {
                e.Handled = false;
                return;
            }

            if (((Tab_Map)tabPage_map.Controls[0]).control_MinimapViewer1.MapFocused)
            {
                ((Tab_Map)tabPage_map.Controls[0]).control_MinimapViewer1.pictureBox1_KeyDown(sender, e);
                e.Handled = true;
                return;
            }
        }
        private void Form_Main_KeyUp(object sender, KeyEventArgs e)
        {

            if (((Tab_Map)tabPage_map.Controls[0]).control_MinimapViewer1 == null)
            {
                e.Handled = false;
                return;
            }

            if (((Tab_Map)tabPage_map.Controls[0]).control_MinimapViewer1.MapFocused)
            {
                ((Tab_Map)tabPage_map.Controls[0]).control_MinimapViewer1.pictureBox1_KeyUp(sender, e);
                e.Handled = true;
                return;
            }
        }

        #endregion

        #region Traybar

        private static TrayBubbleBehaviour trayBubbleBehaviour = 0;

        public struct TraybarContainer
        {
            public NotifyIcon notifyIcon;
            public Timer flashTimer;
            public bool flashIsRed;
        }

        private void timerTrayFlash_Tick(object sender, EventArgs e)
        {
            if (_traybarContainer.flashIsRed)
            {
                _traybarContainer.notifyIcon.Icon = Resources.IconTrayGreen;
                _traybarContainer.flashIsRed = false;
            }
            else
            {
                _traybarContainer.notifyIcon.Icon = Resources.IconTrayRed;
                _traybarContainer.flashIsRed = true;
            }
        }


        private void notifyIcon1_BalloonTipClicked(object sender, EventArgs e)
        {
            if (trayBubbleBehaviour == TrayBubbleBehaviour.MuteMinimize)
            {
                Settings.Default.NotifOnMinimizeToTray = false;
                Settings.Default.Save();
            }
            else if (trayBubbleBehaviour == TrayBubbleBehaviour.Timer && Settings.Default.NotifTimerOpen)
            {
                tabControl1.SelectedTab = tabPage_timers;

                this.WindowState = FormWindowState.Normal;
                this.BringToFront();
                this.TopMost = true;
                this.Focus();
                this.TopMost = false;
            }
        }

        public static void Tray_ShowBubble(TrayBubbleBehaviour behaviour, int time)
        {
            trayBubbleBehaviour = behaviour;
            _traybarContainer.notifyIcon.ShowBalloonTip(time);
        }
        public static void Tray_ShowBubble(TrayBubbleBehaviour behaviour, int time, string title, string text, ToolTipIcon icon)
        {
            trayBubbleBehaviour = behaviour;
            _traybarContainer.notifyIcon.ShowBalloonTip(time, title, text, icon);
        }
        
        
        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.WindowState = FormWindowState.Normal;
                this.BringToFront();
                this.TopMost = true;
                this.Focus();
                this.TopMost = false;
            }
            else
                this.WindowState = FormWindowState.Minimized;
        }
        private void Tray_BuildContextMenu()
        {
            // NotifyIcon is not a control so can't use ContextMenu + MouseClick event
            // Instead we get this garbage where we use an associated placeholder context strip and populate it on Opening event
            // Actually could remove placeholder and construct it all here but I'm too tired and this works so

            tray_contextMenuStrip.Items.Clear();

            ToolStripItem accountDropdown = tray_contextMenuStrip.Items.Add("Accounts");

            if (Tab_Accounts._accounts.Count == 0) accountDropdown.Enabled = false;

            foreach (Tab_Accounts.Account acc in Tab_Accounts._accounts)
            {
                (accountDropdown as ToolStripMenuItem).DropDownItems.Add(acc.name, null, (s, e) => StringToClipboard(acc.login));
            }



            // ----- SETTINGS

            ToolStripItem settingsDropdown = tray_contextMenuStrip.Items.Add("Settings");

            var enableNetwork = ((ToolStripMenuItem)settingsDropdown).DropDownItems.Add("Enable Network");
            ToolStripMenuItem enableNetworkToolstrip = enableNetwork as ToolStripMenuItem;

            enableNetworkToolstrip.CheckOnClick = true;
            enableNetworkToolstrip.Checked = Settings.Default.EnableNetwork;
            enableNetworkToolstrip.CheckStateChanged += async (s, e) =>
            {
                if (!enableNetworkToolstrip.Checked)
                {
                    timerNetworkStuff.Stop();
                    notifyIcon1.Text = "Tibiantis Helper";
                    header_label_onlineStatus.Text = "Offline";
                    header_linkLabel_playersOnline.Text = "0 players";
                    Settings.Default.EnableNetwork = false;
                    Settings.Default.Save();
                }
                else
                {
                    timerNetworkStuff.Start();
                    Settings.Default.EnableNetwork = true;
                    Settings.Default.Save();
                    await GetNetworkStuff();
                }
            };


            var minimizeOnExit = ((ToolStripMenuItem)settingsDropdown).DropDownItems.Add("Minimize on Exit");
            ToolStripMenuItem minimizeOnExitToolstrip = minimizeOnExit as ToolStripMenuItem;

            minimizeOnExitToolstrip.CheckOnClick = true;
            minimizeOnExitToolstrip.Checked = Settings.Default.MinimizeOnExit;
            minimizeOnExitToolstrip.CheckStateChanged += (s, e) => { 
                Settings.Default.MinimizeOnExit = minimizeOnExitToolstrip.Checked;
                Settings.Default.Save();
            };


            var minimizeToTray = ((ToolStripMenuItem)settingsDropdown).DropDownItems.Add("Minimize to Tray");
            ToolStripMenuItem minimizeToTrayToolstrip = minimizeToTray as ToolStripMenuItem;

            minimizeToTrayToolstrip.CheckOnClick = true;
            minimizeToTrayToolstrip.Checked = Settings.Default.MinimizeToTray;
            minimizeToTrayToolstrip.CheckStateChanged += (s, e) => { 
                Settings.Default.MinimizeToTray = minimizeToTrayToolstrip.Checked;
                Settings.Default.Save();
            };



            // ----- UTILITY

            ToolStripItem extraDropdown = tray_contextMenuStrip.Items.Add("Utility");
            
            (extraDropdown as ToolStripMenuItem).DropDownItems.Add("Keycode Detector", null, (s, e) =>
            {
                var keyCodeDetector = new Form_KeyCodeViewer();
                keyCodeDetector.ShowDialog();
            });

            (extraDropdown as ToolStripMenuItem).DropDownItems.Add("Sprite Browser", null, (s, e) =>
            {
                var spriteBrowser = new Form_SpriteBrowser();

                spriteBrowser.StartPosition = FormStartPosition.CenterScreen;

                spriteBrowser.Show();
            });

            (extraDropdown as ToolStripMenuItem).DropDownItems.Add("Player Lookup", null, (s, e) =>
            {
                var playerLookup = new Form_PlayerLookup();

                playerLookup.StartPosition = FormStartPosition.CenterScreen;

                playerLookup.ShowDialog();
            });


            tray_contextMenuStrip.Items.Add("-");
            ToolStripItem exit = tray_contextMenuStrip.Items.Add("Exit", null, exitToolStripMenuItem_Click);
        }
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (var timer in _timers)
            {
                timer.TimerComponent.Stop();
            }

            
            SaveAllExit();
        }

        private void tray_contextMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            Tray_BuildContextMenu();
        }

        public enum TrayBubbleBehaviour
        {
            None,
            MuteMinimize,
            Timer
        }

        #endregion



        #region Network Components

        private async void timerNetworkStuff_Tick(object sender, EventArgs e)
        {
            await GetNetworkStuff();
        }

        private async Task GetNetworkStuff()
        {
            if (Settings.Default.EnableNetwork)
            {
                try
                {
                    await GetOnlineUsers();
                    tab_LoginAlert1.CheckOnline();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Failed something in network components" + Environment.NewLine + Environment.NewLine);
                    Console.WriteLine(ex.Message);
                }
            }

        }


        async Task GetOnlineUsers()
        {
            try
            { 

                HttpResponseMessage response = await webClient.GetAsync(webpage_whoIsOnline);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();

                _currentlyOnline.Clear();

                string line = "";

                StringReader strReader = new StringReader(responseBody);
                while (true)
                {
                    line = strReader.ReadLine();
                    if (line == null) break;
                    if (!string.IsNullOrEmpty(line))
                    {
                        if (line.Contains("class=\"tabi\""))
                        {
                            // Online users string, thankfully it's all in one line lol
                            string currentLine = line;

                            while (true)
                            {
                                var player = new Player();

                                string name, voc, level;
                                name = voc = level = "";

                                int ni = currentLine.IndexOf("&name=");

                                if (ni == -1) break;

                                currentLine = currentLine.Substring(ni);
                                name = GetBetweenChars(currentLine, '>', '<').Replace('+', ' ');

                                int vi = currentLine.IndexOf("</td>") + 5 + 3;
                                currentLine = currentLine.Substring(vi);
                                voc = GetBetweenChars(currentLine, '>', '<');
                                voc = CapitalizeString(voc);
                                string vocInitials = "";
                                foreach (char c in voc)
                                    if (char.IsUpper(c))
                                        vocInitials += c;


                                int li = currentLine.IndexOf("</td>") + 5 + 3;
                                currentLine = currentLine.Substring(li);
                                level = GetBetweenChars(currentLine, '>', '<');

                                player.Name = name;
                                player.Level = short.Parse(level);
                                player.Vocation = vocInitials;

                                _currentlyOnline.Add(player);
                            }
                            
                            

                            // Console.WriteLine("Parsed " + _currentlyOnline.Count + " usernames from website");

                        }
                        
                        if (line.Contains("'text-success'>Online"))
                        {
                            // Sets the "X players" header label
                            // Just in case online user string turns out to be unreliable
                            // (Like maybe if theres >200 users it'll have several pages, and currently we're only getting one page)

                            header_label_onlineStatus.Text = "Online";
                            int end = line.Length;
                            var index = line.IndexOf("<b>");
                            int i = 0;

                            foreach (var c in line.Substring(index + 3))
                            {
                                if (end == line.Length && c == '<')
                                    end = i;
                                i++;
                            }

                            if (!header_linkLabel_playersOnline.Visible)
                                header_linkLabel_playersOnline.Visible = true;

                            header_linkLabel_playersOnline.Text = line.Substring(index + 3, end) + " players";
                            notifyIcon1.Text = "Tibiantis Helper";
                            notifyIcon1.Text += Environment.NewLine + header_linkLabel_playersOnline.Text;
                        }
                        else if (line.Contains("'text-danger'>Offline"))
                        {
                            header_label_onlineStatus.Text = "Offline";

                            header_linkLabel_playersOnline.Text = "0 players";
                            notifyIcon1.Text = "Tibiantis Helper";
                            notifyIcon1.Text += Environment.NewLine + header_linkLabel_playersOnline.Text;

                            _currentlyOnline.Clear();
                        }

                    }
                }

            }
            catch (HttpRequestException e)
            {
                header_label_onlineStatus.Text = "Offline";

                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }
        }

        #endregion



        #region Header Links

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenInBrowser("https://tibiantis.online");
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenInBrowser("https://tibiantis.info");
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenInBrowser("https://discord.com/invite/YATGtun");
        }


        #endregion

        #region Players Online Form

        private void header_linkLabel_playersOnline_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            if (e.Button == MouseButtons.Right)
                OpenInBrowser(webpage_whoIsOnline);
            else
            {
                if (Settings.Default.EnableNetwork)
                {
                    Form_CurrentOnline form = new Form_CurrentOnline(_currentlyOnline);

                    form.Owner = this;

                    form.SetDesktopLocation(Cursor.Position.X, Cursor.Position.Y);
                    form.Show();
                }
                else
                    OpenInBrowser(webpage_whoIsOnline);
            }
        }

        // For the online players list
        public struct Player
        {
            public string Name;
            public short Level;
            public string Vocation;
        }

        #endregion
        
        #region Vocation
        private void comboBox_vocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            Settings.Default.Vocation = (byte)header_vocation_comboBox.SelectedIndex;
            UpdateVocation();
            tab_Calculators1.UpdateVocation();
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.Promotion = header_vocation_promo_checkBox.Checked;
            UpdateVocation();
        }
        private void InitializeVocations()
        {
            PopulateVocations();

            List<Image> vocationIcons = new List<Image>();

            foreach (Vocation v in _vocations)
                vocationIcons.Add(v.icon);

            ImageComboBox.DisplayImages(header_vocation_comboBox, vocationIcons);
            
        }
        private void PopulateVocations()
        {

            Vocation knight = new Vocation(
                    DataReader.ReadSprite(203),
                    "Elite", "Knight",
                    10, 15,
                    5, 5
                );

            Vocation paladin = new Vocation(
                    DataReader.ReadSprite(506),
                    "Royal", "Paladin",
                    7.5, 10,
                    7.5, 10
                );


            Vocation sorcerer = new Vocation(
                    DataReader.ReadSprite(1337),
                    "Master", "Sorcerer",
                    5, 5,
                    10, 15
                );



            Vocation druid = new Vocation(
                    DataReader.ReadSprite(1286),
                    "Elder", "Druid",
                    5, 5,
                    10, 15
                );

            _vocations.Add(knight);
            _vocations.Add(paladin);
            _vocations.Add(sorcerer);
            _vocations.Add(druid);

        }
        private void UpdateVocation()
        {
            header_vocation_promo_checkBox.Text = "";
            _selectedVocation.Name = "";

            Settings.Default.Save();

            if (header_vocation_promo_checkBox.Checked) _selectedVocation.Name = _vocations[header_vocation_comboBox.SelectedIndex].promoPrefix + " ";
            _selectedVocation.Name += _vocations[header_vocation_comboBox.SelectedIndex].name;

            header_vocation_promo_checkBox.Text = _selectedVocation.Name;


            if (header_vocation_promo_checkBox.Checked)
            {
                _selectedVocation.regenHP = _vocations[header_vocation_comboBox.SelectedIndex].regenHP_promo;
                _selectedVocation.regenMP = _vocations[header_vocation_comboBox.SelectedIndex].regenMP_promo;
            }
            else
            {
                _selectedVocation.regenHP = _vocations[header_vocation_comboBox.SelectedIndex].regenHP;
                _selectedVocation.regenMP = _vocations[header_vocation_comboBox.SelectedIndex].regenMP;
            }

            header_vocation_label2.Text = _selectedVocation.regenHP.ToString();
            header_vocation_label4.Text = _selectedVocation.regenMP.ToString();

            header_vocation_label2.Text += "/min";
            header_vocation_label4.Text += "/min";

            // FIX B
            // Calculator_Production.CalculateProduction();
        }

        public class Vocation
        {

            public double regenHP, regenHP_promo; // Per minute
            public double regenMP, regenMP_promo;

            public string name;
            public string promoPrefix;

            public Image icon;

            public Vocation(Image icon, string promoPrefix, string name, double regenHP, double regenHP_promo, double regenMP, double regenMP_promo)
            {
                this.icon = icon;

                this.promoPrefix = promoPrefix;
                this.name = name;

                this.regenHP = regenHP;
                this.regenHP_promo = regenHP_promo;
                this.regenMP = regenMP;
                this.regenMP_promo = regenMP_promo;
            }

        }

        public struct SelectedVocation
        {
            public string Name;
            public double regenHP;
            public double regenMP;
        }

        #endregion





        private void SaveAllExit()
        {
            try
            {
                Tab_Accounts.AccountsSave(file_accounts);
            }
            catch (Exception ex)
            {
                switch (MessageBox.Show(ex.Message, "Could not save Accounts data", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Error))
                {
                    case DialogResult.Abort:
                        return;
                    case DialogResult.Retry:
                        SaveAllExit();
                        break;
                }
            }

            try
            {
                tab_LoginAlert1.SaveGroups();
            }
            catch (Exception ex)
            {
                switch (MessageBox.Show(ex.Message, "Could not save Login Tracker data", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Error))
                {
                    case DialogResult.Abort:
                        return;
                    case DialogResult.Retry:
                        SaveAllExit();
                        break;
                }
            }

            tab_Raids1.SaveXML();

            _traybarContainer.notifyIcon.Visible = false;
            Settings.Default.Save();
            Environment.Exit(0);
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab.Text != "Map")
            {
                if (tab_Map1.control_MinimapViewer1 != null)
                    tab_Map1.control_MinimapViewer1.Unload();
            }
            else
                tab_Map1.control_MinimapViewer1.SLoad();

            if (tabControl1.SelectedTab.Text != "Raids")
            {
                if (tab_Raids1.control_MinimapViewer1 != null)
                    tab_Raids1.control_MinimapViewer1.Unload();
            }
            else
                tab_Raids1.control_MinimapViewer1.SLoad();


            if (tabControl1.SelectedTab.Text == "Accounts")
            {
                tab_Accounts.UpdateSelectedInfo();
            }
        }

        #region Drag & Drop

        private void Form_Main_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

            if (files[0] == null)
                return;

            string lwr = Path.GetExtension(files[0]).ToLowerInvariant();

            if (lwr == ".thg")
            {
                tabControl1.SelectedTab = tabPage_LoginAlert;

                var grp = Tabs.LoginAlert.Tab_LoginAlert.LoadGroups(files[0]);
                tab_LoginAlert1.Import(grp[0]);
            }
        }

        private void Form_Main_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
        }

        #endregion

    }

    public static class ImageComboBox
    {

        // Margins aroun owner drawn ComboBoxes.
        private const int MarginWidth = 4;
        private const int MarginHeight = 4;


        // Set up the ComboBox to display images.
        public static void DisplayImages(this ComboBox cbo, List<Image> images)
        {
            // Make the ComboBox owner-drawn.
            cbo.DrawMode = DrawMode.OwnerDrawVariable;

            // Add the images to the ComboBox's items.
            cbo.Items.Clear();
            foreach (Image image in images) cbo.Items.Add(image);

            // Subscribe to the DrawItem event.
            cbo.MeasureItem += cboDrawImage_MeasureItem;
            cbo.DrawItem += cboDrawImage_DrawItem;

        }

        // Measure a ComboBox item that is displaying an image.
        private static void cboDrawImage_MeasureItem(object sender, MeasureItemEventArgs e)
        {
            if (e.Index < 0) return;

            // Get this item's image.
            ComboBox cbo = sender as ComboBox;
            Image img = (Image)cbo.Items[e.Index];
            e.ItemHeight = img.Height + 2 * MarginHeight;
            e.ItemWidth = img.Width + 2 * MarginWidth;
        }

        // Draw a ComboBox item that is displaying an image.
        private static void cboDrawImage_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0) return;

            // Clear the background appropriately.
            //e.DrawBackground();

            // Draw the image.
            ComboBox cbo = sender as ComboBox;
            Image img = (Image)cbo.Items[e.Index];
            float hgt = img.Height;
            float scale = hgt / img.Height;
            float wid = img.Width;
            RectangleF rect = new RectangleF(
                e.Bounds.X + MarginWidth,
                e.Bounds.Y + MarginHeight,
                wid, hgt);
            e.Graphics.InterpolationMode = InterpolationMode.HighQualityBilinear;
            e.Graphics.DrawImage(img, rect);

            // Draw the focus rectangle if appropriate.
            // e.DrawFocusRectangle();
        }



    }

}
