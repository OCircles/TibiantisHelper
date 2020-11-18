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

        List<Account> _accounts;
        List<Control_Timer> _timers;
        List<Vocation> _vocations;
        List<Player> _trackedPlayers;
        List<string> _lastOnline; // Separate list for _trackedPlayers
        List<string> _currentlyOnline;

        SelectedVocation _selectedVocation;


        DataReader _dataReader;

        static string file_accounts = "Accounts.xml";
        static string file_loginAlert = "TrackedPlayers.xml";

        public static NotifyIcon _trayIcon;

        static readonly HttpClient webClient = new HttpClient();
        static string webpage_whoIsOnline = "https://tibiantis.online/?page=WhoIsOnline";

        public Form_Main()
        {

            _accounts = new List<Account>();
            _timers = new List<Control_Timer>();
            _vocations = new List<Vocation>();
            _dataReader = new DataReader();
            _trackedPlayers = new List<Player>();
            _lastOnline = new List<string>();
            _currentlyOnline = new List<string>();


            if (Settings.Default.UpgradeRequired)
            {
                Settings.Default.Upgrade();
                Settings.Default.UpgradeRequired = false;
                Settings.Default.Save();
            }



            InitializeComponent();

            PostInitialize();


            Color colorTibia_Beige = Color.FromArgb(251, 234, 208);
            this.BackColor = colorTibia_Beige;




            // Maybe move on to having everything embedded at some point:

            // Assembly assem = this.GetType().Assembly;
            // var resources = assem.GetManifestResourceNames();

            // foreach (var r in resources) Console.WriteLine(r);


        }

        private void PostInitialize()
        {

            _trayIcon = notifyIcon1;

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
            if (!string.IsNullOrEmpty(Settings.Default.TimerNotifSoundPath))
                textBox13.Text = Path.GetFileName(Settings.Default.TimerNotifSoundPath);

        }



        #region Form

        private async void Form1_Load(object sender, EventArgs e)
        {
            await GetNetworkStuff();
        }
        private void Form1_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                if (Settings.Default.MinimizeToTray)
                {
                    if (Settings.Default.NotifOnMinimizeToTray)
                    {
                        _trayIcon.BalloonTipTitle = "Tibiantis Helper";
                        _trayIcon.BalloonTipIcon = ToolTipIcon.Info;
                        _trayIcon.BalloonTipText = $"Minimized to taskbar{Environment.NewLine}{Environment.NewLine}" +
                            "(Click this if you don't want to see this message anymore)";

                        Tray_ShowBubble(TrayBubbleBehaviour.MuteMinimize, 5000);


                    }
                    this.ShowInTaskbar = false;
                }

            }
            else if (this.WindowState == FormWindowState.Normal)
            {
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

        #endregion

        #region Traybar
        
        private static TrayBubbleBehaviour trayBubbleBehaviour = 0;


        private void notifyIcon1_BalloonTipClicked(object sender, EventArgs e)
        {
            if (trayBubbleBehaviour == TrayBubbleBehaviour.MuteMinimize)
            {
                Settings.Default.NotifOnMinimizeToTray = false;
                Settings.Default.Save();
            }
        }

        public static void Tray_ShowBubble(TrayBubbleBehaviour behaviour, int time)
        {
            trayBubbleBehaviour = behaviour;
            _trayIcon.ShowBalloonTip(time);
        }
        public static void Tray_ShowBubble(TrayBubbleBehaviour behaviour, int time, string title, string text, ToolTipIcon icon)
        {
            trayBubbleBehaviour = behaviour;
            _trayIcon.ShowBalloonTip(time, title, text, icon);
        }
        
        
        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            this.BringToFront();
            this.TopMost = true;
            this.Focus();
            this.TopMost = false;
        }
        private void Tray_BuildContextMenu()
        {
            // NotifyIcon is not a control so can't use ContextMenu + MouseClick event
            // Instead we get this garbage where we use an associated placeholder context strip and populate it on Opening event
            // Actually could remove placeholder and construct it all here but I'm too tired and this works so

            tray_contextMenuStrip.Items.Clear();

            ToolStripItem accountDropdown = tray_contextMenuStrip.Items.Add("Accounts");

            foreach (Account acc in _accounts)
            {
                (accountDropdown as ToolStripMenuItem).DropDownItems.Add(acc.name, null, (s, e) => StringToClipboard(acc.login));
            }


            ToolStripItem settingsDropdown = tray_contextMenuStrip.Items.Add("Settings");

            var enableNetwork = ((ToolStripMenuItem)settingsDropdown).DropDownItems.Add("Enable Network");
            ToolStripMenuItem enableNetworkToolstrip = enableNetwork as ToolStripMenuItem;

            enableNetworkToolstrip.CheckOnClick = true;
            enableNetworkToolstrip.Checked = Settings.Default.EnableNetwork;
            enableNetworkToolstrip.CheckStateChanged += async (s, e) =>
            {
                if (!enableNetworkToolstrip.Checked)
                {
                    timer1.Stop();
                    header_label_onlineStatus.Text = "Offline";
                    header_linkLabel_playersOnline.Text = "0 players";
                    Settings.Default.EnableNetwork = false;
                    Settings.Default.Save();
                }
                else
                {
                    timer1.Start();
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
            MuteMinimize
        }

        #endregion



        #region Network Components

        private async void timer1_Tick(object sender, EventArgs e)
        {
            await GetNetworkStuff();
        }

        private async Task GetNetworkStuff()
        {
            if (Settings.Default.EnableNetwork)
            {
                await GetOnlineUsers();
                LoginTrackerCheckOnline();
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

                            bool bracketSearch, readingName;
                            bracketSearch = readingName = false;
                            string playerName = "";

                            int index = line.IndexOf("&#8595;Level");

                            for (int i = index; i < line.Length-13; i++)
                            {
                                if ( line.Substring(i, 13) == "menulink_hs'>")
                                {
                                    playerName = "";
                                    bracketSearch = true;
                                }  

                                if (readingName)
                                {
                                    if (line[i] == '<')
                                    {
                                        readingName = false;
                                        bracketSearch = false;
                                        _currentlyOnline.Add(playerName);
                                    }
                                    else
                                        playerName += line[i];
                                }

                                if (bracketSearch)
                                    if (line[i] == '>')
                                        readingName = true;
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

                            header_linkLabel_playersOnline.Text = line.Substring(index + 3, end) + " players";
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

                    // form.Location = Cursor.Position;
                    form.SetDesktopLocation(Cursor.Position.X, Cursor.Position.Y);
                    form.Show();
                }
                else
                    OpenInBrowser(webpage_whoIsOnline);
            }
            

        }

        #endregion
        
        #region Vocation
        private void comboBox_vocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            Settings.Default.Vocation = (byte)header_vocation_comboBox.SelectedIndex;
            UpdateVocation();
            UpdateProductionDropdown();
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

            CalculateProduction();
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

        private struct SelectedVocation
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
            textBox10.Text = $"Name: {acc.name}{Environment.NewLine}Account: {acc.login}";

            DateTime blank = new DateTime();
             
            if (acc.premium != blank || acc.house != blank) textBox10.Text += $"{Environment.NewLine}{Environment.NewLine}";

            if (acc.premium != blank) textBox10.Text += $"Premium ends in {(acc.premium - DateTime.Today).Days} days " +
                    $"({acc.premium.ToString("d", CultureInfo.CreateSpecificCulture("en-US"))}){Environment.NewLine}{Environment.NewLine}";


            if (acc.house != blank) textBox10.Text += $"House payment in {(acc.house - DateTime.Today).Days} days " +
                    $"({acc.house.ToString("d", CultureInfo.CreateSpecificCulture("en-US"))})";

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

                var acc = _accounts.Where(a => a.name == (string)accounts_dataGridView.Rows[clickedCell.RowIndex].Cells[0].Value).FirstOrDefault();

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
                                    var house = new MenuItem("Premium");
                                    var houseAdd30 = new MenuItem("Add 30 days (piggybank)", (s, ee) =>
                                    {
                                        acc.house = acc.house.AddDays(30);
                                        AccountsPopulate();
                                        AccountsSave(file_accounts);
                                    });
                                    var houseAdd33 = new MenuItem("Add 33 days (paypal)", (s, ee) =>
                                    {
                                        acc.house = acc.house.AddDays(33);
                                        AccountsPopulate();
                                        AccountsSave(file_accounts);
                                    });

                                    house.MenuItems.Add(houseAdd30);
                                    house.MenuItems.Add(houseAdd33);

                                    menu.MenuItems.Add(house);

                                }
                            }

                            menu.Show((Control)sender, new Point(e.X, e.Y));
                        }
                    }
                }
            }
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
                table.Rows.Add(a.name, a.login, a.premium, a.house);
            }

            accounts_dataGridView.DataSource = table;
        }

        private void AccountsSave(string path)
        { 
            if (accounts_dataGridView.DataSource != null)
            {
                DataTable t = (DataTable)accounts_dataGridView.DataSource;
                t.WriteXml(path);
            } 
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
                                case "Account":
                                    acc.login = accNodes.InnerText;
                                    break;
                                case "Premium_x0020_Expiry":
                                    acc.premium = DateTime.Parse(accNodes.InnerText);
                                    break;
                                case "House_x0020_Payment":
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

        private void TimersAdd(string name, string time, int multiplier, bool autoRestart)
        {
            Control_Timer timer = new Control_Timer(name, time, multiplier, autoRestart, _trayIcon);

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
                TimersAdd(dialog.TimerName, dialog.Time, dialog.Multiplier, dialog.AutoRestart);
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
                    regenStringSingle, 1, false));

                (runesBackpack as ToolStripMenuItem).DropDownItems.Add(r.Name, null, (s, ee) =>
                    TimersAdd($"BP of {r.Name} ({_selectedVocation.Name})",
                    regenStringBp, 1, false));

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
                    regenStringSingle, 1, false));

                (conjuringBackpack as ToolStripMenuItem).DropDownItems.Add(item.Name, null, (ss, ee) =>
                    TimersAdd($"BP of {item.Name} ({_selectedVocation.Name})",
                    regenStringBp, 1, false));


            }

            timers_contextMenuStrip.Items.Add("-");

            timers_contextMenuStrip.Items.Add("Bedmage (01:40:00)", null, (ss, ee) => TimersAdd("Bedmage", "01:40:00", 1, false));

            timers_contextMenuStrip.Items.Add("Idle (00:15:00)", null, (ss, ee) => TimersAdd("Idle", "00:15:00", 1, false));

            timers_contextMenuStrip.Items.Add("Food (00:20:00)", null, (ss, ee) => TimersAdd("Food", "00:20:00", 1, false));
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

        #endregion

        #region Calculator

        private void InitializeCalculatorTab()
        {

            foreach (TabPage t in calculator_tabControl.TabPages)
                calculator_listBox.Items.Add(t.Text);

            calculator_listBox.SelectedIndex = 0;

            // Food
            List<Item> foods = _dataReader.items.Where(i => i.Flags.Contains("Food")).OrderBy(i => i.Name).ToList();
            foreach (var f in foods) calculator_production_comboBox_food.Items.Add(f.Name);
            calculator_production_comboBox_food.SelectedIndex = 0;

            // Runes
            UpdateProductionDropdown();

            CalculateProduction();
        }
        
        private void calculator_listBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            calculator_tabControl.SelectedIndex = calculator_listBox.SelectedIndex;
        }

        #region Production

        string calculator_production_addTimer_time = "";
        string calculator_production_addTimer_title = "";

        private void CalculateProduction()
        {

            if (calculator_production_comboBox_rune.Text != "N/A")
            {
                bool num1, num2;
                num1 = num2 = false;

                int in1, in2;
                in1 = in2 = 0;

                num1 = int.TryParse(calculator_production_textBox_amountSingle.Text, out in1);
                num2 = int.TryParse(calculator_production_textBox_amountBackpack.Text, out in2);

                if (num1 && num2)
                {
                    if (in1+in2 > 0)
                    {
                        Item food = _dataReader.items.Where(f => f.Name == calculator_production_comboBox_food.Text).FirstOrDefault();
                        Rune rune = _dataReader.runes.Where(r => r.Name == calculator_production_comboBox_rune.Text).FirstOrDefault();
                        Spell spell = _dataReader.spells.Where(s => s.Name == calculator_production_comboBox_rune.Text).FirstOrDefault();
                    
                        string itemName = rune.Name;

                        int amount = in1 + (in2 * 20);
                        double casts = amount;

                        if (spell.Type == 0)
                        {
                            // For arrows/bolts, adjust amount for stacks
                            itemName = _dataReader.items.Where(i => i.ID == spell.ProduceID).FirstOrDefault().Name;
                        
                            casts = Math.Ceiling(((double)(amount * 100)) / (double)spell.ProduceAmount);
                        }


                        double totalMana = casts * spell.Mana;

                        double regenSec = (totalMana / _selectedVocation.regenMP) * 60;
                        double foodSec = ((double)((int)food.GetAttributeValue("Nutrition") * 2) / 10) * 60;
                        double foodDiv = Math.Ceiling(regenSec / foodSec);


                        TimeSpan regenTime = TimeSpan.FromSeconds(regenSec);

                        string regenString = "";

                        if (regenTime.Hours != 0) regenString += string.Format("{0:D1}h", regenTime.Hours + (regenTime.Days*24));
                        if (regenTime.Minutes != 0) regenString += string.Format("{0:D1}m", regenTime.Minutes);
                        if (regenTime.Seconds != 0) regenString += string.Format("{0:D1}s", regenTime.Seconds);

                        calculator_production_addTimer_time = string.Format("{0:D2}:{1:D2}:{2:D2}", regenTime.Hours + (regenTime.Days * 24), regenTime.Minutes, regenTime.Seconds); ;
                        calculator_production_addTimer_title = itemName;

                        calculator_textBox_result.Text = "A " + _selectedVocation.Name +
                            " producing " + itemName + " (";

                        string parenthesis = "";

                        if (spell.Type == 1)
                            parenthesis = casts + "x";
                        else
                        {
                            parenthesis += amount + " stack";
                            if (amount != 1) parenthesis += "s";
                        }

                        calculator_textBox_result.Text += parenthesis;

                        calculator_production_addTimer_title += $" {parenthesis} ({_selectedVocation.Name})";

                        calculator_textBox_result.Text += 
                            ") takes " + regenString +
                            " and uses " + foodDiv + "x " + food.Name + "(" +
                            (((double)food.GetAttributeValue("Weight")) / 100) * foodDiv + "oz)";


                    }
                    else
                    {
                        calculator_textBox_result.Text = "Please input production amount above";
                    }
                } 
                else
                {
                    calculator_textBox_result.Text = "Could not parse input";
                }
            }
        }
        private void UpdateProductionDropdown()
        {
            string oldSelection = calculator_production_comboBox_rune.Text;

            calculator_production_comboBox_rune.Items.Clear();

            List<Spell> spells = _dataReader.spells.Where(s => s.ProduceID != 0).OrderBy(s=>s.Name).ToList();

            foreach (var s in spells)
            {
                bool add = false;
                if (header_vocation_comboBox.SelectedIndex == 0 && s.VocKnight) add = true;
                if (header_vocation_comboBox.SelectedIndex == 1 && s.VocPaladin) add = true;
                if (header_vocation_comboBox.SelectedIndex == 2 && s.VocSorcerer) add = true;
                if (header_vocation_comboBox.SelectedIndex == 3 && s.VocDruid) add = true;

                if (add)
                    calculator_production_comboBox_rune.Items.Add(s.Name);
            }
            if (calculator_production_comboBox_rune.Items.Count == 0) calculator_production_comboBox_rune.Items.Add("N/A");

            int oldIndex = calculator_production_comboBox_rune.Items.IndexOf(oldSelection);
            
            if (oldIndex != -1)
                calculator_production_comboBox_rune.SelectedIndex = oldIndex;
            else
                calculator_production_comboBox_rune.SelectedIndex = 0;
        }

        private void calculator_production_button_addTimer_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(calculator_production_addTimer_time))
            {
                TimersAdd(calculator_production_addTimer_title, calculator_production_addTimer_time, 0, false);
                tabControl1.SelectedTab = tabPage_timers;
            }
        }


        private void calculator_production_textBox_TextChanged(object sender, EventArgs e)
        {
            CalculateProduction();
        }

        private void calculator_production_textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar);
        }

        private void calculator_production_comboBox_food_SelectedIndexChanged(object sender, EventArgs e)
        {
            CalculateProduction();
        }

        private void calculator_production_comboBox_rune_SelectedIndexChanged(object sender, EventArgs e)
        {
            CalculateProduction();
        }

        #endregion

        #region Experience
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
                            TimeSpan endOfDay = new TimeSpan(1,0,0,0);
                            endOfDay = endOfDay.Subtract(startTime);

                            diff = diff.Add(endOfDay);
                            diff = diff.Add(endTime);
                        }
                        else
                            diff = endTime.Subtract(startTime);

                        double expPerMin = (totalExp / diff.TotalSeconds) * 60;

                        string durationString = "";


                        if (diff.Hours != 0) durationString += string.Format("{0:D1}h", diff.Hours);
                        if (diff.Minutes != 0) durationString += string.Format("{0:D1}m", diff.Minutes);
                        if (diff.Seconds != 0) durationString += string.Format("{0:D1}s", diff.Seconds);

                        calculator_textBox_result.Text = "You gained " + totalExp + " experience over a duration of " +
                            durationString + " (" + Math.Round(expPerMin, 1) + " exp per minute)";
                         
                         
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

        #endregion

        #endregion

        #region Monsters

        private void InitializeMonsterTab()
        {
            // Monster list
            olvColumn18.AspectToStringConverter = delegate (object x)
            {
                var s = (int)x;

                if (s == 0) return "N/A";
                return s + " MP";
            };

            monsters_listView.Objects = _dataReader.monsters;

            // Monster drop list
            olvColumn21.AspectGetter = delegate (object x) { return _dataReader.items.Where(item => item.ID == ((Monster.Drop)x).ItemID).FirstOrDefault().Name; };
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
                    Entity entity = _dataReader.ReadEntity(transaction.ItemID - 100);
                    return _dataReader.ReadSprite(entity.sprites[0]);
                }
                else
                    return "Spell";
            };

            olvColumn5.AspectGetter = delegate (object x)
            {
                if (((NPC.Transaction)x).ItemID > 100)
                    return _dataReader.items.Where(i => i.ID == ((NPC.Transaction)x).ItemID).FirstOrDefault().Name;
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
                        Console.WriteLine(npcFolder + "\\" + npc.Filename);
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

        private void InitializeItemsTab()
        {
            InitializeItemTrades();
            InitializeItemDrops();

            items_comboBox_itemCategory.SelectedIndex = 1;
        }
        private void PopulateItemList(string filter)
        {

            listView_items.Columns.Clear(); 

            listView_items.SmallImageList = new ImageList();
            listView_items.SmallImageList.ImageSize = new Size(32, 32);

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

            listView_items.Columns.Add(itemName);

            List<Item> items = ProcessItemFilters(filter.Split(','));
            
            OLVColumn weight = new OLVColumn();
            weight.Text = "Weight"; 
            weight.AspectGetter = delegate (object x) { return ((double)((Item)x).GetAttributeValue("Weight")) / 100; }; 
            weight.AspectToStringFormat = "{0} oz";

            listView_items.Columns.Add(weight);
             
            listView_items.Objects = items;

        }

        private List<Item> ProcessItemFilters(string[] filters)
        {
            // This has to be returned so that listView_items.Objects can get set after adding weight column
            // If you add it in here instead it'll glitch the heck out

            // You could put all this in PopulateItemList to avoid the return but I thought it was nice to have this bit separate
            // Readability +1

            List<Item> items = new List<Item>();

            foreach (string filter in filters)
            {
                if (filter.Contains('='))
                {
                    // Attribute
                    var splitFilter = filter.Split('=');

                    int value = -1;
                    if (splitFilter[1] != "*") value = int.Parse(splitFilter[1]);

                    items.AddRange(_dataReader.GetItemsByAttribute(_dataReader.items, splitFilter[0], value));

                    if (splitFilter[0] == "BodyPosition")
                    {
                        switch (value)
                        {
                            case 1:
                            case 2:
                            case 4:
                            case 7:
                            case 8:
                            case 9:
                                var armor = new OLVColumn();
                                armor.Text = "Arm";

                                armor.AspectGetter = delegate (object x)  { return ((Item)x).GetAttributeValue("ArmorValue"); };

                                listView_items.Columns.Add(armor);

                                break;
                            case 3:
                                var container = new OLVColumn();
                                container.Text = "Size";

                                container.AspectGetter = delegate (object x) { return ((Item)x).GetAttributeValue("Capacity"); };

                                listView_items.Columns.Add(container);

                                break;
                        }
                    }
                    else if (splitFilter[0] == "WeaponType")
                    {

                        var atk = new OLVColumn();
                        atk.Text = "Attack"; 
                        atk.AspectGetter = delegate (object x) { return ((Item)x).GetAttributeValue("WeaponAttackValue"); };
                         
                        var def = new OLVColumn();
                        def.Text = "Defense";
                        def.AspectGetter = delegate (object x) { return ((Item)x).GetAttributeValue("WeaponDefendValue"); };

                        listView_items.Columns.Add(atk);
                        listView_items.Columns.Add(def);
                         
                    }
                    else if (splitFilter[0] == "BowRange" || splitFilter[0] == "ThrowRange")
                    {
                        bool exists = false;
                        foreach (OLVColumn col in listView_items.Columns)
                            if (col.Text == "Range") exists = true; 


                        if (!exists)
                        { 
                            var atk = new OLVColumn();
                            atk.Text = "Attack";
                            atk.AspectGetter = delegate (object x) { return ((Item)x).GetAttributeValue("ThrowAttackValue"); };

                            var range = new OLVColumn();
                            range.Text = "Range";
                            range.AspectGetter = delegate (object x) {
                                int rangeValue = ((Item)x).GetAttributeValue("BowRange");
                                if (rangeValue == 0) rangeValue = ((Item)x).GetAttributeValue("ThrowRange");
                                return rangeValue; 
                            };

                            listView_items.Columns.Add(atk);
                            listView_items.Columns.Add(range);
                        }
                    }

                }
                else
                {

                    items.AddRange(_dataReader.GetItemsByFlag(_dataReader.items, filter));
                    // Flag
                    if ( filter == "Shield" )
                    { 
                        var def = new OLVColumn();
                        def.Text = "Defense";
                        def.AspectGetter = delegate (object x) { return ((Item)x).GetAttributeValue("ShieldDefendValue"); };

                        listView_items.Columns.Add(def);
                    }
                    else if ( filter == "Rune")
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

                        listView_items.Columns.Add(charges);
                    }
                    else if ( filter == "Food" )
                    {
                        var regen = new OLVColumn();
                        regen.Text = "Regen";
                        regen.AspectGetter = delegate (object x) { return ((Item)x).GetAttributeValue("Nutrition"); };
                        regen.AspectToStringConverter = delegate (object x)
                        {
                            TimeSpan t = TimeSpan.FromSeconds( ((double)((int)x * 2) / 10) * 60);

                            string answer = string.Format("{0:D2}:{1:D2}",
                                            t.Minutes,
                                            t.Seconds);

                            return answer;
                        };

                        var nutritionByWeight = new OLVColumn();
                        nutritionByWeight.Text = "W/R";
                        nutritionByWeight.AspectGetter = delegate (object x) {
                            var weight = ((Item)x).GetAttributeValue("Weight");
                            var nutrition = (double)((Item)x).GetAttributeValue("Nutrition");

                            return Math.Round(weight / nutrition, 2);
                        };
                        nutritionByWeight.ToolTipText = "Weight / regen. Lower values mean better regen by weight";

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

                            return Math.Round( price / nutrition, 2);
                        };
                        nutritionByPrice.AspectToStringConverter = delegate (object x)
                        {
                            var value = (double)x;

                            if (value > 100) return "N/A";
                            else return value.ToString();
                        };
                        nutritionByPrice.ToolTipText = "Cheapest vendor price / regen. Lower values mean it's more priceworthy";


                        listView_items.Columns.Add(regen);
                        listView_items.Columns.Add(nutritionByPrice);
                        listView_items.Columns.Add(nutritionByWeight);
                    }
                }

            } 

            return items;
        } 

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
              
            // Column headers + attribute names
            string[] col_armorArm = { "Arm", "Weight" };
            string[] col_shield = { "Shield Def.", "Weight" };
            string[] col_weapon = { "Attack", "Weapon Def.", "Weight" };
            string[] col_bow = { "Bow Range", "Bow Ammo" };
            string[] col_throw = { "Throw Attack", "Throw Range", "Throw Def.", "Throw Effect", "Throw Effect Str.", "Throw Fragility", "Weight" };
            string[] col_ammo = { "Ammo Attack", "Ammo Effect", "Effect Strength", "Ammo Type", "Weight" };


            if (items_comboBox_itemCategory.Text == "All") PopulateItemList("Take");

            if (items_comboBox_itemCategory.Text == "Helmet") PopulateItemList("BodyPosition=1");
            if (items_comboBox_itemCategory.Text == "Armor") PopulateItemList("BodyPosition=4");
            if (items_comboBox_itemCategory.Text == "Legs") PopulateItemList("BodyPosition=7");
            if (items_comboBox_itemCategory.Text == "Boots") PopulateItemList("BodyPosition=8");
            if (items_comboBox_itemCategory.Text == "Necklace") PopulateItemList("BodyPosition=2");
            if (items_comboBox_itemCategory.Text == "Ring") PopulateItemList("BodyPosition=9");  
            if (items_comboBox_itemCategory.Text == "Shield") PopulateItemList("Shield");


            if (items_comboBox_itemCategory.Text == "Axe") PopulateItemList("WeaponType=3");
            if (items_comboBox_itemCategory.Text == "Club") PopulateItemList("WeaponType=2");
            if (items_comboBox_itemCategory.Text == "Sword") PopulateItemList("WeaponType=1");
            if (items_comboBox_itemCategory.Text == "Distance") PopulateItemList("BowRange=*,ThrowRange=*"); 
            if (items_comboBox_itemCategory.Text == "Ammo") PopulateItemList("AmmoType=*");

            if (items_comboBox_itemCategory.Text == "Equippable Container") PopulateItemList("BodyPosition=3");
            if (items_comboBox_itemCategory.Text == "Runes") PopulateItemList("Rune");
            if (items_comboBox_itemCategory.Text == "Food") PopulateItemList("Food");

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

                var player = new Player(name, alert);
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
                {
                    Player p = new Player(diag.PlayerName);
                    p.Alert = diag.Alert;
                    _trackedPlayers.Add(p);
                    _trackedPlayers = _trackedPlayers.OrderBy(pp => pp.Name).ToList();
                    LoginTrackerUpdate();
                }
            }
        }

        private void LoginTrackerCheckOnline()
        {
            DataTable table = (DataTable)loginAlert_dataGridView.DataSource;
            List<Player> alerts = new List<Player>();
            


            foreach ( var p in _trackedPlayers )
            {
                bool needsUpdate = false;

                // Some stuff for when user has put the tracked players name in the wrong casing
                var onlinePlayer = _currentlyOnline.Where(s => s.Equals(p.Name, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();

                if (onlinePlayer != null)
                {
                    if (!onlinePlayer.Equals(p.Name, StringComparison.Ordinal))
                    {
                        // If wrong casing, fix it
                        p.Name = onlinePlayer;
                        needsUpdate = true;
                    }

                    DataRow[] select = table.Select("Name='" + onlinePlayer + "'");
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
                        DataRow r = (select[0]);
                        if (select[0] != null)
                        {
                            var index = table.Rows.IndexOf(r);
                            loginAlert_dataGridView.Rows[index].DefaultCellStyle.BackColor = Color.PaleGreen;

                            if (needsUpdate)
                                loginAlert_dataGridView.Rows[index].Cells[0].Value = onlinePlayer; // Already updated name in _trackedPlayers, also update i

                        }
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

        private void LoginAlert(List<Player> players)
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
                            Console.WriteLine(players[i].Alert);
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
                    }
                }
            }
        }

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
            if (_currentlyOnline.Contains(loginAlert_dataGridView.Rows[index].Cells[0].Value))
                loginAlert_dataGridView.Rows[index].DefaultCellStyle.BackColor = Color.PaleGreen;
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

        private void LoginAlertChangeAlertDialog(Player player)
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

        private class Player
        {
            public string Name;
            public string Alert;        // Leaving this uninitialized is good for DataTable.WriteXml, avoids writing empty xml nodes

            public Player(string Name, string Alert = null)
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

            _trayIcon.Visible = false;
            Settings.Default.Save();
            Environment.Exit(0);
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
