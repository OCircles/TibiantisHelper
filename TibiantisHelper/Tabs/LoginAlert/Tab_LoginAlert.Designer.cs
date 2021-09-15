
namespace TibiantisHelper.Tabs
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
            this.splitContainer10 = new System.Windows.Forms.SplitContainer();
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
            this.loginAlert_dataGridView = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer10)).BeginInit();
            this.splitContainer10.Panel1.SuspendLayout();
            this.splitContainer10.Panel2.SuspendLayout();
            this.splitContainer10.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer11)).BeginInit();
            this.splitContainer11.Panel1.SuspendLayout();
            this.splitContainer11.Panel2.SuspendLayout();
            this.splitContainer11.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.loginAlert_dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer10
            // 
            this.splitContainer10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer10.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer10.Location = new System.Drawing.Point(0, 0);
            this.splitContainer10.Name = "splitContainer10";
            // 
            // splitContainer10.Panel1
            // 
            this.splitContainer10.Panel1.Controls.Add(this.splitContainer11);
            // 
            // splitContainer10.Panel2
            // 
            this.splitContainer10.Panel2.Controls.Add(this.loginAlert_dataGridView);
            this.splitContainer10.Size = new System.Drawing.Size(859, 487);
            this.splitContainer10.SplitterDistance = 262;
            this.splitContainer10.TabIndex = 1;
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
            this.splitContainer11.Size = new System.Drawing.Size(262, 487);
            this.splitContainer11.SplitterDistance = 157;
            this.splitContainer11.TabIndex = 2;
            // 
            // label_selectedPlayer
            // 
            this.label_selectedPlayer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label_selectedPlayer.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_selectedPlayer.Location = new System.Drawing.Point(4, 106);
            this.label_selectedPlayer.Name = "label_selectedPlayer";
            this.label_selectedPlayer.Size = new System.Drawing.Size(259, 18);
            this.label_selectedPlayer.TabIndex = 3;
            this.label_selectedPlayer.Text = "No player selected";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.loginAlert_button_browseSound);
            this.groupBox1.Controls.Add(this.loginAlert_alertTextBox);
            this.groupBox1.Controls.Add(this.checkBox_notifSound);
            this.groupBox1.Controls.Add(this.checkBox_notifTray);
            this.groupBox1.Location = new System.Drawing.Point(4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(255, 99);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Settings";
            // 
            // loginAlert_button_browseSound
            // 
            this.loginAlert_button_browseSound.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.loginAlert_button_browseSound.Location = new System.Drawing.Point(192, 68);
            this.loginAlert_button_browseSound.Name = "loginAlert_button_browseSound";
            this.loginAlert_button_browseSound.Size = new System.Drawing.Size(57, 23);
            this.loginAlert_button_browseSound.TabIndex = 3;
            this.loginAlert_button_browseSound.Text = "Browse";
            this.loginAlert_button_browseSound.UseVisualStyleBackColor = true;
            this.loginAlert_button_browseSound.Click += new System.EventHandler(this.button_browseSound_Click);
            // 
            // loginAlert_alertTextBox
            // 
            this.loginAlert_alertTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.loginAlert_alertTextBox.Location = new System.Drawing.Point(7, 69);
            this.loginAlert_alertTextBox.Name = "loginAlert_alertTextBox";
            this.loginAlert_alertTextBox.ReadOnly = true;
            this.loginAlert_alertTextBox.Size = new System.Drawing.Size(179, 21);
            this.loginAlert_alertTextBox.TabIndex = 2;
            // 
            // checkBox_notifSound
            // 
            this.checkBox_notifSound.AutoSize = true;
            this.checkBox_notifSound.Checked = global::TibiantisHelper.Properties.Settings.Default.LoginAlertNotifSound;
            this.checkBox_notifSound.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::TibiantisHelper.Properties.Settings.Default, "LoginAlertNotifSound", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.checkBox_notifSound.Location = new System.Drawing.Point(7, 45);
            this.checkBox_notifSound.Name = "checkBox_notifSound";
            this.checkBox_notifSound.Size = new System.Drawing.Size(169, 17);
            this.checkBox_notifSound.TabIndex = 1;
            this.checkBox_notifSound.Text = "Default alerts play sound";
            this.checkBox_notifSound.UseVisualStyleBackColor = true;
            this.checkBox_notifSound.CheckedChanged += new System.EventHandler(this.checkBox_notifSound_CheckedChanged);
            // 
            // checkBox_notifTray
            // 
            this.checkBox_notifTray.AutoSize = true;
            this.checkBox_notifTray.Checked = global::TibiantisHelper.Properties.Settings.Default.LoginAlertNotifTray;
            this.checkBox_notifTray.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_notifTray.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::TibiantisHelper.Properties.Settings.Default, "LoginAlertNotifTray", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.checkBox_notifTray.Location = new System.Drawing.Point(7, 21);
            this.checkBox_notifTray.Name = "checkBox_notifTray";
            this.checkBox_notifTray.Size = new System.Drawing.Size(118, 17);
            this.checkBox_notifTray.TabIndex = 0;
            this.checkBox_notifTray.Text = "Tray Notification";
            this.checkBox_notifTray.UseVisualStyleBackColor = true;
            this.checkBox_notifTray.CheckedChanged += new System.EventHandler(this.checkBox_notifTray_CheckedChanged);
            // 
            // button_add
            // 
            this.button_add.Location = new System.Drawing.Point(4, 127);
            this.button_add.Name = "button_add";
            this.button_add.Size = new System.Drawing.Size(118, 23);
            this.button_add.TabIndex = 0;
            this.button_add.Text = "Add Player";
            this.button_add.UseVisualStyleBackColor = true;
            this.button_add.Click += new System.EventHandler(this.button_add_Click);
            // 
            // button_remove
            // 
            this.button_remove.Location = new System.Drawing.Point(141, 127);
            this.button_remove.Name = "button_remove";
            this.button_remove.Size = new System.Drawing.Size(118, 23);
            this.button_remove.TabIndex = 1;
            this.button_remove.Text = "Remove Selected";
            this.button_remove.UseVisualStyleBackColor = true;
            this.button_remove.Click += new System.EventHandler(this.button_remove_Click);
            // 
            // textBox11
            // 
            this.textBox11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox11.Location = new System.Drawing.Point(0, 0);
            this.textBox11.Multiline = true;
            this.textBox11.Name = "textBox11";
            this.textBox11.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox11.Size = new System.Drawing.Size(262, 326);
            this.textBox11.TabIndex = 0;
            this.textBox11.Text = resources.GetString("textBox11.Text");
            // 
            // loginAlert_dataGridView
            // 
            this.loginAlert_dataGridView.AllowUserToAddRows = false;
            this.loginAlert_dataGridView.AllowUserToDeleteRows = false;
            this.loginAlert_dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.loginAlert_dataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.loginAlert_dataGridView.Location = new System.Drawing.Point(0, 0);
            this.loginAlert_dataGridView.MultiSelect = false;
            this.loginAlert_dataGridView.Name = "loginAlert_dataGridView";
            this.loginAlert_dataGridView.Size = new System.Drawing.Size(593, 487);
            this.loginAlert_dataGridView.TabIndex = 0;
            this.loginAlert_dataGridView.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.loginAlert_dataGridView_CellBeginEdit);
            this.loginAlert_dataGridView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.loginAlert_dataGridView_CellDoubleClick);
            this.loginAlert_dataGridView.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.loginAlert_dataGridView_CellEndEdit);
            this.loginAlert_dataGridView.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.loginAlert_dataGridView_CellFormatting);
            this.loginAlert_dataGridView.ColumnAdded += new System.Windows.Forms.DataGridViewColumnEventHandler(this.loginAlert_dataGridView_ColumnAdded);
            this.loginAlert_dataGridView.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.loginAlert_dataGridView_DataBindingComplete);
            this.loginAlert_dataGridView.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.loginAlert_dataGridView_RowsAdded);
            this.loginAlert_dataGridView.SelectionChanged += new System.EventHandler(this.loginAlert_dataGridView_SelectionChanged);
            this.loginAlert_dataGridView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.loginAlert_dataGridView_KeyDown);
            this.loginAlert_dataGridView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.loginAlert_dataGridView_MouseDown);
            // 
            // Tab_LoginAlert
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer10);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "Tab_LoginAlert";
            this.Size = new System.Drawing.Size(859, 487);
            this.Load += new System.EventHandler(this.Tab_LoginAlert_Load);
            this.splitContainer10.Panel1.ResumeLayout(false);
            this.splitContainer10.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer10)).EndInit();
            this.splitContainer10.ResumeLayout(false);
            this.splitContainer11.Panel1.ResumeLayout(false);
            this.splitContainer11.Panel2.ResumeLayout(false);
            this.splitContainer11.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer11)).EndInit();
            this.splitContainer11.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.loginAlert_dataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer10;
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
        private System.Windows.Forms.DataGridView loginAlert_dataGridView;
    }
}
