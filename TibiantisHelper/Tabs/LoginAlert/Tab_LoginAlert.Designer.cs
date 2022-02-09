
namespace TibiantisHelper.Tabs.LoginAlert
{
    partial class Tab_LoginAlert
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Tab_LoginAlert));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer11 = new System.Windows.Forms.SplitContainer();
            this.label_selectedPlayer = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.loginAlert_button_browseSound = new System.Windows.Forms.Button();
            this.loginAlert_alertTextBox = new System.Windows.Forms.TextBox();
            this.checkBox_notifSound = new System.Windows.Forms.CheckBox();
            this.checkBox_notifTray = new System.Windows.Forms.CheckBox();
            this.button_add = new System.Windows.Forms.Button();
            this.button_remove = new System.Windows.Forms.Button();
            this.textBox11 = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer11)).BeginInit();
            this.splitContainer11.Panel1.SuspendLayout();
            this.splitContainer11.Panel2.SuspendLayout();
            this.splitContainer11.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer10
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer10";
            // 
            // splitContainer10.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer11);
            // 
            // splitContainer10.Panel2
            // 
            this.splitContainer1.Panel2.AutoScroll = true;
            this.splitContainer1.Size = new System.Drawing.Size(1095, 485);
            this.splitContainer1.SplitterDistance = 262;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 2;
            // 
            // splitContainer11
            // 
            this.splitContainer11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer11.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer11.Location = new System.Drawing.Point(0, 0);
            this.splitContainer11.Name = "splitContainer11";
            this.splitContainer11.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer11.Panel1
            // 
            this.splitContainer11.Panel1.Controls.Add(this.label_selectedPlayer);
            this.splitContainer11.Panel1.Controls.Add(this.groupBox1);
            this.splitContainer11.Panel1.Controls.Add(this.button_add);
            this.splitContainer11.Panel1.Controls.Add(this.button_remove);
            // 
            // splitContainer11.Panel2
            // 
            this.splitContainer11.Panel2.Controls.Add(this.textBox11);
            this.splitContainer11.Size = new System.Drawing.Size(262, 485);
            this.splitContainer11.SplitterDistance = 157;
            this.splitContainer11.TabIndex = 2;
            // 
            // label_selectedPlayer
            // 
            this.label_selectedPlayer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label_selectedPlayer.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_selectedPlayer.Location = new System.Drawing.Point(5, 106);
            this.label_selectedPlayer.Name = "label_selectedPlayer";
            this.label_selectedPlayer.Size = new System.Drawing.Size(254, 18);
            this.label_selectedPlayer.TabIndex = 3;
            this.label_selectedPlayer.Text = "No player selected";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.loginAlert_button_browseSound);
            this.groupBox1.Controls.Add(this.loginAlert_alertTextBox);
            this.groupBox1.Controls.Add(this.checkBox_notifSound);
            this.groupBox1.Controls.Add(this.checkBox_notifTray);
            this.groupBox1.Location = new System.Drawing.Point(5, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(254, 99);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Settings";
            // 
            // loginAlert_button_browseSound
            // 
            this.loginAlert_button_browseSound.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.loginAlert_button_browseSound.Location = new System.Drawing.Point(149, 67);
            this.loginAlert_button_browseSound.Name = "loginAlert_button_browseSound";
            this.loginAlert_button_browseSound.Size = new System.Drawing.Size(99, 23);
            this.loginAlert_button_browseSound.TabIndex = 3;
            this.loginAlert_button_browseSound.Text = "Browse";
            this.loginAlert_button_browseSound.UseVisualStyleBackColor = true;
            // 
            // loginAlert_alertTextBox
            // 
            this.loginAlert_alertTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.loginAlert_alertTextBox.Location = new System.Drawing.Point(8, 72);
            this.loginAlert_alertTextBox.Name = "loginAlert_alertTextBox";
            this.loginAlert_alertTextBox.ReadOnly = true;
            this.loginAlert_alertTextBox.Size = new System.Drawing.Size(135, 21);
            this.loginAlert_alertTextBox.TabIndex = 2;
            // 
            // checkBox_notifSound
            // 
            this.checkBox_notifSound.AutoSize = true;
            this.checkBox_notifSound.Checked = global::TibiantisHelper.Properties.Settings.Default.LoginAlertNotifSound;
            this.checkBox_notifSound.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::TibiantisHelper.Properties.Settings.Default, "LoginAlertNotifSound", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.checkBox_notifSound.Location = new System.Drawing.Point(8, 45);
            this.checkBox_notifSound.Name = "checkBox_notifSound";
            this.checkBox_notifSound.Size = new System.Drawing.Size(169, 17);
            this.checkBox_notifSound.TabIndex = 1;
            this.checkBox_notifSound.Text = "Default alerts play sound";
            this.checkBox_notifSound.UseVisualStyleBackColor = true;
            // 
            // checkBox_notifTray
            // 
            this.checkBox_notifTray.AutoSize = true;
            this.checkBox_notifTray.Checked = global::TibiantisHelper.Properties.Settings.Default.LoginAlertNotifTray;
            this.checkBox_notifTray.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_notifTray.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::TibiantisHelper.Properties.Settings.Default, "LoginAlertNotifTray", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.checkBox_notifTray.Location = new System.Drawing.Point(8, 21);
            this.checkBox_notifTray.Name = "checkBox_notifTray";
            this.checkBox_notifTray.Size = new System.Drawing.Size(118, 17);
            this.checkBox_notifTray.TabIndex = 0;
            this.checkBox_notifTray.Text = "Tray Notification";
            this.checkBox_notifTray.UseVisualStyleBackColor = true;
            // 
            // button_add
            // 
            this.button_add.Location = new System.Drawing.Point(5, 127);
            this.button_add.Name = "button_add";
            this.button_add.Size = new System.Drawing.Size(116, 23);
            this.button_add.TabIndex = 0;
            this.button_add.Text = "Add Player";
            this.button_add.UseVisualStyleBackColor = true;
            this.button_add.Click += new System.EventHandler(this.button_add_Click);
            // 
            // button_remove
            // 
            this.button_remove.Location = new System.Drawing.Point(142, 131);
            this.button_remove.Name = "button_remove";
            this.button_remove.Size = new System.Drawing.Size(117, 23);
            this.button_remove.TabIndex = 1;
            this.button_remove.Text = "Remove Selected";
            this.button_remove.UseVisualStyleBackColor = true;
            // 
            // textBox11
            // 
            this.textBox11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox11.Location = new System.Drawing.Point(0, 0);
            this.textBox11.Multiline = true;
            this.textBox11.Name = "textBox11";
            this.textBox11.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox11.Size = new System.Drawing.Size(262, 324);
            this.textBox11.TabIndex = 0;
            this.textBox11.Text = resources.GetString("textBox11.Text");
            // 
            // Tab_LoginAlertNew
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "Tab_LoginAlertNew";
            this.Size = new System.Drawing.Size(1095, 485);
            this.splitContainer1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer11.Panel1.ResumeLayout(false);
            this.splitContainer11.Panel2.ResumeLayout(false);
            this.splitContainer11.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer11)).EndInit();
            this.splitContainer11.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer11;
        private System.Windows.Forms.Label label_selectedPlayer;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button loginAlert_button_browseSound;
        private System.Windows.Forms.TextBox loginAlert_alertTextBox;
        private System.Windows.Forms.CheckBox checkBox_notifSound;
        private System.Windows.Forms.CheckBox checkBox_notifTray;
        private System.Windows.Forms.Button button_add;
        private System.Windows.Forms.Button button_remove;
        private System.Windows.Forms.TextBox textBox11;
    }
}
