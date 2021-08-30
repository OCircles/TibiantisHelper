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
using static TibiantisHelper.DataReader;
using static TibiantisHelper.Utility;

namespace TibiantisHelper
{
    public partial class Form_Main : Form
    {

        public static List<Account> _accounts;
        public static List<Control_Timer> _timers;
        public static List<Vocation> _vocations;
        public static List<TrackedPlayer> _trackedPlayers;
        public static List<string> _lastOnline; // Separate list for _trackedPlayers
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

            _accounts = new List<Account>();
            _timers = new List<Control_Timer>();
            _vocations = new List<Vocation>();
            _dataReader = new DataReader();
            _trackedPlayers = new List<TrackedPlayer>();
            _lastOnline = new List<string>();
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
            ReadAccounts(file_accounts);

            AccountsPopulate();
            if (_accounts.Count != 0)
                AccountsDisplayInfo(_accounts[0]);

            InitializeCalculatorTab();
            InitializeNpcTab();
            InitializeItemsTab();
            InitializeSpellsTab();
            InitializeMonsterTab();
            InitializeLoginAlert();


            Tray_BuildContextMenu();


            // App settings

            accounts_checkBox_hideAcc.Checked = Settings.Default.AccountsHideLogin;

            // Login Alert
            loginAlert_checkBox_notifTray.Checked = Settings.Default.LoginAlertNotifTray;
            loginAlert_checkBox_notifSound.Checked = Settings.Default.LoginAlertNotifSound;

            if (!string.IsNullOrEmpty(Settings.Default.LoginAlertNotifSoundPath))
                loginAlert_alertTextBox.Text = Path.GetFileName(Settings.Default.LoginAlertNotifSoundPath);

            // Vocation
            header_vocation_comboBox.SelectedIndex = Settings.Default.Vocation;
            header_vocation_promo_checkBox.Checked = Settings.Default.Promotion;

            // Timers
            checkBox1.Checked = Settings.Default.TimerNotifTray;
            checkBox2.Checked = Settings.Default.TimerNotifSound;
            checkBox3.Checked = Settings.Default.NotifTimerOpen;
            if (!string.IsNullOrEmpty(Settings.Default.TimerNotifSoundPath))
                textBox13.Text = Path.GetFileName(Settings.Default.TimerNotifSoundPath);

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
                var acc = _accounts.Where(a => a.name == accounts_label_selected.Text).FirstOrDefault();
                AccountsDisplayInfo(acc);

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

            if (_accounts.Count == 0) accountDropdown.Enabled = false;

            foreach (Account acc in _accounts)
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
                    LoginTrackerCheckOnline();
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
            Calculator_Production.UpdateProductionDropdown();
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
                    _dataReader.ReadSprite(203),
                    "Elite", "Knight",
                    10, 15,
                    5, 5
                );

            Vocation paladin = new Vocation(
                    _dataReader.ReadSprite(506),
                    "Royal", "Paladin",
                    7.5, 10,
                    7.5, 10
                );


            Vocation sorcerer = new Vocation(
                    _dataReader.ReadSprite(1337),
                    "Master", "Sorcerer",
                    5, 5,
                    10, 15
                );



            Vocation druid = new Vocation(
                    _dataReader.ReadSprite(1286),
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

            Calculator_Production.CalculateProduction();
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

        #region Accounts

        private void button6_Click(object sender, EventArgs e)
        {
            AccountAddDialog();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            AccountsRemoveSelected();
        }


        private void AccountsDisplaySelectedInfo()
        {
            if (accounts_dataGridView.SelectedCells.Count != 0)
            {
                var index = accounts_dataGridView.SelectedCells[0].RowIndex;
                var selectedName = (string)accounts_dataGridView.Rows[index].Cells[0].Value;

                var acc = _accounts.Where(a => a.name == (string)accounts_dataGridView.Rows[index].Cells[0].Value).FirstOrDefault();

                if (acc != null)
                {
                    AccountsDisplayInfo(acc);
                }
            }
        }
        private void AccountsDisplayInfo(Account acc)
        {
            if (acc != null)
            {
                textBox10.Text = $"Name: {acc.name}{Environment.NewLine}Account: {acc.login}";

                DateTime blank = new DateTime();
             
                if (acc.premium != blank || acc.house != blank) textBox10.Text += $"{Environment.NewLine}{Environment.NewLine}";

                if (acc.premium != blank) textBox10.Text += $"Premium ends in {(acc.premium - DateTime.Today).Days} days " +
                        $"({acc.premium.ToString("d", CultureInfo.CreateSpecificCulture("en-US"))}){Environment.NewLine}{Environment.NewLine}";


                if (acc.house != blank) textBox10.Text += $"House payment in {(acc.house - DateTime.Today).Days} days " +
                        $"({acc.house.ToString("d", CultureInfo.CreateSpecificCulture("en-US"))})";
            }

        }

        private void AccountAddDialog()
        {
            var diag = new Form_AccountAddDialog();

            if (diag.ShowDialog() == DialogResult.OK)
            {
                var acc = new Account();
                acc.name = diag.AccountName;
                acc.login = diag.AccountLogin;
                acc.premium = diag.AccountPremium;
                acc.house = diag.AccountHouse;

                _accounts.Add(acc);
                AccountsPopulate();
                AccountsSave(file_accounts);
            }
        }
        private void AccountsRemoveSelected()
        {
            if (accounts_dataGridView.SelectedCells.Count != 0)
            {
                var index = accounts_dataGridView.SelectedCells[0].RowIndex;
                var selectedName = (string)accounts_dataGridView.Rows[index].Cells[0].Value;

                var acc = _accounts.Where(a => a.name == (string)accounts_dataGridView.Rows[index].Cells[0].Value).FirstOrDefault();

                if (acc != null)
                {
                    if (MessageBox.Show(
                        $"Are you sure you want to remove {acc.name} from accounts?",
                        "Remove Account",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                    == DialogResult.Yes)
                    {
                        _accounts.Remove(acc);
                        AccountsPopulate();
                        AccountsSave(file_accounts);
                    }
                }
            }
        }

        private void accounts_dataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 2 || e.ColumnIndex == 3)
            {
                DateTime blank = new DateTime();
                DateTime actual = DateTime.Parse(e.Value.ToString());

                if (actual == blank)
                {
                    e.Value = "N/A";
                    accounts_dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.White;
                }
                else
                {
                    e.Value = actual.ToString("d", CultureInfo.CreateSpecificCulture("en-US"));

                    var diff = (actual - DateTime.Today).Days;

                    if (diff <= 3)
                        accounts_dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.PaleVioletRed;
                    else if (diff <= 7)
                        accounts_dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.LightYellow;
                    else
                        accounts_dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.White;
                }
            }
        }

        private void accounts_dataGridView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
                AccountsRemoveSelected();
        }

        private void accounts_dataGridView_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            DataGridView grid = (DataGridView)sender;

            grid.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            grid.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            grid.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            grid.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void accounts_dataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            accounts_dataGridView.BeginEdit(true);
        }

        private void accounts_dataGridView_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            e.Cancel = true;

            var acc = _accounts.Where(a => a.name == (string)accounts_dataGridView.Rows[e.RowIndex].Cells[0].Value).FirstOrDefault();

            if (acc != null)
            {
                var diag = new Form_AccountAddDialog(acc.name, acc.login, acc.premium, acc.house);

                if (diag.ShowDialog() == DialogResult.OK)
                {
                    acc.name = diag.AccountName;
                    acc.login = diag.AccountLogin;
                    acc.premium = diag.AccountPremium;
                    acc.house = diag.AccountHouse;

                    AccountsDisplayInfo(acc);

                    AccountsPopulate();
                    AccountsSave(file_accounts);
                }
            }

        }

        private void accounts_dataGridView_MouseDown(object sender, MouseEventArgs e)
        {

            DataGridView.HitTestInfo hit = accounts_dataGridView.HitTest(e.X, e.Y);
            if (hit.Type == DataGridViewHitTestType.Cell)
            {
                accounts_dataGridView.ClearSelection();

                var clickedCell = accounts_dataGridView.Rows[hit.RowIndex].Cells[hit.ColumnIndex];
                clickedCell.Selected = true;

                var accName = (string)accounts_dataGridView.Rows[clickedCell.RowIndex].Cells[0].Value;
                accounts_label_selected.Text = accName;

                var acc = _accounts.Where(a => a.name == accName).FirstOrDefault();

                if (acc != null)
                {

                    AccountsDisplayInfo(acc);

                    if (e.Button == MouseButtons.Right)
                    {

                        if (clickedCell.ColumnIndex == 1)
                            StringToClipboard(acc.login);
                        else
                        {
                            ContextMenu menu = new ContextMenu();

                            var add = new MenuItem("Add", (s, ee) => { AccountAddDialog(); });
                            var remove = new MenuItem("Remove", (s, ee) =>
                            {
                                _accounts.Remove(acc);
                                AccountsPopulate();
                                AccountsSave(file_accounts);
                            });

                            menu.MenuItems.Add(add);
                            menu.MenuItems.Add(remove);


                            DateTime blank = new DateTime();

                            if (acc.premium != blank || acc.house != blank)
                            {
                                var spacer = new MenuItem("-");
                                menu.MenuItems.Add(spacer);

                                if (acc.premium != blank)
                                {
                                    var premium = new MenuItem("Premium");
                                    var premiumAdd30 = new MenuItem("Add 30 days (piggybank)", (s, ee) =>
                                    {
                                        acc.premium = acc.premium.AddDays(30);
                                        AccountsPopulate();
                                        AccountsSave(file_accounts);
                                    });
                                    var premiumAdd33 = new MenuItem("Add 33 days (paypal)", (s, ee) =>
                                    {
                                        acc.premium = acc.premium.AddDays(33);
                                        AccountsPopulate();
                                        AccountsSave(file_accounts);
                                    });

                                    premium.MenuItems.Add(premiumAdd30);
                                    premium.MenuItems.Add(premiumAdd33);

                                    menu.MenuItems.Add(premium);
                                }

                                if (acc.house != blank)
                                {
                                    var house = new MenuItem("House");
                                    var houseAdd30 = new MenuItem("Add 30 days", (s, ee) =>
                                    {
                                        acc.house = acc.house.AddDays(30);
                                        AccountsPopulate();
                                        AccountsSave(file_accounts);
                                    });

                                    house.MenuItems.Add(houseAdd30);

                                    menu.MenuItems.Add(house);

                                }
                            }

                            menu.Show((Control)sender, new Point(e.X, e.Y));
                        }
                    }
                }
            }
        }



        private void accounts_checkBox_hideAcc_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.AccountsHideLogin = accounts_checkBox_hideAcc.Checked;
            Settings.Default.Save();

            AccountsPopulate();
        }



        private void AccountsPopulate()
        {

            DataTable table = new DataTable();

            table.TableName = "Player";

            table.Columns.AddRange(new DataColumn[] {
                new DataColumn("Name"),
                new DataColumn("Account"),
                new DataColumn("Premium Expiry"),
                new DataColumn("House Payment")
            });

            foreach (var a in _accounts)
            {
                var login = a.login;
                if (accounts_checkBox_hideAcc.Checked)
                    login = ProtectedString(a.login);

                table.Rows.Add(a.name, login, a.premium, a.house);
            }

            accounts_dataGridView.DataSource = table;
        }

        private string ProtectedString(string input)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder(input.Length);
            for (int i = 0; i < input.Length; i++)
                sb.Append('*');
            return sb.ToString();
        }

        private void AccountsSave(string path)
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;

            XmlWriter xmlWriter = XmlWriter.Create("Accounts.xml",settings);
            xmlWriter.WriteStartDocument();
            xmlWriter.WriteStartElement("Accounts");

            foreach (var a in _accounts)
            {
                xmlWriter.WriteStartElement("Account");

                xmlWriter.WriteStartElement("Name");
                xmlWriter.WriteString(a.name);
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement("Login");
                xmlWriter.WriteString(a.login);
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement("Premium");
                xmlWriter.WriteString(a.premium.ToString());
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement("HouseDate");
                xmlWriter.WriteString(a.house.ToString());
                xmlWriter.WriteEndElement();

                xmlWriter.WriteEndElement();
            }

            xmlWriter.WriteEndElement();
            xmlWriter.WriteEndDocument();

            xmlWriter.Close();

        }

        private void ReadAccounts(string path)
        {
            if (File.Exists(path))
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(path);

                foreach (XmlNode xmlNode in xmlDoc.DocumentElement)
                {

                    Account acc = new Account();
                    foreach (XmlNode accNodes in xmlNode.ChildNodes)
                    {
                        if (!String.IsNullOrEmpty(accNodes.InnerText))
                        {

                            switch (accNodes.Name)
                            {
                                case "Name":
                                    acc.name = accNodes.InnerText;
                                    break;
                                case "Account": // Legacy node name, need for migrating v0.96 and earlier xml:s
                                case "Login":
                                    acc.login = accNodes.InnerText;
                                    break;
                                case "Premium_x0020_Expiry": // Also legacy migration thing
                                case "Premium":
                                    acc.premium = DateTime.Parse(accNodes.InnerText);
                                    break;
                                case "House_x0020_Payment": // Also legacy migration thing
                                case "HouseDate":
                                    acc.house = DateTime.Parse(accNodes.InnerText);
                                    break;
                            }
                        }
                    }

                    if ( !string.IsNullOrEmpty(acc.name) && !string.IsNullOrEmpty(acc.login) )
                        _accounts.Add(acc);
                }
            }
        }
        public class Account
        {

            public string name;
            public string login;
            public DateTime premium;
            public DateTime house;

        }

        #endregion

        #region Timers

        private void TimersPopulate()
        {
            foreach (var timer in _timers)
            {
                timers_splitContainer.Panel2.Controls.Add(timer);
                timer.Dock = DockStyle.Top;
                timer.BackColor = timers_splitContainer.Panel1.BackColor;
            }
        }

        public void TimersAdd(string name, string time, int multiplier, bool autoRestart, bool trayFlash)
        {
            Control_Timer timer = new Control_Timer(name, time, multiplier, autoRestart, trayFlash, _traybarContainer);

            _timers.Add(timer);

            TimersPopulate();

            timer.deleteButton.Click += (s, ee) =>
            {
                timer.Stop();
                timers_splitContainer.Panel2.Controls.Remove(timer);
                timer.Dispose();
            };
        }

        private void button2_Click(object sender, EventArgs e)
        {

            Form_TimerDialog dialog = new Form_TimerDialog();

            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                TimersAdd(dialog.TimerName, dialog.Time, dialog.Multiplier, dialog.AutoRestart, dialog.TraybarFlash);
            }


        }
        private void timers_contextMenuStrip_Opening(object sender, CancelEventArgs e)
        {

            timers_contextMenuStrip.Items.Clear();

            var runes = timers_contextMenuStrip.Items.Add("Runes");
            var runesSingle = (runes as ToolStripMenuItem).DropDownItems.Add("Single");
            var runesBackpack = (runes as ToolStripMenuItem).DropDownItems.Add("Backpack");

            foreach (Rune r in _dataReader.runes)
            {

                Spell spell = _dataReader.spells.Where(s => s.Name == r.Name).FirstOrDefault();

                double regenSingle = (spell.Mana / _selectedVocation.regenMP) * 60;
                double regenBp = ((spell.Mana * 20) / _selectedVocation.regenMP) * 60;

                TimeSpan regenTime = TimeSpan.FromSeconds(regenSingle);

                var regenStringSingle = string.Format("{0:D2}:{1:D2}:{2:D2}", regenTime.Hours, regenTime.Minutes, regenTime.Seconds);

                regenTime = TimeSpan.FromSeconds(regenBp);

                var regenStringBp = string.Format("{0:D2}:{1:D2}:{2:D2}", regenTime.Hours, regenTime.Minutes, regenTime.Seconds);

                (runesSingle as ToolStripMenuItem).DropDownItems.Add(r.Name, null, (s, ee) =>
                    TimersAdd($"{r.Name} ({_selectedVocation.Name})",
                    regenStringSingle, 1, false, false));

                (runesBackpack as ToolStripMenuItem).DropDownItems.Add(r.Name, null, (s, ee) =>
                    TimersAdd($"BP of {r.Name} ({_selectedVocation.Name})",
                    regenStringBp, 1, false, false));

            }


            var conjuring = timers_contextMenuStrip.Items.Add("Conjuring");
            var conjuringSingle = (conjuring as ToolStripMenuItem).DropDownItems.Add("Stack");
            var conjuringBackpack = (conjuring as ToolStripMenuItem).DropDownItems.Add("Backpack");

            foreach (Spell s in _dataReader.spells.Where(s => s.ProduceAmount != 0))
            {

                Item item = _dataReader.items.Where(i => i.ID == s.ProduceID).FirstOrDefault();

                double castSingle = Math.Ceiling(((double)(1 * 100)) / (double)s.ProduceAmount);
                double castBp = Math.Ceiling(((double)(20 * 100)) / (double)s.ProduceAmount);


                double singleMana = castSingle * s.Mana;
                double regenSingle = (singleMana / _selectedVocation.regenMP) * 60;

                double bpMana = castBp * s.Mana;
                double regenBp = (bpMana / _selectedVocation.regenMP) * 60;

                TimeSpan regenTime = TimeSpan.FromSeconds(regenSingle);

                var regenStringSingle = string.Format("{0:D2}:{1:D2}:{2:D2}", regenTime.Hours, regenTime.Minutes, regenTime.Seconds);

                regenTime = TimeSpan.FromSeconds(regenBp);

                var regenStringBp = string.Format("{0:D2}:{1:D2}:{2:D2}", regenTime.Hours, regenTime.Minutes, regenTime.Seconds);


                (conjuringSingle as ToolStripMenuItem).DropDownItems.Add(item.Name, null, (ss, ee) =>
                    TimersAdd($"Stack of {item.Name} ({_selectedVocation.Name})",
                    regenStringSingle, 1, false, false));

                (conjuringBackpack as ToolStripMenuItem).DropDownItems.Add(item.Name, null, (ss, ee) =>
                    TimersAdd($"BP of {item.Name} ({_selectedVocation.Name})",
                    regenStringBp, 1, false, false));


            }

            timers_contextMenuStrip.Items.Add("-");

            timers_contextMenuStrip.Items.Add("Bedmage (01:40:00)", null, (ss, ee) => TimersAdd("Bedmage", "01:40:00", 1, false, true));

            timers_contextMenuStrip.Items.Add("Idle (00:15:00)", null, (ss, ee) => TimersAdd("Idle", "00:15:00", 1, true, false));

            timers_contextMenuStrip.Items.Add("Food (00:20:00)", null, (ss, ee) => TimersAdd("Food", "00:20:00", 1, false, false));
        }

        private void button5_MouseClick(object sender, MouseEventArgs e)
        {
            timers_contextMenuStrip.Show((Control)sender, new Point(e.X, e.Y));
        }

        private void button8_Click(object sender, EventArgs e)
        {
            var diag = new OpenFileDialog();
            diag.RestoreDirectory = true;

            diag.Filter = "WAV files (*.wav)|*.wav";

            if (diag.ShowDialog() == DialogResult.OK)
            {
                textBox13.Text = Path.GetFileName(diag.FileName);
                Settings.Default.TimerNotifSoundPath = diag.FileName;
                Settings.Default.Save();
            }
        }
        private void checkBox1_CheckedChanged_1(object sender, EventArgs e)
        {
            Settings.Default.TimerNotifTray = checkBox1.Checked;
            Settings.Default.Save();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.TimerNotifSound = checkBox2.Checked;
            Settings.Default.Save();
        }
        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.NotifTimerOpen = checkBox3.Checked;
            Settings.Default.Save();
        }

        #endregion

        #region Calculator

        Calculator_Production Calculator_Production;
        Calculator_Experience Calculator_Experience;


        private void InitializeCalculatorTab()
        {
            Calculator_Production = new Calculator_Production(this);
            calculator_addPage("Production", Calculator_Production);


            Calculator_Experience = new Calculator_Experience(this);
            calculator_addPage("Experience", Calculator_Experience);
        }
        
        private void calculator_addPage(string title, UserControl cc)
        {
            var page = new TabPage();
            page.BackColor = Color.White;
            page.Controls.Add(cc);
            cc.Dock = DockStyle.Fill;
            calculator_tabControl.TabPages.Add(page);
            calculator_listBox.Items.Add(title);
        }

        private void calculator_listBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            calculator_tabControl.SelectedIndex = calculator_listBox.SelectedIndex;
        }

        #endregion



        #region Monsters

        private void InitializeMonsterTab()
        {
            monsters_listView.UseFiltering = true;

            // Monster list

            // Summon mana
            olvColumn18.AspectToStringConverter = delegate (object x)
            {
                var s = (int)x;

                if (s == 0) return "N/A";
                return s + " MP";
            };

            monsters_listView.Objects = _dataReader.monsters;

            // Monster drop list
            olvColumn21.AspectGetter = delegate (object x) {
                Item item = _dataReader.items.Where(i => i.ID == ((Monster.Drop)x).ItemID).FirstOrDefault();
                if (item != null)
                    return item.Name;
                else
                    return "Removed post-7.4 item";
            };
            olvColumn22.AspectGetter = delegate (object x) { return ((Monster.Drop)x).Amount; };
            olvColumn23.AspectGetter = delegate (object x) { return ((double)(((Monster.Drop)x).Rate) / 10); };
            olvColumn23.AspectToStringFormat = "{0}%";
            
            olvColumn24.AspectGetter = delegate (object x)
            {
                int id = ((Monster.Drop)x).ItemID;
                int cheapest = 0;
                foreach (NPC npc in _dataReader.npcs)
                {
                    foreach (NPC.Transaction transaction in npc.transactions)
                    {
                        if (transaction.ItemID == id)
                            if (transaction.Type == NPC.TransactionType.Sell && transaction.Price > cheapest) cheapest = transaction.Price;
                    }
                }
                return cheapest;
            };
        }

        private void monsters_listView_SelectionChanged(object sender, EventArgs e)
        {

            if (monsters_listView.SelectedObject != null)
            {

                Monster selected = (Monster)monsters_listView.SelectedObject;
                monsters_labelName.Text = selected.Name; 
                monsters_dropListView.Objects = selected.Inventory;

                try
                {
                    monsters_textBox.Lines = File.ReadAllLines(monsterFolder + "\\" + selected.Filename);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "File read error");
                }
            }
        }

        private void monsters_dropListView_ItemActivate(object sender, EventArgs e)
        {
            var drop = (Monster.Drop)monsters_dropListView.SelectedObject;
            Item item = _dataReader.items.Where(i => i.ID == drop.ItemID).FirstOrDefault();

            if (item != null)
            {
                tabControl1.SelectedTab = tabPage_items;
                Items_DisplayInfo(item);
            }
        }


        private void monsters_checkBox_hideUniques_CheckedChanged(object sender, EventArgs e)
        {
            if (monsters_checkBox_hideUniques.Checked)
                monsters_listView.ModelFilter = new ModelFilter(delegate (object x) { return !string.IsNullOrEmpty(((Monster)x).Article); });
            else
                monsters_listView.ModelFilter = null;
        }

        private void monsters_checkBox_showSummonLevel_CheckedChanged(object sender, EventArgs e)
        {
            if (monsters_checkBox_showSummonLevel.Checked)
            {
                olvColumn18.AspectToStringConverter = delegate (object x)
                {
                    var s = (int)x;

                    if (s == 0) return "N/A";
                    return (Math.Ceiling(((decimal)s - 35) / 30) + 8).ToString();
                };
            } 
            else
            {
                olvColumn18.AspectToStringConverter = delegate (object x)
                {
                    var s = (int)x;

                    if (s == 0) return "N/A";
                    return s + " MP";
                };
            }
            monsters_listView.SetObjects(monsters_listView.Objects);
        }

        #endregion

        #region NPC

        private void InitializeNpcTab()
        {
            olvColumn1.AspectGetter = delegate (object x) { return ((NPC)x).Name; };
            olvColumn2.AspectGetter = delegate (object x) { return ((NPC)x).Location.Name; };
            olvColumn3.AspectGetter = delegate (object x) { return ((NPC)x).Vendor; };
            olvColumn4.AspectGetter = delegate (object x) { return ((NPC)x).IsTeacher; };

            olvColumn3.AspectToStringConverter =
            olvColumn4.AspectToStringConverter = delegate (object x)
            {
                if ((bool)x) return "Yes";
                return "No";
            };

            listView_npcs.Objects = _dataReader.npcs;


            // Items list

            listView_npcItems.SmallImageList = new ImageList();
            listView_npcItems.SmallImageList.ImageSize = new Size(32,32);

            olvColumn5.ImageGetter = delegate (object x) {
                var transaction = (NPC.Transaction)x;
                if (transaction.ItemID > 100)
                {
                    byte spriteIndex = 0;

                    Entity entity = _dataReader.ReadEntity(transaction.ItemID - 100);

                    // If it's a vial
                    if (transaction.ItemID == 2874)
                        spriteIndex = (Item_GetLiquidContainerName((NPC.Transaction)x)).spriteIndex;

                    return _dataReader.ReadSprite(entity.sprites[spriteIndex]);
                }
                else
                    return "Spell";
            };

            olvColumn5.AspectGetter = delegate (object x)
            {
                if (((NPC.Transaction)x).ItemID > 100)
                {
                    var item = _dataReader.items.Where(i => i.ID == ((NPC.Transaction)x).ItemID).FirstOrDefault();
                    if (item != null)
                    {
                        if (item.Flags.Contains("LiquidContainer"))
                        {
                            if (((NPC.Transaction)x).Data != 0)
                            {
                                var container = Item_GetLiquidContainerName((NPC.Transaction)x);
                                return $"{item.Name} ({container.liquid})";
                            }
;
                        }
                        return item.Name;
                    }
                    else
                        return "Removed post-7.4 item";
                }
                else
                {
                    Spell spell = _dataReader.spells.Where(s => s.ID == ((NPC.Transaction)x).ItemID).FirstOrDefault();
                    return spell.Name;
                }
            };

            olvColumn5.GroupKeyGetter = delegate (object x)
            {
                if (((NPC.Transaction)x).Type == NPC.TransactionType.Buy) return "Selling";
                else if (((NPC.Transaction)x).Type == NPC.TransactionType.Sell) return "Buying";
                else return "Teaching";
            };

            olvColumn6.AspectGetter = delegate (object x) { return ((NPC.Transaction)x).Price; };


        }
        private void PopulateNpcItemList(NPC npc)
        {
            if (npc != null)
            {
                List<NPC.Transaction> list = new List<NPC.Transaction>();

                list.AddRange(npc.GetTransactions(NPC.TransactionType.Buy));
                list.AddRange(npc.GetTransactions(NPC.TransactionType.Sell));
                list.AddRange(npc.GetTransactions(NPC.TransactionType.Spell));
                
                listView_npcItems.Objects = list;

                listView_npcItems.BuildList();
            }

        }
        private void listView_npcs_SelectionChanged(object sender, EventArgs e)
        {
            if (listView_npcs.SelectedObject != null)
            {
                label2.Text = (listView_npcs.SelectedObject as NPC).Name;
                PopulateNpcItemList((NPC)listView_npcs.SelectedObject);
            }
        } 
        private void listView_npcs_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var test = listView_npcs.HitTest(e.X, e.Y).Item;

                if (test != null)
                {
                    var npc = ((NPC)((OLVListItem)test).RowObject);

                    var menu = new ContextMenu();

                    var posD = new MenuItem("Location");
                    var posView = new MenuItem("View", (s, ee) => {
                        var pos = npc.Position;
                        OpenInBrowser("https://tibiantis.info/library/map#" + pos.X + "," + pos.Y + "," + pos.Z + ",8");
                    } );

                    var posClip = new MenuItem("To clipboard", (s, ee) =>
                    {
                        var pos = npc.Position;
                        StringToClipboard("https://tibiantis.info/library/map#" + pos.X + "," + pos.Y + "," + pos.Z + ",8");
                    } );


                    var file = new MenuItem("Open", (s, ee) => {
                        System.Diagnostics.Process.Start(npcFolder + "\\" + npc.Filename);
                        // Console.WriteLine(npcFolder + "\\" + npc.Filename);
                });

                 

                    posD.MenuItems.Add(posView);
                    posD.MenuItems.Add(posClip);

                    menu.MenuItems.Add(posD);
                    menu.MenuItems.Add(file); 

                    menu.Show(listView_npcs, new Point(e.X, e.Y));


                 
                } 
            }

            


        }

        private void listView_npcItems_ItemActivate(object sender, EventArgs e)
        {
            if (listView_npcItems.SelectedObject != null)
            {
                var trade = (NPC.Transaction)listView_npcItems.SelectedObject;

                if (trade.Type != NPC.TransactionType.Spell)
                {
                    var item = _dataReader.items.Where(i => i.ID == trade.ItemID).FirstOrDefault();

                    if (item != null)
                    {
                        tabControl1.SelectedTab = tabPage_items;
                        Items_DisplayInfo(item);
                    }
                } else
                {
                    Spell spell = _dataReader.spells.Where(s => s.ID == trade.ItemID).FirstOrDefault();
                    
                    tabControl1.SelectedTab = tabPage_spells;
                    spells_listView.SelectedObject = spell;
                }

            }
        }

        private void listView_npcs_ItemActivate(object sender, EventArgs e)
        {
            var npc = (NPC)listView_npcs.SelectedObject;

            var pos = npc.Position;
            OpenInBrowser("https://tibiantis.info/library/map#" + pos.X + "," + pos.Y + "," + pos.Z + ",8");
        }

        #endregion

        #region Items

        string itemSearch = "";

        private void InitializeItemsTab()
        {
            listView_items.UseFiltering = true;

            InitializeItemTrades();
            InitializeItemDrops();

            items_comboBox_itemCategory.SelectedIndex = 1;
        }

        #region Item Columns

        private OLVColumn itemCol_Attack()
        {
            var atk = new OLVColumn();
            atk.Text = "Attack";
            atk.AspectGetter = delegate (object x) { return ((Item)x).GetAttributeValue("WeaponAttackValue"); };

            return atk;
        }
        private OLVColumn itemCol_AttackRanged()
        {
            var atk = new OLVColumn();
            atk.Text = "Attack";
            atk.AspectGetter = delegate (object x) { return ((Item)x).GetAttributeValue("ThrowAttackValue"); };

            return atk;
        }
        private OLVColumn itemCol_Range()
        {
            var range = new OLVColumn();
            range.Text = "Range";
            range.AspectGetter = delegate (object x) {
                int rangeValue = ((Item)x).GetAttributeValue("BowRange");
                if (rangeValue == 0) rangeValue = ((Item)x).GetAttributeValue("ThrowRange");
                return rangeValue;
            };

            return range;
        }
        private OLVColumn itemCol_WeaponDefense()
        {
            var def = new OLVColumn();
            def.Text = "Defense";
            def.AspectGetter = delegate (object x) { return ((Item)x).GetAttributeValue("WeaponDefendValue"); };
            return def;
        }


        private OLVColumn itemCol_Arm()
        {
            var armor = new OLVColumn();
            armor.Text = "Arm";
            armor.AspectGetter = delegate (object x) { return ((Item)x).GetAttributeValue("ArmorValue"); };

            return armor;
        }
        private OLVColumn itemCol_ShieldDefense()
        {
            var def = new OLVColumn();
            def.Text = "Defense";
            def.AspectGetter = delegate (object x) { return ((Item)x).GetAttributeValue("ShieldDefendValue"); };
            return def;
        }
        private OLVColumn itemCol_SkillModifier()
        {
            var skillMod = new OLVColumn();
            skillMod.Text = "+Skill";
            skillMod.AspectGetter = delegate (object x) { return ((Item)x).GetAttributeValue("SkillModification"); };
            skillMod.AspectToStringConverter = delegate (object x)
            {
                var skill = (int)x;

                if (skill == 0) return "N/A";
                else
                    return skill.ToString();
            };

            return skillMod;
        }

        private OLVColumn itemCol_RuneCharges()
        {
            var charges = new OLVColumn();
            charges.Text = "Charges";
            charges.AspectGetter = delegate (object x)
            {
                Item i = (Item)x;

                Rune r = _dataReader.runes.Where(oo => oo.ID == i.ID).FirstOrDefault();
                if (string.IsNullOrEmpty(r.Name)) return (byte)0;
                else return r.Charges;

            };
            charges.AspectToStringConverter = delegate (object x)
            {
                var i = (byte)x;

                if (i == 0) return "N/A";
                else return i.ToString();
            };

            return charges;
        }


        private OLVColumn itemCol_ItemName()
        {
            OLVColumn itemName = new OLVColumn();
            itemName.Text = "Name";
            itemName.FillsFreeSpace = true;
            itemName.AspectGetter = delegate (object x)
            {
                Item i = (Item)x;

                if (i.Flags.Contains("Rune"))
                {
                    Rune r = _dataReader.runes.Where(oo => oo.ID == i.ID).FirstOrDefault();
                    if (string.IsNullOrEmpty(r.Name)) return "Spell Rune " + i.ID + " (unused)";
                    return r.Name;
                }
                else
                    return i.Name;
            };
            itemName.ImageGetter = delegate (object x)
            {
                var entity = _dataReader.ReadEntity(((Item)x).ID - 100);
                if (entity != null) return _dataReader.ReadSprite(entity.sprites[0]);
                else return null;
            };

            return itemName;
        }

        private OLVColumn itemCol_Weight()
        {
            OLVColumn weight = new OLVColumn();
            weight.Text = "Weight";
            weight.AspectGetter = delegate (object x) { return ((double)((Item)x).GetAttributeValue("Weight")) / 100; };
            weight.AspectToStringFormat = "{0} oz";

            return weight;
        }


        private OLVColumn itemCol_FoodRegen()
        {
            var regen = new OLVColumn();
            regen.Text = "Regen";
            regen.AspectGetter = delegate (object x) { return ((Item)x).GetAttributeValue("Nutrition"); };
            regen.AspectToStringConverter = delegate (object x)
            {
                TimeSpan t = TimeSpan.FromSeconds(((double)((int)x * 2) / 10) * 60);

                string answer = string.Format("{0:D2}:{1:D2}",
                                t.Minutes,
                                t.Seconds);

                return answer;
            };

            return regen;
        }
        private OLVColumn itemCol_FoodNBP() {

            var nutritionByPrice = new OLVColumn();
            nutritionByPrice.Text = "P/R";
            nutritionByPrice.AspectGetter = delegate (object x) {
                var nutrition = ((Item)x).GetAttributeValue("Nutrition");

                // Find cheapers vendor price
                double price = 9999;

                foreach (var npc in _dataReader.npcs)
                    foreach (var t in npc.transactions)
                        if (t.ItemID == ((Item)x).ID && t.Type == NPC.TransactionType.Buy)
                            if (t.Price < price) price = t.Price;

                return Math.Round(price / nutrition, 2);
            };
            nutritionByPrice.AspectToStringConverter = delegate (object x)
            {
                var value = (double)x;

                if (value > 100) return "N/A";
                else return value.ToString();
            };
            nutritionByPrice.ToolTipText = "Cheapest vendor price / regen. Lower values mean it's more priceworthy";

            return nutritionByPrice;
        }
        private OLVColumn itemCol_FoodNBW() {

            var nutritionByWeight = new OLVColumn();
            nutritionByWeight.Text = "W/R";
            nutritionByWeight.AspectGetter = delegate (object x) {
                var weight = ((Item)x).GetAttributeValue("Weight");
                var nutrition = (double)((Item)x).GetAttributeValue("Nutrition");

                return Math.Round(weight / nutrition, 2);
            };
            nutritionByWeight.ToolTipText = "Weight / regen. Lower values mean better regen by weight";

            return nutritionByWeight;
        }

        private OLVColumn itemCol_ExpireTime()
        {
            var expiry = new OLVColumn();
            expiry.Text = "Duration";
            expiry.AspectGetter = delegate (object x) { return ((Item)x).GetAttributeValue("TotalExpireTime"); };
            expiry.AspectToStringConverter = delegate (object x)
            {
                var time = (int)x;

                if (time == 0) return "N/A";
                else
                {
                    TimeSpan t = TimeSpan.FromSeconds(time);
                    if (t.Hours != 0) return string.Format("{0:D2}:{1:D2}:{2:D2}", t.Hours, t.Minutes, t.Seconds);
                    else return string.Format("{0:D2}:{1:D2}", t.Minutes, t.Seconds);
                }
            };

            return expiry;
        }

        private OLVColumn itemCol_ContainerCapacity()
        {
            var capacity = new OLVColumn();
            capacity.Text = "Size";

            capacity.AspectGetter = delegate (object x) { return ((Item)x).GetAttributeValue("Capacity"); };

            return capacity;
        }

        #endregion

        private void PopulateItemList(string filter, OLVColumn[] columns = null, bool matchAnyFilter = false)
        {
            listView_items.Columns.Clear();

            listView_items.SmallImageList = new ImageList();
            listView_items.SmallImageList.ImageSize = new Size(32, 32);

            listView_items.Columns.Add(itemCol_ItemName());

            List<Item> items = new List<Item>();

            if (matchAnyFilter)
            {
                var split = filter.Split(',');

                foreach ( var f in split ) 
                    items.AddRange(_dataReader.GetItemsByFilter(_dataReader.items, f));

                items = items.Distinct().ToList();
            }
            else
                items.AddRange(_dataReader.GetItemsByFilter(_dataReader.items, filter));

            if (columns != null)
                listView_items.Columns.AddRange(columns);

            listView_items.Columns.Add(itemCol_Weight());

            listView_items.Objects = items;
        }

        private void items_comboBox_itemCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            itemSearch = "";

            // Column headers + attribute names

            if (items_comboBox_itemCategory.Text == "All") PopulateItemList("Take");

            if (items_comboBox_itemCategory.Text == "Helmet") PopulateItemList("BodyPosition=1", new OLVColumn[] { itemCol_Arm() } );
            if (items_comboBox_itemCategory.Text == "Armor") PopulateItemList("BodyPosition=4", new OLVColumn[] { itemCol_Arm() });
            if (items_comboBox_itemCategory.Text == "Legs") PopulateItemList("BodyPosition=7", new OLVColumn[] { itemCol_Arm() });
            if (items_comboBox_itemCategory.Text == "Boots") PopulateItemList("BodyPosition=8", new OLVColumn[] { itemCol_Arm() });
            if (items_comboBox_itemCategory.Text == "Necklace") PopulateItemList("BodyPosition=2", new OLVColumn[] { itemCol_Arm() });
            if (items_comboBox_itemCategory.Text == "Ring") PopulateItemList("BodyPosition=9", new OLVColumn[] { itemCol_SkillModifier(), itemCol_ExpireTime() });  
            if (items_comboBox_itemCategory.Text == "Shield") PopulateItemList("Shield", new OLVColumn[] { itemCol_ShieldDefense() } );


            if (items_comboBox_itemCategory.Text == "Axe") PopulateItemList("WeaponType=3", new OLVColumn[] { itemCol_Attack(), itemCol_WeaponDefense() });
            if (items_comboBox_itemCategory.Text == "Club") PopulateItemList("WeaponType=2", new OLVColumn[] { itemCol_Attack(), itemCol_WeaponDefense() });
            if (items_comboBox_itemCategory.Text == "Sword") PopulateItemList("WeaponType=1", new OLVColumn[] { itemCol_Attack(), itemCol_WeaponDefense() });
            if (items_comboBox_itemCategory.Text == "Distance") PopulateItemList("BowRange=*,ThrowRange=*", new OLVColumn[] { itemCol_AttackRanged(), itemCol_Range() }, true); 
            if (items_comboBox_itemCategory.Text == "Ammo") PopulateItemList("AmmoType=*");

            if (items_comboBox_itemCategory.Text == "Equippable Container") PopulateItemList("BodyPosition=3", new OLVColumn[] { itemCol_ContainerCapacity() });
            if (items_comboBox_itemCategory.Text == "Runes") PopulateItemList("Rune", new OLVColumn[] { itemCol_RuneCharges() });
            if (items_comboBox_itemCategory.Text == "Light") PopulateItemList("Light,Expire,Take", new OLVColumn[] { itemCol_ExpireTime() } );
            if (items_comboBox_itemCategory.Text == "Food") PopulateItemList("Food", new OLVColumn[] { itemCol_FoodRegen(), itemCol_FoodNBP(), itemCol_FoodNBW() });

        }
        private void items_comboBox_itemCategory_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                itemSearch = items_comboBox_itemCategory.Text.ToLower();

                PopulateItemList("Take");
                listView_items.ModelFilter = Items_BuildFilter();
            }
        }


        private void items_checkBox_hideExpiring_CheckedChanged(object sender, EventArgs e)
        {
            listView_items.ModelFilter = Items_BuildFilter();
        }


        private ModelFilter Items_BuildFilter()
        {
            ModelFilter filter = new ModelFilter(delegate (object x) {
                bool search, hideExpiring;
                search = hideExpiring = true;

                if (items_checkBox_hideExpiring.Checked)
                    hideExpiring = !((Item)x).Name.ToLower().Contains('(');

                if (!string.IsNullOrEmpty(itemSearch)) search = ((Item)x).Name.ToLower().Contains(itemSearch);

                return search && hideExpiring;
            
            });

            return filter;
        }

        private void listView_items_SelectionChanged(object sender, EventArgs e)
        {
            if (listView_items.SelectedObject != null)
            {
                 
                Item selected = (Item)listView_items.SelectedObject;

                Items_DisplayInfo(selected);


            }
        }

        private void Items_DisplayInfo(Item item)
        {
            itemName_label.Text = item.Name;

            string itemInfo = "ID: " + item.ID;

            if (!string.IsNullOrEmpty(item.Description)) itemInfo += ",," + "--- Description ---,," + item.Description;

            itemInfo += ",,--- Flags ---,,";

            foreach (var flag in item.Flags) itemInfo += "  " + flag + ",";

            itemInfo += ",--- Attributes ---,,";

            foreach (var att in item.Attributes) itemInfo += "  " + att.Name + " = " + att.Value + ",";

            textBox1.Lines = itemInfo.Split(',');

            var entity = _dataReader.ReadEntity(item.ID - 100);

            itemIcon.Image = _dataReader.ReadSprite(entity.sprites[0]);


            List<ItemTrade> trades = new List<ItemTrade>();

            foreach (var npc in _dataReader.npcs)
            {
                foreach (var t in npc.transactions)
                {
                    if (t.ItemID == item.ID)
                    {
                        ItemTrade trade;

                        trade.npc = npc;
                        trade.transaction = t;

                        trades.Add(trade);
                    }
                }
            }

            List<ItemDrop> drops = new List<ItemDrop>();

            foreach (var mon in _dataReader.monsters)
            {
                foreach (var d in mon.Inventory)
                {
                    if (d.ItemID == item.ID)
                    {
                        ItemDrop drop;

                        drop.monster = mon;
                        drop.drop = d;

                        drops.Add(drop);
                    }
                }
            }

            listView_trades.Objects = trades;
            listView_itemDrops.Objects = drops;

        }

        private void InitializeItemTrades()
        {
            olvColumn7.AspectGetter = delegate (object x) {  return ((ItemTrade)x).npc.Name; };
            olvColumn8.AspectGetter = delegate (object x) { return ((ItemTrade)x).transaction.Price;   };
            olvColumn9.AspectGetter = delegate (object x) { return ((ItemTrade)x).npc.Location.Name; };

            olvColumn7.GroupKeyGetter =
            olvColumn8.GroupKeyGetter =
            olvColumn9.GroupKeyGetter = delegate (object x) { 
                switch (((ItemTrade)x).transaction.Type)
                {
                    case NPC.TransactionType.Buy:
                        return "Buy from";
                    case NPC.TransactionType.Sell:
                        return "Sell to";
                }
                return "Error";
            };
        }
        private void InitializeItemDrops()
        {

            olvColumn10.AspectGetter = delegate (object x) { return ((ItemDrop)x).monster.Name; };
            olvColumn12.AspectGetter = delegate (object x) { return ((ItemDrop)x).drop.Amount; };
            olvColumn11.AspectGetter = delegate (object x) { return ((double)(((ItemDrop)x).drop.Rate) / 10); };
            olvColumn11.AspectToStringFormat = "{0}%";
        }


        private void listView_trades_ItemActivate(object sender, EventArgs e)
        {
            var trade = (ItemTrade)listView_trades.SelectedObject;

            var pos = trade.npc.Position;
            OpenInBrowser("https://tibiantis.info/library/map#" + pos.X + "," + pos.Y + "," + pos.Z + ",8");

        }
        private void listView_itemDrops_ItemActivate(object sender, EventArgs e)
        {
            var drop = (ItemDrop)listView_itemDrops.SelectedObject;

            tabControl1.SelectedTab = tabPage_monsters;
            monsters_listView.SelectedObject = drop.monster;
        }


        private LiquidContainer Item_GetLiquidContainerName(NPC.Transaction transaction)
        {
            Item item = _dataReader.items.Where(i => i.ID == transaction.ItemID).FirstOrDefault();

            LiquidContainer container = new LiquidContainer();

            if (item != null)
            {
                switch (transaction.Data)
                {
                    case 1:
                        container.liquid = "Water";
                        container.spriteIndex = 1;
                        break;
                    case 2:
                        container.liquid = "Wine";
                        container.spriteIndex = 7;
                        break;
                    case 3:
                        container.liquid = "Beer";
                        container.spriteIndex = 3;
                        break;
                    case 4:
                        container.liquid = "Mud";
                        container.spriteIndex = 3;
                        break;
                    case 5:
                        container.liquid = "Blood";
                        container.spriteIndex = 2;
                        break;
                    case 6:
                        container.liquid = "Slime";
                        container.spriteIndex = 4;
                        break;
                    case 7:
                        container.liquid = "Oil";
                        container.spriteIndex = 3;
                        break;
                    case 8:
                        container.liquid = "Urine";
                        container.spriteIndex = 5;
                        break;
                    case 9:
                        container.liquid = "Milk";
                        container.spriteIndex = 6;
                        break;
                    case 10:
                        container.liquid = "Manafluid";
                        container.spriteIndex = 7;
                        break;
                    case 11:
                        container.spriteIndex = 2;
                        container.liquid = "Lifefluid";
                        break;
                    case 12:
                        container.liquid = "Lemonade";
                        container.spriteIndex = 5;
                        break;
                }
            }
            return container;
        }


        public struct LiquidContainer
        {
            public string liquid;
            public byte spriteIndex; // Only for vial, not sure if mug even has a bunch of sprites
        }

        private struct ItemTrade
        {
            public NPC npc;
            public NPC.Transaction transaction;
        }

        private struct ItemDrop
        {
            public Monster monster;
            public Monster.Drop drop;
        }
        
        #endregion

        #region Spells

        private void InitializeSpellsTab()
        {
            spells_comboBox_vocation.SelectedIndex = 0;
            spells_comboBox_group.SelectedIndex = 0;
            spells_comboBox_type.SelectedIndex = 0;
            spells_comboBox_premium.SelectedIndex = 0;

            spells_listView.UseFiltering = true;


            olvColumn32.AspectGetter = delegate (object x)
            {
                Spell s = (Spell)x;
                string classes = "";

                if (s.VocKnight) classes += "K,";
                if (s.VocPaladin) classes += "P,";
                if (s.VocSorcerer) classes += "S,";
                if (s.VocDruid) classes += "D";

                if (classes[classes.Length-1] == ',') classes = classes.Substring(0, classes.Length - 1);

                return classes;
            };


            olvColumn27.AspectToStringConverter = delegate (object x) { 
                byte type = (byte)x;
                if (type == 1) return "Rune";
                return "Instant";
            };

            olvColumn36.AspectToStringConverter = delegate (object x) {
                byte group = (byte)x;
                if (group == 1) return "Support";
                if (group == 2) return "Attack";
                return "Healing";
            };

            olvColumn28.AspectToStringConverter = delegate (object x)
            {
                bool prem = (bool)x;
                if (prem) return "Yes";
                return "No";
            };

            spells_listView.Objects = _dataReader.spells;




        }
        private void spells_listView_SelectionChanged(object sender, EventArgs e)
        {
            if (spells_listView.SelectedObject != null)
            {
                Spell s = (Spell)spells_listView.SelectedObject;

                spells_label.Text = s.Name;

                List<NPC> npcs = new List<NPC>();

                foreach (NPC n in _dataReader.npcs)
                {
                    foreach (var t in n.transactions)
                        if (t.Type == NPC.TransactionType.Spell && t.ItemID == s.ID) npcs.Add(n);
                }

                spellsNpcs_listView.Objects = npcs;

            }
        }

        private void spells_comboBox_filters_SelectedIndexChanged(object sender, EventArgs e)
        {
            spells_listView.ModelFilter = new ModelFilter(delegate (object x) {
                bool vocFilter, groupFilter, typeFilter, premiumFilter;
                vocFilter = groupFilter = typeFilter = premiumFilter = false;
                Spell s = (Spell)x;

                switch (spells_comboBox_vocation.SelectedIndex)
                {
                    case 0:
                        vocFilter = true;
                        break;
                    case 1:
                        vocFilter = s.VocKnight;
                        break;
                    case 2:
                        vocFilter = s.VocPaladin;
                        break;
                    case 3:
                        vocFilter = s.VocSorcerer;
                        break;
                    case 4:
                        vocFilter = s.VocDruid;
                        break;
                }

                switch (spells_comboBox_group.SelectedIndex)
                {
                    case 0:
                        groupFilter = true;
                        break;
                    default:
                        groupFilter = s.Group == spells_comboBox_group.SelectedIndex - 1;
                        break;
                }

                switch (spells_comboBox_type.SelectedIndex)
                {
                    case 0:
                        typeFilter = true;
                        break;
                    default:
                        typeFilter = s.Type == spells_comboBox_type.SelectedIndex - 1;
                        break;
                }


                switch (spells_comboBox_premium.SelectedIndex)
                {
                    case 0:
                        premiumFilter = true;
                        break;
                    case 1:
                        premiumFilter = s.Premium == true;
                        break;
                    case 2:
                        premiumFilter = s.Premium == false;
                        break;
                }


                return vocFilter & groupFilter & typeFilter & premiumFilter;
            });



        }
        
        private void spellsNpcs_listView_ItemActivate(object sender, EventArgs e)
        {
            var npc = (NPC)spellsNpcs_listView.SelectedObject;

            var pos = npc.Position;
            OpenInBrowser("https://tibiantis.info/library/map#" + pos.X + "," + pos.Y + "," + pos.Z + ",8");
        }
        
        #endregion


        #region Login Alert

        private void InitializeLoginAlert()
        {
            DataTable table = new DataTable();
            table.TableName = "Player";

            table.Columns.AddRange(new DataColumn[] {
                new DataColumn("Name"),
                new DataColumn("Alert")
            });

            try
            {
                table.ReadXml(file_loginAlert);
            }
            catch (FileNotFoundException ex )
            {
                return;
            }
            catch (Exception ex)
            {
                switch (MessageBox.Show(ex.Message, "Could not read Login Tracker data", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error))
                {
                    case DialogResult.Retry:
                        InitializeLoginAlert();
                        break;
                }
            }
            
            foreach ( DataRow p in table.Rows )
            {
                string name, alert;
                name = alert = null;

                if (p.ItemArray[0].GetType() != typeof(DBNull)) name = (string)p.ItemArray[0];
                if (p.ItemArray[1].GetType() != typeof(DBNull)) alert = (string)p.ItemArray[1];

                var player = new TrackedPlayer(name, alert);
                _trackedPlayers.Add(player);


            }

            _trackedPlayers = _trackedPlayers.OrderBy(p => p.Name).ToList();

            loginAlert_dataGridView.DataSource = table;

        }

        private void LoginTrackerAddDialog()
        {
            var diag = new Form_LoginAlertAddDialog();

            if (diag.ShowDialog() == DialogResult.OK)
            {
                if (!string.IsNullOrEmpty(diag.PlayerName))
                    LoginTrackerAddPlayer(diag.PlayerName, diag.Alert);
            }
        }

        public void LoginTrackerAddPlayer(string player, string alert = null)
        {
            TrackedPlayer p = new TrackedPlayer(player, alert);
            _trackedPlayers.Add(p);
            _trackedPlayers = _trackedPlayers.OrderBy(pp => pp.Name).ToList();
            LoginTrackerUpdate();
            LoginAlertSaveData(file_loginAlert);
        }

        private void LoginTrackerCheckOnline()
        {
            DataTable table = (DataTable)loginAlert_dataGridView.DataSource;
            List<TrackedPlayer> alerts = new List<TrackedPlayer>();
            


            foreach ( var p in _trackedPlayers )
            {
                bool needsUpdate = false;

                // Some stuff for when user has put the tracked players name in the wrong casing
                var onlinePlayer = _currentlyOnline.Where(s => s.Name.Equals(p.Name, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();

                if (!onlinePlayer.Equals(default(Player)))
                {
                    if (!onlinePlayer.Name.Equals(p.Name, StringComparison.Ordinal))
                    {
                        // If wrong casing, fix it
                        p.Name = onlinePlayer.Name;
                        needsUpdate = true;
                    }

                    DataRow[] select = table.Select("Name='" + onlinePlayer.Name + "'");
                    if (select[0] != null)
                    {
                        // Tracked player is online and everything checks out. Do the stuff

                        if (!_lastOnline.Contains(p.Name))
                        {
                            // Wasn't online before so run the alert
                            _lastOnline.Add(p.Name);
                            alerts.Add(p);
                        }

                        // Color row green :)
                        var index = table.Rows.IndexOf(select[0]);
                        loginAlert_dataGridView.Rows[index].DefaultCellStyle.BackColor = Color.PaleGreen;

                        if (needsUpdate)
                            loginAlert_dataGridView.Rows[index].Cells[0].Value = onlinePlayer.Name; // Already updated name in _trackedPlayers, also update i

                    }
                }
                else
                {
                    // Not online
                    if (_lastOnline.Contains(p.Name))
                    {
                        // Was online before
                        _lastOnline.Remove(p.Name);

                        DataRow[] select = table.Select("Name='" + p.Name + "'");
                        if (select[0] != null)
                        {
                            DataRow row = (select[0]);
                            var index = table.Rows.IndexOf(row);
                            loginAlert_dataGridView.Rows[index].DefaultCellStyle.BackColor = Color.White;
                        }
                    }
                }

            }


            LoginAlert(alerts);

        }

        private void LoginAlert(List<TrackedPlayer> players)
        {
            if (players.Count != 0)
            {
                string alertSound = "";
                string alertString = "";

                int lastShownIndex = 0;

                if (players.Count == 2) lastShownIndex = 1;
                else if (players.Count >= 3) lastShownIndex = 2;

                for (int i = 0; i < players.Count; i++)
                {
                    if (alertSound == "")
                        if (players[i].Alert != null)
                        {
                            if (File.Exists(players[i].Alert))
                                alertSound = players[i].Alert;

                        }

                    if (i < 3)
                    {
                        alertString += players[i].Name;

                        if (lastShownIndex != 0)
                        {
                            if (i == lastShownIndex-1) alertString += " and ";
                            else if (i != lastShownIndex) alertString += ", ";
                        }
                    }
                }

                if (players.Count == 1) alertString += " has ";
                else
                {
                    if (players.Count > 3) alertString += $" (plus {players.Count-3} more) ";
                    alertString += " have ";
                }

                alertString += "logged in!";
                
                if (Settings.Default.LoginAlertNotifTray)
                    Tray_ShowBubble(TrayBubbleBehaviour.None, 5000, "Tibiantis Login", alertString, ToolTipIcon.Warning);

                if (alertSound == "" && Settings.Default.LoginAlertNotifSound && !string.IsNullOrEmpty(Settings.Default.LoginAlertNotifSoundPath))
                    alertSound = Settings.Default.LoginAlertNotifSoundPath; 

                if (alertSound != "")
                {
                    try
                    {
                        SoundPlayer alertPlayer = new SoundPlayer(alertSound);
                        alertPlayer.Play();
                    } 
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Could not play alert sound", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }

        }

        private void LoginAlertRemoveSelected()
        {
            if (loginAlert_dataGridView.SelectedCells.Count != 0)
            {
                var index = loginAlert_dataGridView.SelectedCells[0].RowIndex;
                var selectedName = (string)loginAlert_dataGridView.Rows[index].Cells[0].Value;

                var ooc = _trackedPlayers.Where(p => p.Name == (string)loginAlert_dataGridView.Rows[index].Cells[0].Value).FirstOrDefault();

                if (ooc != null)
                {
                    if ( MessageBox.Show(
                        $"Are you sure you want to remove {ooc.Name} from login alert?",
                        "Login Alert Remove",
                        MessageBoxButtons.YesNo,MessageBoxIcon.Question)    
                    == DialogResult.Yes )
                    {
                        _trackedPlayers.Remove(ooc);
                        LoginTrackerUpdate();
                        LoginAlertSaveData(file_loginAlert);
                    }
                }
            }
        }


        // For adding player via current online form. This should absolutely not exist but I'm lazy
        // Somehow, a more generalized function that takes a string to tabControl.SelectTab() didn't work
        public void OpenLoginAlertTab() { tabControl1.SelectTab(tabPage_loginAlert); }



        #region Login Alert Sidebar

        private void loginAlert_button_add_Click(object sender, EventArgs e)
        {
            LoginTrackerAddDialog();
        }

        private void loginAlert_button_remove_Click(object sender, EventArgs e)
        {
            LoginAlertRemoveSelected();
        }

        private void loginAlert_button_browseSound_Click(object sender, EventArgs e)
        {
            var diag = new OpenFileDialog();
            diag.RestoreDirectory = true;

            diag.Filter = "WAV files (*.wav)|*.wav";

            if (diag.ShowDialog() == DialogResult.OK)
            {
                loginAlert_alertTextBox.Text = Path.GetFileName(diag.FileName); 
                Settings.Default.LoginAlertNotifSoundPath = diag.FileName;
                Settings.Default.Save();
            }
        }

        private void loginAlert_checkBox_notifSound_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.LoginAlertNotifSound = loginAlert_checkBox_notifSound.Checked;
            Settings.Default.Save();
        }

        private void loginAlert_checkBox_notifTray_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.LoginAlertNotifTray = loginAlert_checkBox_notifTray.Checked;
            Settings.Default.Save();
        }

        #endregion

        #region Login Alert Data
        private void loginAlert_dataGridView_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            DataGridView grid = (DataGridView)sender;

            grid.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells; 
            grid.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }
        private void LoginAlertSaveData(string path)
        {
            if (loginAlert_dataGridView.DataSource != null)
            {
                DataTable t = (DataTable)loginAlert_dataGridView.DataSource;
                t.WriteXml(path);
            }
        }
        private void LoginTrackerUpdate()
        {
            DataTable table = new DataTable();

            table.TableName = "Player";

            table.Columns.AddRange(new DataColumn[] {
                new DataColumn("Name"),
                new DataColumn("Alert")
            });

            foreach (var p in _trackedPlayers)
            {
                table.Rows.Add(p.Name, p.Alert);
            }

            loginAlert_dataGridView.DataSource = table;
            

        }

        private void loginAlert_dataGridView_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            // Unfortunately need this because rows can't be colored before they have been shown
            // Alternatively you could change the tab to Login Alert and back on startup, which would be better performance-wise, but
            // you'd have to do it with this.Visible = true, so it ends up just looking kind of ugly..

            for (int i = e.RowIndex; i < e.RowIndex + e.RowCount; i++)
            {
                LoginAlertColorRow(i);

                if (loginAlert_dataGridView.Rows[i].Cells[1].Value.GetType() == typeof(DBNull))
                    loginAlert_dataGridView.Rows[i].Cells[1].Value = "Default";
            }
        }

        private void LoginAlertColorRow(int index)
        {
            if (!_currentlyOnline.FirstOrDefault(i => i.Name == (string)(loginAlert_dataGridView.Rows[index].Cells[0].Value)).Equals(default(Player)))
                loginAlert_dataGridView.Rows[index].DefaultCellStyle.BackColor = Color.PaleGreen;
            else
                loginAlert_dataGridView.Rows[index].DefaultCellStyle.BackColor = Color.White;
        }

        private void loginAlert_dataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (loginAlert_dataGridView.SelectedCells.Count != 0)
            {
                var row = loginAlert_dataGridView.SelectedCells[0].RowIndex;
                loginAlert_labelSelected.Text = (string)loginAlert_dataGridView.Rows[row].Cells[0].Value;
            }
        }

        private void loginAlert_dataGridView_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                DataGridView.HitTestInfo hit = loginAlert_dataGridView.HitTest(e.X, e.Y);
                if (hit.Type == DataGridViewHitTestType.Cell)
                {
                    loginAlert_dataGridView.ClearSelection();

                    var clickedCell = loginAlert_dataGridView.Rows[hit.RowIndex].Cells[hit.ColumnIndex];
                    clickedCell.Selected = true;

                    var player = _trackedPlayers.Where(p => p.Name == (string)loginAlert_dataGridView.Rows[clickedCell.RowIndex].Cells[0].Value).FirstOrDefault();

                    if (player != null)
                    {
                        ContextMenu menu = new ContextMenu();


                        var add = new MenuItem("Add", (s, ee) => { LoginTrackerAddDialog(); });
                        var remove = new MenuItem("Remove", (s, ee) => {
                            _trackedPlayers.Remove(player);
                            LoginTrackerUpdate();
                            LoginAlertSaveData(file_loginAlert);
                        });
                        var alert = new MenuItem("Alert");
                        var alertChange = new MenuItem("Change", (s, ee) => {
                            LoginAlertChangeAlertDialog(player);
                            LoginTrackerUpdate();
                            LoginAlertSaveData(file_loginAlert);
                        }); 
                        var alertClear = new MenuItem("Clear", (s, ee) => {
                            player.Alert = null;
                            LoginTrackerUpdate();
                            LoginAlertSaveData(file_loginAlert);
                        });

                        alert.MenuItems.Add(alertChange);
                        alert.MenuItems.Add(alertClear);


                        menu.MenuItems.Add(add);
                        menu.MenuItems.Add(remove);
                        menu.MenuItems.Add(alert);

                        menu.Show((Control)sender,new Point(e.X, e.Y));

                    }
                }
            }
        }

        private void loginAlert_dataGridView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
                LoginAlertRemoveSelected();
        }

        private void loginAlert_dataGridView_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
            // Sorting was causing an issue where all the rows would get colored as if they were all online for some reason...
            e.Column.SortMode = DataGridViewColumnSortMode.NotSortable;
        }



        // Clicking cells stuff
        string beforeLoginAlertNameEdit = "";
        private void loginAlert_dataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            loginAlert_dataGridView.BeginEdit(true);
        }

        private void loginAlert_dataGridView_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                beforeLoginAlertNameEdit = (string)loginAlert_dataGridView.Rows[e.RowIndex].Cells[0].Value;
            }

            if (e.ColumnIndex == 1)
            {
                e.Cancel = true;

                var player = _trackedPlayers.Where(p => p.Name == (string)loginAlert_dataGridView.Rows[e.RowIndex].Cells[0].Value).FirstOrDefault();

                if (player != null)
                    LoginAlertChangeAlertDialog(player);
            }
        }

        private void LoginAlertChangeAlertDialog(TrackedPlayer player)
        {
            var diag = new OpenFileDialog();
            diag.RestoreDirectory = true;

            diag.Filter = "WAV files (*.wav)|*.wav";

            if (diag.ShowDialog() == DialogResult.OK)
            {
                player.Alert = diag.FileName;
                LoginTrackerUpdate();
            }
        }

        private void loginAlert_dataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 1)
            {
                e.Value = Path.GetFileName((string)e.Value);
            }
        }

        private void loginAlert_dataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {

            var player = _trackedPlayers.Where(p => p.Name == beforeLoginAlertNameEdit).FirstOrDefault();

            if (player != null && player.Name != beforeLoginAlertNameEdit)
            {
                player.Name = (string)loginAlert_dataGridView.Rows[e.RowIndex].Cells[0].Value;
                LoginTrackerUpdate();
            } 
        }
        #endregion

        public class TrackedPlayer
        {
            public string Name;
            public string Alert;        // Leaving this uninitialized is good for DataTable.WriteXml, avoids writing empty xml nodes

            public TrackedPlayer(string Name, string Alert = null)
            {
                this.Name = Name;
                this.Alert = Alert;
            }
        }
        
        #endregion





        private void SaveAllExit()
        {
            try
            {
                AccountsSave(file_accounts);
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
                LoginAlertSaveData(file_loginAlert);
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
                var acc = _accounts.Where(a => a.name == accounts_label_selected.Text).FirstOrDefault();
                AccountsDisplayInfo(acc);
            }
        }



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
            e.DrawFocusRectangle();
        }



    }

}
