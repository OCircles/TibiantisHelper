
namespace TibiantisHelper.Tabs
{
    partial class Tab_Timers
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Tab_Timers));
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.textBox_info = new System.Windows.Forms.TextBox();
            this.groupBox_settings = new System.Windows.Forms.GroupBox();
            this.checkBox_trayOpenOnClick = new System.Windows.Forms.CheckBox();
            this.textBox_soundPath = new System.Windows.Forms.TextBox();
            this.button_browseSound = new System.Windows.Forms.Button();
            this.checkBox_playSound = new System.Windows.Forms.CheckBox();
            this.checkBox_trayNotification = new System.Windows.Forms.CheckBox();
            this.button_addTimer = new System.Windows.Forms.Button();
            this.button_presets = new System.Windows.Forms.Button();
            this.contextMenuStrip_presets = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.bedmageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.idleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.food002000ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.groupBox_settings.SuspendLayout();
            this.contextMenuStrip_presets.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer.IsSplitterFixed = true;
            this.splitContainer.Location = new System.Drawing.Point(0, 0);
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.textBox_info);
            this.splitContainer.Panel1.Controls.Add(this.groupBox_settings);
            this.splitContainer.Panel1.Controls.Add(this.button_addTimer);
            this.splitContainer.Panel1.Controls.Add(this.button_presets);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.BackColor = System.Drawing.SystemColors.Window;
            this.splitContainer.Size = new System.Drawing.Size(737, 400);
            this.splitContainer.SplitterDistance = 235;
            this.splitContainer.SplitterWidth = 5;
            this.splitContainer.TabIndex = 4;
            // 
            // textBox_info
            // 
            this.textBox_info.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_info.Location = new System.Drawing.Point(0, 154);
            this.textBox_info.Multiline = true;
            this.textBox_info.Name = "textBox_info";
            this.textBox_info.ReadOnly = true;
            this.textBox_info.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox_info.Size = new System.Drawing.Size(231, 243);
            this.textBox_info.TabIndex = 5;
            this.textBox_info.Text = resources.GetString("textBox_info.Text");
            // 
            // groupBox_settings
            // 
            this.groupBox_settings.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox_settings.Controls.Add(this.checkBox_trayOpenOnClick);
            this.groupBox_settings.Controls.Add(this.textBox_soundPath);
            this.groupBox_settings.Controls.Add(this.button_browseSound);
            this.groupBox_settings.Controls.Add(this.checkBox_playSound);
            this.groupBox_settings.Controls.Add(this.checkBox_trayNotification);
            this.groupBox_settings.Location = new System.Drawing.Point(3, 3);
            this.groupBox_settings.Name = "groupBox_settings";
            this.groupBox_settings.Size = new System.Drawing.Size(228, 118);
            this.groupBox_settings.TabIndex = 4;
            this.groupBox_settings.TabStop = false;
            this.groupBox_settings.Text = "Settings";
            // 
            // checkBox_trayOpenOnClick
            // 
            this.checkBox_trayOpenOnClick.AutoSize = true;
            this.checkBox_trayOpenOnClick.Checked = global::TibiantisHelper.Properties.Settings.Default.NotifTimerOpen;
            this.checkBox_trayOpenOnClick.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::TibiantisHelper.Properties.Settings.Default, "NotifTimerOpen", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.checkBox_trayOpenOnClick.Location = new System.Drawing.Point(7, 43);
            this.checkBox_trayOpenOnClick.Name = "checkBox_trayOpenOnClick";
            this.checkBox_trayOpenOnClick.Size = new System.Drawing.Size(171, 17);
            this.checkBox_trayOpenOnClick.TabIndex = 6;
            this.checkBox_trayOpenOnClick.Text = "Tray Notifs Open on Click";
            this.checkBox_trayOpenOnClick.UseVisualStyleBackColor = true;
            this.checkBox_trayOpenOnClick.CheckedChanged += new System.EventHandler(this.checkBox_trayOpenOnClick_CheckedChanged);
            // 
            // textBox_soundPath
            // 
            this.textBox_soundPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_soundPath.Location = new System.Drawing.Point(5, 91);
            this.textBox_soundPath.Name = "textBox_soundPath";
            this.textBox_soundPath.ReadOnly = true;
            this.textBox_soundPath.Size = new System.Drawing.Size(140, 21);
            this.textBox_soundPath.TabIndex = 4;
            // 
            // button_browseSound
            // 
            this.button_browseSound.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_browseSound.Location = new System.Drawing.Point(152, 89);
            this.button_browseSound.Name = "button_browseSound";
            this.button_browseSound.Size = new System.Drawing.Size(66, 23);
            this.button_browseSound.TabIndex = 5;
            this.button_browseSound.Text = "Browse";
            this.button_browseSound.UseVisualStyleBackColor = true;
            this.button_browseSound.Click += new System.EventHandler(this.button_browseSound_Click);
            // 
            // checkBox_playSound
            // 
            this.checkBox_playSound.AutoSize = true;
            this.checkBox_playSound.Checked = global::TibiantisHelper.Properties.Settings.Default.TimerNotifSound;
            this.checkBox_playSound.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::TibiantisHelper.Properties.Settings.Default, "TimerNotifSound", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.checkBox_playSound.Location = new System.Drawing.Point(7, 66);
            this.checkBox_playSound.Name = "checkBox_playSound";
            this.checkBox_playSound.Size = new System.Drawing.Size(90, 17);
            this.checkBox_playSound.TabIndex = 1;
            this.checkBox_playSound.Text = "Play Sound";
            this.checkBox_playSound.UseVisualStyleBackColor = true;
            this.checkBox_playSound.CheckedChanged += new System.EventHandler(this.checkBox_playSound_CheckedChanged);
            // 
            // checkBox_trayNotification
            // 
            this.checkBox_trayNotification.AutoSize = true;
            this.checkBox_trayNotification.Checked = global::TibiantisHelper.Properties.Settings.Default.TimerNotifTray;
            this.checkBox_trayNotification.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_trayNotification.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::TibiantisHelper.Properties.Settings.Default, "TimerNotifTray", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.checkBox_trayNotification.Location = new System.Drawing.Point(7, 20);
            this.checkBox_trayNotification.Name = "checkBox_trayNotification";
            this.checkBox_trayNotification.Size = new System.Drawing.Size(118, 17);
            this.checkBox_trayNotification.TabIndex = 0;
            this.checkBox_trayNotification.Text = "Tray Notification";
            this.checkBox_trayNotification.UseVisualStyleBackColor = true;
            this.checkBox_trayNotification.CheckedChanged += new System.EventHandler(this.checkBox_trayNotification_CheckedChanged);
            // 
            // button_addTimer
            // 
            this.button_addTimer.Location = new System.Drawing.Point(3, 125);
            this.button_addTimer.Name = "button_addTimer";
            this.button_addTimer.Size = new System.Drawing.Size(110, 23);
            this.button_addTimer.TabIndex = 2;
            this.button_addTimer.Text = "Add";
            this.button_addTimer.UseVisualStyleBackColor = true;
            this.button_addTimer.Click += new System.EventHandler(this.button_addTimer_Click);
            // 
            // button_presets
            // 
            this.button_presets.Location = new System.Drawing.Point(122, 125);
            this.button_presets.Name = "button_presets";
            this.button_presets.Size = new System.Drawing.Size(110, 23);
            this.button_presets.TabIndex = 3;
            this.button_presets.Text = "Preset";
            this.button_presets.UseVisualStyleBackColor = true;
            this.button_presets.MouseClick += new System.Windows.Forms.MouseEventHandler(this.button_presets_MouseClick);
            // 
            // contextMenuStrip_presets
            // 
            this.contextMenuStrip_presets.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bedmageToolStripMenuItem,
            this.idleToolStripMenuItem,
            this.food002000ToolStripMenuItem});
            this.contextMenuStrip_presets.Name = "timers_contextMenuStrip";
            this.contextMenuStrip_presets.Size = new System.Drawing.Size(178, 70);
            this.contextMenuStrip_presets.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip_presets_Opening);
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
            // Tab_Timers
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "Tab_Timers";
            this.Size = new System.Drawing.Size(737, 400);
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.groupBox_settings.ResumeLayout(false);
            this.groupBox_settings.PerformLayout();
            this.contextMenuStrip_presets.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.TextBox textBox_info;
        private System.Windows.Forms.GroupBox groupBox_settings;
        private System.Windows.Forms.CheckBox checkBox_trayOpenOnClick;
        private System.Windows.Forms.TextBox textBox_soundPath;
        private System.Windows.Forms.Button button_browseSound;
        private System.Windows.Forms.CheckBox checkBox_playSound;
        private System.Windows.Forms.CheckBox checkBox_trayNotification;
        private System.Windows.Forms.Button button_addTimer;
        private System.Windows.Forms.Button button_presets;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_presets;
        private System.Windows.Forms.ToolStripMenuItem bedmageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem idleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem food002000ToolStripMenuItem;
    }
}
