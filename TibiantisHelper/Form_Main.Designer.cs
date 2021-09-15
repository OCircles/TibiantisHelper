namespace TibiantisHelper
{
    partial class Form_Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_Main));
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.tray_contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.header_linkLabel_playersOnline = new System.Windows.Forms.LinkLabel();
            this.header_label_onlineStatus = new System.Windows.Forms.Label();
            this.header_pictureBox = new System.Windows.Forms.PictureBox();
            this.header_panel_vocations = new System.Windows.Forms.Panel();
            this.header_vocation_promo_checkBox = new System.Windows.Forms.CheckBox();
            this.header_vocation_label4 = new System.Windows.Forms.Label();
            this.header_vocation_label3 = new System.Windows.Forms.Label();
            this.header_vocation_label1 = new System.Windows.Forms.Label();
            this.header_vocation_label2 = new System.Windows.Forms.Label();
            this.header_vocation_comboBox = new System.Windows.Forms.ComboBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage_accounts = new System.Windows.Forms.TabPage();
            this.tab_Accounts = new TibiantisHelper.Tabs.Tab_Accounts();
            this.tabPage_timers = new System.Windows.Forms.TabPage();
            this.tab_Timers1 = new TibiantisHelper.Tabs.Tab_Timers();
            this.tabPage_calculator = new System.Windows.Forms.TabPage();
            this.splitContainer14 = new System.Windows.Forms.SplitContainer();
            this.calculator_listBox = new System.Windows.Forms.ListBox();
            this.splitContainer15 = new System.Windows.Forms.SplitContainer();
            this.calculator_tabControl = new System.Windows.Forms.TabControl();
            this.calculator_textBox_result = new System.Windows.Forms.TextBox();
            this.tabPage_items = new System.Windows.Forms.TabPage();
            this.tab_Items1 = new TibiantisHelper.Tabs.Tab_Items();
            this.tabPage_spells = new System.Windows.Forms.TabPage();
            this.tab_Spells1 = new TibiantisHelper.Tabs.Tab_Spells();
            this.tabPage_npc = new System.Windows.Forms.TabPage();
            this.tab_NPC1 = new TibiantisHelper.Tabs.Tab_NPC();
            this.tabPage_monsters = new System.Windows.Forms.TabPage();
            this.tab_Monsters1 = new TibiantisHelper.Tabs.Tab_Monsters();
            this.tabPage_loginAlert = new System.Windows.Forms.TabPage();
            this.tabPage_map = new System.Windows.Forms.TabPage();
            this.tab_Map1 = new TibiantisHelper.Tab_Map();
            this.tabPage_Raids = new System.Windows.Forms.TabPage();
            this.tab_Raids1 = new TibiantisHelper.Tabs.Tab_Raids();
            this.timers_contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.bedmageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.idleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.food002000ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.timerNetworkStuff = new System.Windows.Forms.Timer(this.components);
            this.header_linkLabel_tibiantisHome = new System.Windows.Forms.LinkLabel();
            this.header_linkLabel_tibiantisInfo = new System.Windows.Forms.LinkLabel();
            this.header_linkLabel_discord = new System.Windows.Forms.LinkLabel();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.timerTrayFlash = new System.Windows.Forms.Timer(this.components);
            this.tab_LoginAlert1 = new TibiantisHelper.Tabs.Tab_LoginAlert();
            ((System.ComponentModel.ISupportInitialize)(this.header_pictureBox)).BeginInit();
            this.header_panel_vocations.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage_accounts.SuspendLayout();
            this.tabPage_timers.SuspendLayout();
            this.tabPage_calculator.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer14)).BeginInit();
            this.splitContainer14.Panel1.SuspendLayout();
            this.splitContainer14.Panel2.SuspendLayout();
            this.splitContainer14.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer15)).BeginInit();
            this.splitContainer15.Panel1.SuspendLayout();
            this.splitContainer15.Panel2.SuspendLayout();
            this.splitContainer15.SuspendLayout();
            this.tabPage_items.SuspendLayout();
            this.tabPage_spells.SuspendLayout();
            this.tabPage_npc.SuspendLayout();
            this.tabPage_monsters.SuspendLayout();
            this.tabPage_loginAlert.SuspendLayout();
            this.tabPage_map.SuspendLayout();
            this.tabPage_Raids.SuspendLayout();
            this.timers_contextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.BalloonTipText = "Testy";
            this.notifyIcon1.ContextMenuStrip = this.tray_contextMenuStrip;
            this.notifyIcon1.Text = "Tibiantis Helper";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.BalloonTipClicked += new System.EventHandler(this.notifyIcon1_BalloonTipClicked);
            this.notifyIcon1.DoubleClick += new System.EventHandler(this.notifyIcon1_DoubleClick);
            // 
            // tray_contextMenuStrip
            // 
            this.tray_contextMenuStrip.Name = "tray_contextMenuStrip";
            this.tray_contextMenuStrip.Size = new System.Drawing.Size(61, 4);
            this.tray_contextMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(this.tray_contextMenuStrip_Opening);
            // 
            // header_linkLabel_playersOnline
            // 
            this.header_linkLabel_playersOnline.AutoSize = true;
            this.header_linkLabel_playersOnline.Location = new System.Drawing.Point(100, 35);
            this.header_linkLabel_playersOnline.Name = "header_linkLabel_playersOnline";
            this.header_linkLabel_playersOnline.Size = new System.Drawing.Size(60, 13);
            this.header_linkLabel_playersOnline.TabIndex = 5;
            this.header_linkLabel_playersOnline.TabStop = true;
            this.header_linkLabel_playersOnline.Text = "0 players";
            this.toolTip1.SetToolTip(this.header_linkLabel_playersOnline, "Right-click to open in browser");
            this.header_linkLabel_playersOnline.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.header_linkLabel_playersOnline_LinkClicked);
            // 
            // header_label_onlineStatus
            // 
            this.header_label_onlineStatus.AutoSize = true;
            this.header_label_onlineStatus.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.header_label_onlineStatus.Location = new System.Drawing.Point(100, 19);
            this.header_label_onlineStatus.Name = "header_label_onlineStatus";
            this.header_label_onlineStatus.Size = new System.Drawing.Size(50, 13);
            this.header_label_onlineStatus.TabIndex = 4;
            this.header_label_onlineStatus.Text = "Offline";
            // 
            // header_pictureBox
            // 
            this.header_pictureBox.Image = ((System.Drawing.Image)(resources.GetObject("header_pictureBox.Image")));
            this.header_pictureBox.Location = new System.Drawing.Point(2, 5);
            this.header_pictureBox.Name = "header_pictureBox";
            this.header_pictureBox.Size = new System.Drawing.Size(98, 59);
            this.header_pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.header_pictureBox.TabIndex = 0;
            this.header_pictureBox.TabStop = false;
            // 
            // header_panel_vocations
            // 
            this.header_panel_vocations.Controls.Add(this.header_vocation_promo_checkBox);
            this.header_panel_vocations.Controls.Add(this.header_vocation_label4);
            this.header_panel_vocations.Controls.Add(this.header_vocation_label3);
            this.header_panel_vocations.Controls.Add(this.header_vocation_label1);
            this.header_panel_vocations.Controls.Add(this.header_vocation_label2);
            this.header_panel_vocations.Location = new System.Drawing.Point(269, 4);
            this.header_panel_vocations.Name = "header_panel_vocations";
            this.header_panel_vocations.Size = new System.Drawing.Size(128, 60);
            this.header_panel_vocations.TabIndex = 8;
            // 
            // header_vocation_promo_checkBox
            // 
            this.header_vocation_promo_checkBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.header_vocation_promo_checkBox.Appearance = System.Windows.Forms.Appearance.Button;
            this.header_vocation_promo_checkBox.FlatAppearance.BorderSize = 5;
            this.header_vocation_promo_checkBox.Location = new System.Drawing.Point(3, 3);
            this.header_vocation_promo_checkBox.Name = "header_vocation_promo_checkBox";
            this.header_vocation_promo_checkBox.Size = new System.Drawing.Size(122, 23);
            this.header_vocation_promo_checkBox.TabIndex = 10;
            this.header_vocation_promo_checkBox.Text = "Vocation";
            this.header_vocation_promo_checkBox.UseVisualStyleBackColor = true;
            this.header_vocation_promo_checkBox.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // header_vocation_label4
            // 
            this.header_vocation_label4.AutoSize = true;
            this.header_vocation_label4.Location = new System.Drawing.Point(78, 44);
            this.header_vocation_label4.Name = "header_vocation_label4";
            this.header_vocation_label4.Size = new System.Drawing.Size(30, 13);
            this.header_vocation_label4.TabIndex = 11;
            this.header_vocation_label4.Text = "0/m";
            // 
            // header_vocation_label3
            // 
            this.header_vocation_label3.AutoSize = true;
            this.header_vocation_label3.Location = new System.Drawing.Point(3, 44);
            this.header_vocation_label3.Name = "header_vocation_label3";
            this.header_vocation_label3.Size = new System.Drawing.Size(63, 13);
            this.header_vocation_label3.TabIndex = 10;
            this.header_vocation_label3.Text = "MP Regen";
            // 
            // header_vocation_label1
            // 
            this.header_vocation_label1.AutoSize = true;
            this.header_vocation_label1.Location = new System.Drawing.Point(3, 27);
            this.header_vocation_label1.Name = "header_vocation_label1";
            this.header_vocation_label1.Size = new System.Drawing.Size(62, 13);
            this.header_vocation_label1.TabIndex = 9;
            this.header_vocation_label1.Text = "HP Regen";
            // 
            // header_vocation_label2
            // 
            this.header_vocation_label2.AutoSize = true;
            this.header_vocation_label2.Location = new System.Drawing.Point(78, 27);
            this.header_vocation_label2.Name = "header_vocation_label2";
            this.header_vocation_label2.Size = new System.Drawing.Size(30, 13);
            this.header_vocation_label2.TabIndex = 7;
            this.header_vocation_label2.Text = "0/m";
            // 
            // header_vocation_comboBox
            // 
            this.header_vocation_comboBox.BackColor = System.Drawing.SystemColors.Window;
            this.header_vocation_comboBox.DropDownHeight = 250;
            this.header_vocation_comboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.header_vocation_comboBox.Font = new System.Drawing.Font("Verdana", 24F);
            this.header_vocation_comboBox.IntegralHeight = false;
            this.header_vocation_comboBox.ItemHeight = 38;
            this.header_vocation_comboBox.Location = new System.Drawing.Point(195, 13);
            this.header_vocation_comboBox.Name = "header_vocation_comboBox";
            this.header_vocation_comboBox.Size = new System.Drawing.Size(68, 46);
            this.header_vocation_comboBox.TabIndex = 2;
            this.header_vocation_comboBox.SelectedIndexChanged += new System.EventHandler(this.comboBox_vocation_SelectedIndexChanged);
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.Color.Transparent;
            this.panel2.Controls.Add(this.tabControl1);
            this.panel2.Location = new System.Drawing.Point(0, 70);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(961, 451);
            this.panel2.TabIndex = 1;
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage_accounts);
            this.tabControl1.Controls.Add(this.tabPage_timers);
            this.tabControl1.Controls.Add(this.tabPage_calculator);
            this.tabControl1.Controls.Add(this.tabPage_items);
            this.tabControl1.Controls.Add(this.tabPage_spells);
            this.tabControl1.Controls.Add(this.tabPage_npc);
            this.tabControl1.Controls.Add(this.tabPage_monsters);
            this.tabControl1.Controls.Add(this.tabPage_loginAlert);
            this.tabControl1.Controls.Add(this.tabPage_map);
            this.tabControl1.Controls.Add(this.tabPage_Raids);
            this.tabControl1.Location = new System.Drawing.Point(14, 9);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(933, 432);
            this.tabControl1.SizeMode = System.Windows.Forms.TabSizeMode.FillToRight;
            this.tabControl1.TabIndex = 0;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPage_accounts
            // 
            this.tabPage_accounts.Controls.Add(this.tab_Accounts);
            this.tabPage_accounts.Location = new System.Drawing.Point(4, 22);
            this.tabPage_accounts.Name = "tabPage_accounts";
            this.tabPage_accounts.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_accounts.Size = new System.Drawing.Size(925, 406);
            this.tabPage_accounts.TabIndex = 0;
            this.tabPage_accounts.Text = "Accounts";
            this.tabPage_accounts.UseVisualStyleBackColor = true;
            // 
            // tab_Accounts
            // 
            this.tab_Accounts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tab_Accounts.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tab_Accounts.Location = new System.Drawing.Point(3, 3);
            this.tab_Accounts.Name = "tab_Accounts";
            this.tab_Accounts.Size = new System.Drawing.Size(919, 400);
            this.tab_Accounts.TabIndex = 0;
            // 
            // tabPage_timers
            // 
            this.tabPage_timers.Controls.Add(this.tab_Timers1);
            this.tabPage_timers.Location = new System.Drawing.Point(4, 22);
            this.tabPage_timers.Name = "tabPage_timers";
            this.tabPage_timers.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_timers.Size = new System.Drawing.Size(925, 406);
            this.tabPage_timers.TabIndex = 1;
            this.tabPage_timers.Text = "Timers";
            this.tabPage_timers.UseVisualStyleBackColor = true;
            // 
            // tab_Timers1
            // 
            this.tab_Timers1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tab_Timers1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tab_Timers1.Location = new System.Drawing.Point(3, 3);
            this.tab_Timers1.Name = "tab_Timers1";
            this.tab_Timers1.Size = new System.Drawing.Size(919, 400);
            this.tab_Timers1.TabIndex = 0;
            // 
            // tabPage_calculator
            // 
            this.tabPage_calculator.AutoScroll = true;
            this.tabPage_calculator.Controls.Add(this.splitContainer14);
            this.tabPage_calculator.Location = new System.Drawing.Point(4, 22);
            this.tabPage_calculator.Name = "tabPage_calculator";
            this.tabPage_calculator.Size = new System.Drawing.Size(925, 406);
            this.tabPage_calculator.TabIndex = 7;
            this.tabPage_calculator.Text = "Calculator";
            this.tabPage_calculator.UseVisualStyleBackColor = true;
            // 
            // splitContainer14
            // 
            this.splitContainer14.BackColor = System.Drawing.Color.Transparent;
            this.splitContainer14.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer14.Location = new System.Drawing.Point(0, 0);
            this.splitContainer14.Name = "splitContainer14";
            // 
            // splitContainer14.Panel1
            // 
            this.splitContainer14.Panel1.Controls.Add(this.calculator_listBox);
            // 
            // splitContainer14.Panel2
            // 
            this.splitContainer14.Panel2.Controls.Add(this.splitContainer15);
            this.splitContainer14.Size = new System.Drawing.Size(925, 406);
            this.splitContainer14.SplitterDistance = 174;
            this.splitContainer14.TabIndex = 13;
            // 
            // calculator_listBox
            // 
            this.calculator_listBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.calculator_listBox.FormattingEnabled = true;
            this.calculator_listBox.Location = new System.Drawing.Point(0, 0);
            this.calculator_listBox.Name = "calculator_listBox";
            this.calculator_listBox.Size = new System.Drawing.Size(174, 406);
            this.calculator_listBox.TabIndex = 0;
            this.calculator_listBox.SelectedIndexChanged += new System.EventHandler(this.calculator_listBox_SelectedIndexChanged);
            // 
            // splitContainer15
            // 
            this.splitContainer15.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer15.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer15.Location = new System.Drawing.Point(0, 0);
            this.splitContainer15.Name = "splitContainer15";
            this.splitContainer15.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer15.Panel1
            // 
            this.splitContainer15.Panel1.Controls.Add(this.calculator_tabControl);
            // 
            // splitContainer15.Panel2
            // 
            this.splitContainer15.Panel2.Controls.Add(this.calculator_textBox_result);
            this.splitContainer15.Size = new System.Drawing.Size(747, 406);
            this.splitContainer15.SplitterDistance = 307;
            this.splitContainer15.TabIndex = 0;
            // 
            // calculator_tabControl
            // 
            this.calculator_tabControl.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.calculator_tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.calculator_tabControl.ItemSize = new System.Drawing.Size(0, 1);
            this.calculator_tabControl.Location = new System.Drawing.Point(0, 0);
            this.calculator_tabControl.Name = "calculator_tabControl";
            this.calculator_tabControl.SelectedIndex = 0;
            this.calculator_tabControl.Size = new System.Drawing.Size(747, 307);
            this.calculator_tabControl.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.calculator_tabControl.TabIndex = 12;
            // 
            // calculator_textBox_result
            // 
            this.calculator_textBox_result.BackColor = System.Drawing.SystemColors.ControlLight;
            this.calculator_textBox_result.Dock = System.Windows.Forms.DockStyle.Fill;
            this.calculator_textBox_result.Location = new System.Drawing.Point(0, 0);
            this.calculator_textBox_result.Multiline = true;
            this.calculator_textBox_result.Name = "calculator_textBox_result";
            this.calculator_textBox_result.ReadOnly = true;
            this.calculator_textBox_result.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.calculator_textBox_result.Size = new System.Drawing.Size(747, 95);
            this.calculator_textBox_result.TabIndex = 8;
            // 
            // tabPage_items
            // 
            this.tabPage_items.Controls.Add(this.tab_Items1);
            this.tabPage_items.Location = new System.Drawing.Point(4, 22);
            this.tabPage_items.Name = "tabPage_items";
            this.tabPage_items.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_items.Size = new System.Drawing.Size(925, 406);
            this.tabPage_items.TabIndex = 2;
            this.tabPage_items.Text = "Items";
            this.tabPage_items.UseVisualStyleBackColor = true;
            // 
            // tab_Items1
            // 
            this.tab_Items1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tab_Items1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tab_Items1.Location = new System.Drawing.Point(3, 3);
            this.tab_Items1.Name = "tab_Items1";
            this.tab_Items1.Size = new System.Drawing.Size(919, 400);
            this.tab_Items1.TabIndex = 0;
            // 
            // tabPage_spells
            // 
            this.tabPage_spells.Controls.Add(this.tab_Spells1);
            this.tabPage_spells.Location = new System.Drawing.Point(4, 22);
            this.tabPage_spells.Name = "tabPage_spells";
            this.tabPage_spells.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_spells.Size = new System.Drawing.Size(925, 406);
            this.tabPage_spells.TabIndex = 6;
            this.tabPage_spells.Text = "Spells";
            this.tabPage_spells.UseVisualStyleBackColor = true;
            // 
            // tab_Spells1
            // 
            this.tab_Spells1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tab_Spells1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tab_Spells1.Location = new System.Drawing.Point(3, 3);
            this.tab_Spells1.Name = "tab_Spells1";
            this.tab_Spells1.Size = new System.Drawing.Size(919, 400);
            this.tab_Spells1.TabIndex = 0;
            // 
            // tabPage_npc
            // 
            this.tabPage_npc.Controls.Add(this.tab_NPC1);
            this.tabPage_npc.Location = new System.Drawing.Point(4, 22);
            this.tabPage_npc.Name = "tabPage_npc";
            this.tabPage_npc.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_npc.Size = new System.Drawing.Size(925, 406);
            this.tabPage_npc.TabIndex = 3;
            this.tabPage_npc.Text = "NPC";
            this.tabPage_npc.UseVisualStyleBackColor = true;
            // 
            // tab_NPC1
            // 
            this.tab_NPC1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tab_NPC1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tab_NPC1.Location = new System.Drawing.Point(3, 3);
            this.tab_NPC1.Name = "tab_NPC1";
            this.tab_NPC1.Size = new System.Drawing.Size(919, 400);
            this.tab_NPC1.TabIndex = 0;
            // 
            // tabPage_monsters
            // 
            this.tabPage_monsters.Controls.Add(this.tab_Monsters1);
            this.tabPage_monsters.Location = new System.Drawing.Point(4, 22);
            this.tabPage_monsters.Name = "tabPage_monsters";
            this.tabPage_monsters.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_monsters.Size = new System.Drawing.Size(925, 406);
            this.tabPage_monsters.TabIndex = 4;
            this.tabPage_monsters.Text = "Monsters";
            this.tabPage_monsters.UseVisualStyleBackColor = true;
            // 
            // tab_Monsters1
            // 
            this.tab_Monsters1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tab_Monsters1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tab_Monsters1.Location = new System.Drawing.Point(3, 3);
            this.tab_Monsters1.Name = "tab_Monsters1";
            this.tab_Monsters1.Size = new System.Drawing.Size(919, 400);
            this.tab_Monsters1.TabIndex = 0;
            // 
            // tabPage_loginAlert
            // 
            this.tabPage_loginAlert.Controls.Add(this.tab_LoginAlert1);
            this.tabPage_loginAlert.Location = new System.Drawing.Point(4, 22);
            this.tabPage_loginAlert.Name = "tabPage_loginAlert";
            this.tabPage_loginAlert.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_loginAlert.Size = new System.Drawing.Size(925, 406);
            this.tabPage_loginAlert.TabIndex = 5;
            this.tabPage_loginAlert.Text = "Login Alert";
            this.tabPage_loginAlert.UseVisualStyleBackColor = true;
            // 
            // tabPage_map
            // 
            this.tabPage_map.Controls.Add(this.tab_Map1);
            this.tabPage_map.Location = new System.Drawing.Point(4, 22);
            this.tabPage_map.Name = "tabPage_map";
            this.tabPage_map.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_map.Size = new System.Drawing.Size(925, 406);
            this.tabPage_map.TabIndex = 9;
            this.tabPage_map.Text = "Map";
            this.tabPage_map.UseVisualStyleBackColor = true;
            // 
            // tab_Map1
            // 
            this.tab_Map1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tab_Map1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tab_Map1.Location = new System.Drawing.Point(3, 3);
            this.tab_Map1.Name = "tab_Map1";
            this.tab_Map1.Size = new System.Drawing.Size(919, 400);
            this.tab_Map1.TabIndex = 0;
            // 
            // tabPage_Raids
            // 
            this.tabPage_Raids.Controls.Add(this.tab_Raids1);
            this.tabPage_Raids.Location = new System.Drawing.Point(4, 22);
            this.tabPage_Raids.Name = "tabPage_Raids";
            this.tabPage_Raids.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_Raids.Size = new System.Drawing.Size(925, 406);
            this.tabPage_Raids.TabIndex = 10;
            this.tabPage_Raids.Text = "Raids";
            this.tabPage_Raids.UseVisualStyleBackColor = true;
            // 
            // tab_Raids1
            // 
            this.tab_Raids1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tab_Raids1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tab_Raids1.Location = new System.Drawing.Point(3, 3);
            this.tab_Raids1.Name = "tab_Raids1";
            this.tab_Raids1.Size = new System.Drawing.Size(919, 400);
            this.tab_Raids1.TabIndex = 0;
            // 
            // timers_contextMenuStrip
            // 
            this.timers_contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bedmageToolStripMenuItem,
            this.idleToolStripMenuItem,
            this.food002000ToolStripMenuItem});
            this.timers_contextMenuStrip.Name = "timers_contextMenuStrip";
            this.timers_contextMenuStrip.Size = new System.Drawing.Size(178, 70);
            // 
            // bedmageToolStripMenuItem
            // 
            this.bedmageToolStripMenuItem.Name = "bedmageToolStripMenuItem";
            this.bedmageToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.bedmageToolStripMenuItem.Text = "Bedmage (01:40:00)";
            // 
            // idleToolStripMenuItem
            // 
            this.idleToolStripMenuItem.Name = "idleToolStripMenuItem";
            this.idleToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.idleToolStripMenuItem.Text = "Idle (00:16:00)";
            // 
            // food002000ToolStripMenuItem
            // 
            this.food002000ToolStripMenuItem.Name = "food002000ToolStripMenuItem";
            this.food002000ToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.food002000ToolStripMenuItem.Text = "Food (00:20:00)";
            // 
            // timerNetworkStuff
            // 
            this.timerNetworkStuff.Enabled = true;
            this.timerNetworkStuff.Interval = 300000;
            this.timerNetworkStuff.Tick += new System.EventHandler(this.timerNetworkStuff_Tick);
            // 
            // header_linkLabel_tibiantisHome
            // 
            this.header_linkLabel_tibiantisHome.AutoSize = true;
            this.header_linkLabel_tibiantisHome.Location = new System.Drawing.Point(427, 16);
            this.header_linkLabel_tibiantisHome.Name = "header_linkLabel_tibiantisHome";
            this.header_linkLabel_tibiantisHome.Size = new System.Drawing.Size(54, 13);
            this.header_linkLabel_tibiantisHome.TabIndex = 9;
            this.header_linkLabel_tibiantisHome.TabStop = true;
            this.header_linkLabel_tibiantisHome.Text = "Tibiantis";
            this.header_linkLabel_tibiantisHome.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // header_linkLabel_tibiantisInfo
            // 
            this.header_linkLabel_tibiantisInfo.AutoSize = true;
            this.header_linkLabel_tibiantisInfo.Location = new System.Drawing.Point(427, 31);
            this.header_linkLabel_tibiantisInfo.Name = "header_linkLabel_tibiantisInfo";
            this.header_linkLabel_tibiantisInfo.Size = new System.Drawing.Size(79, 13);
            this.header_linkLabel_tibiantisInfo.TabIndex = 10;
            this.header_linkLabel_tibiantisInfo.TabStop = true;
            this.header_linkLabel_tibiantisInfo.Text = "Tibiantis.info";
            this.header_linkLabel_tibiantisInfo.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel2_LinkClicked);
            // 
            // header_linkLabel_discord
            // 
            this.header_linkLabel_discord.AutoSize = true;
            this.header_linkLabel_discord.Location = new System.Drawing.Point(427, 46);
            this.header_linkLabel_discord.Name = "header_linkLabel_discord";
            this.header_linkLabel_discord.Size = new System.Drawing.Size(101, 13);
            this.header_linkLabel_discord.TabIndex = 11;
            this.header_linkLabel_discord.TabStop = true;
            this.header_linkLabel_discord.Text = "Tibiantis Discord";
            this.header_linkLabel_discord.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel3_LinkClicked);
            // 
            // timerTrayFlash
            // 
            this.timerTrayFlash.Interval = 1000;
            this.timerTrayFlash.Tick += new System.EventHandler(this.timerTrayFlash_Tick);
            // 
            // tab_LoginAlert1
            // 
            this.tab_LoginAlert1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tab_LoginAlert1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tab_LoginAlert1.Location = new System.Drawing.Point(3, 3);
            this.tab_LoginAlert1.Name = "tab_LoginAlert1";
            this.tab_LoginAlert1.Size = new System.Drawing.Size(919, 400);
            this.tab_LoginAlert1.TabIndex = 0;
            // 
            // Form_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.PapayaWhip;
            this.ClientSize = new System.Drawing.Size(961, 521);
            this.Controls.Add(this.header_linkLabel_discord);
            this.Controls.Add(this.header_linkLabel_tibiantisInfo);
            this.Controls.Add(this.header_linkLabel_tibiantisHome);
            this.Controls.Add(this.header_panel_vocations);
            this.Controls.Add(this.header_linkLabel_playersOnline);
            this.Controls.Add(this.header_label_onlineStatus);
            this.Controls.Add(this.header_vocation_comboBox);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.header_pictureBox);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "Form_Main";
            this.ShowIcon = false;
            this.Text = "Tibiantis Helper v0.96";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form_Main_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form_Main_KeyUp);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.header_pictureBox)).EndInit();
            this.header_panel_vocations.ResumeLayout(false);
            this.header_panel_vocations.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage_accounts.ResumeLayout(false);
            this.tabPage_timers.ResumeLayout(false);
            this.tabPage_calculator.ResumeLayout(false);
            this.splitContainer14.Panel1.ResumeLayout(false);
            this.splitContainer14.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer14)).EndInit();
            this.splitContainer14.ResumeLayout(false);
            this.splitContainer15.Panel1.ResumeLayout(false);
            this.splitContainer15.Panel2.ResumeLayout(false);
            this.splitContainer15.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer15)).EndInit();
            this.splitContainer15.ResumeLayout(false);
            this.tabPage_items.ResumeLayout(false);
            this.tabPage_spells.ResumeLayout(false);
            this.tabPage_npc.ResumeLayout(false);
            this.tabPage_monsters.ResumeLayout(false);
            this.tabPage_loginAlert.ResumeLayout(false);
            this.tabPage_map.ResumeLayout(false);
            this.tabPage_Raids.ResumeLayout(false);
            this.timers_contextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TabPage tabPage_accounts;
        private System.Windows.Forms.PictureBox header_pictureBox;
        private System.Windows.Forms.TabPage tabPage_npc;
        private System.Windows.Forms.LinkLabel header_linkLabel_playersOnline;
        private System.Windows.Forms.Label header_label_onlineStatus;
        private System.Windows.Forms.Timer timerNetworkStuff;
        private System.Windows.Forms.Label header_vocation_label2;
        private System.Windows.Forms.Panel header_panel_vocations;
        private System.Windows.Forms.Label header_vocation_label4;
        private System.Windows.Forms.Label header_vocation_label3;
        private System.Windows.Forms.Label header_vocation_label1;
        private System.Windows.Forms.TabPage tabPage_calculator;
        private System.Windows.Forms.ContextMenuStrip tray_contextMenuStrip;
        private System.Windows.Forms.ContextMenuStrip timers_contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem bedmageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem idleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem food002000ToolStripMenuItem;
        private System.Windows.Forms.SplitContainer splitContainer14;
        private System.Windows.Forms.SplitContainer splitContainer15;
        private System.Windows.Forms.TabControl calculator_tabControl;
        private System.Windows.Forms.ListBox calculator_listBox;
        private System.Windows.Forms.LinkLabel header_linkLabel_tibiantisHome;
        private System.Windows.Forms.LinkLabel header_linkLabel_tibiantisInfo;
        private System.Windows.Forms.LinkLabel header_linkLabel_discord;
        private System.Windows.Forms.ToolTip toolTip1;
        public System.Windows.Forms.TextBox calculator_textBox_result;
        public System.Windows.Forms.TabControl tabControl1;
        public System.Windows.Forms.CheckBox header_vocation_promo_checkBox;
        public System.Windows.Forms.ComboBox header_vocation_comboBox;
        private System.Windows.Forms.Timer timerTrayFlash;
        private System.Windows.Forms.TabPage tabPage_map;
        private Tab_Map tab_Map1;
        private System.Windows.Forms.TabPage tabPage_Raids;
        private Tabs.Tab_Raids tab_Raids1;
        private Tabs.Tab_Accounts tab_Accounts;
        public System.Windows.Forms.TabPage tabPage_timers;
        public Tabs.Tab_Timers tab_Timers1;
        public System.Windows.Forms.TabPage tabPage_monsters;
        public System.Windows.Forms.TabPage tabPage_items;
        public Tabs.Tab_Items tab_Items1;
        public Tabs.Tab_Monsters tab_Monsters1;
        public System.Windows.Forms.TabPage tabPage_spells;
        public Tabs.Tab_Spells tab_Spells1;
        private Tabs.Tab_NPC tab_NPC1;
        public System.Windows.Forms.TabPage tabPage_loginAlert;
        public Tabs.Tab_LoginAlert tab_LoginAlert1;
    }
}

