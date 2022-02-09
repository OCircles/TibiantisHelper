
namespace TibiantisHelper.Tabs.LoginAlert
{
    partial class Control_TrackedPlayerGroup
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
            this.label_name = new System.Windows.Forms.Label();
            this.checkBox_trayNotif = new System.Windows.Forms.CheckBox();
            this.checkBox_soundEnabled = new System.Windows.Forms.CheckBox();
            this.textBox_soundPath = new System.Windows.Forms.TextBox();
            this.button_browseSound = new System.Windows.Forms.Button();
            this.panel_header = new System.Windows.Forms.Panel();
            this.checkBox_minimize = new System.Windows.Forms.CheckBox();
            this.button_save = new System.Windows.Forms.Button();
            this.button_deleteGroup = new System.Windows.Forms.Button();
            this.button_edit = new System.Windows.Forms.Button();
            this.label_playerControl = new System.Windows.Forms.Label();
            this.panel_playerControls = new System.Windows.Forms.Panel();
            this.button_addPlayer = new System.Windows.Forms.Button();
            this.button_deletePlayer = new System.Windows.Forms.Button();
            this.button_icon = new System.Windows.Forms.Button();
            this.objectListView1 = new BrightIdeasSoftware.ObjectListView();
            this.panel_main = new System.Windows.Forms.Panel();
            this.panel_header.SuspendLayout();
            this.panel_playerControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.objectListView1)).BeginInit();
            this.panel_main.SuspendLayout();
            this.SuspendLayout();
            // 
            // label_name
            // 
            this.label_name.AutoSize = true;
            this.label_name.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_name.Location = new System.Drawing.Point(3, 5);
            this.label_name.Name = "label_name";
            this.label_name.Size = new System.Drawing.Size(101, 18);
            this.label_name.TabIndex = 0;
            this.label_name.Text = "Group Name";
            // 
            // checkBox_trayNotif
            // 
            this.checkBox_trayNotif.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBox_trayNotif.AutoSize = true;
            this.checkBox_trayNotif.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox_trayNotif.Location = new System.Drawing.Point(6, 74);
            this.checkBox_trayNotif.Name = "checkBox_trayNotif";
            this.checkBox_trayNotif.Size = new System.Drawing.Size(84, 17);
            this.checkBox_trayNotif.TabIndex = 7;
            this.checkBox_trayNotif.Text = "Tray Notif.";
            this.checkBox_trayNotif.UseVisualStyleBackColor = true;
            this.checkBox_trayNotif.CheckedChanged += new System.EventHandler(this.checkBox_trayNotif_CheckedChanged);
            // 
            // checkBox_soundEnabled
            // 
            this.checkBox_soundEnabled.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBox_soundEnabled.AutoSize = true;
            this.checkBox_soundEnabled.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox_soundEnabled.Location = new System.Drawing.Point(105, 74);
            this.checkBox_soundEnabled.Name = "checkBox_soundEnabled";
            this.checkBox_soundEnabled.Size = new System.Drawing.Size(62, 17);
            this.checkBox_soundEnabled.TabIndex = 8;
            this.checkBox_soundEnabled.Text = "Sound";
            this.checkBox_soundEnabled.UseVisualStyleBackColor = true;
            this.checkBox_soundEnabled.CheckedChanged += new System.EventHandler(this.checkBox_soundEnabled_CheckedChanged);
            // 
            // textBox_soundPath
            // 
            this.textBox_soundPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBox_soundPath.Location = new System.Drawing.Point(186, 72);
            this.textBox_soundPath.Name = "textBox_soundPath";
            this.textBox_soundPath.ReadOnly = true;
            this.textBox_soundPath.Size = new System.Drawing.Size(157, 21);
            this.textBox_soundPath.TabIndex = 9;
            this.textBox_soundPath.TextChanged += new System.EventHandler(this.textBox_soundPath_TextChanged);
            // 
            // button_browseSound
            // 
            this.button_browseSound.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button_browseSound.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_browseSound.Location = new System.Drawing.Point(349, 71);
            this.button_browseSound.Name = "button_browseSound";
            this.button_browseSound.Size = new System.Drawing.Size(87, 22);
            this.button_browseSound.TabIndex = 10;
            this.button_browseSound.Text = "Browse";
            this.button_browseSound.UseVisualStyleBackColor = true;
            this.button_browseSound.Click += new System.EventHandler(this.button_browseSound_Click);
            // 
            // panel_header
            // 
            this.panel_header.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel_header.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.panel_header.Controls.Add(this.checkBox_minimize);
            this.panel_header.Controls.Add(this.label_name);
            this.panel_header.Controls.Add(this.button_save);
            this.panel_header.Controls.Add(this.button_deleteGroup);
            this.panel_header.Controls.Add(this.button_edit);
            this.panel_header.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel_header.Location = new System.Drawing.Point(3, 6);
            this.panel_header.Name = "panel_header";
            this.panel_header.Size = new System.Drawing.Size(668, 30);
            this.panel_header.TabIndex = 11;
            // 
            // checkBox_minimize
            // 
            this.checkBox_minimize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBox_minimize.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkBox_minimize.AutoSize = true;
            this.checkBox_minimize.FlatAppearance.BorderSize = 0;
            this.checkBox_minimize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.checkBox_minimize.Image = global::TibiantisHelper.Properties.Resources.view;
            this.checkBox_minimize.Location = new System.Drawing.Point(533, 0);
            this.checkBox_minimize.Name = "checkBox_minimize";
            this.checkBox_minimize.Size = new System.Drawing.Size(30, 30);
            this.checkBox_minimize.TabIndex = 8;
            this.checkBox_minimize.UseVisualStyleBackColor = true;
            this.checkBox_minimize.CheckedChanged += new System.EventHandler(this.checkBox_minimized_CheckedChanged);
            // 
            // button_save
            // 
            this.button_save.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_save.FlatAppearance.BorderSize = 0;
            this.button_save.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_save.Image = global::TibiantisHelper.Properties.Resources.save;
            this.button_save.Location = new System.Drawing.Point(566, 3);
            this.button_save.Margin = new System.Windows.Forms.Padding(0);
            this.button_save.Name = "button_save";
            this.button_save.Size = new System.Drawing.Size(28, 24);
            this.button_save.TabIndex = 6;
            this.button_save.UseVisualStyleBackColor = true;
            this.button_save.Click += new System.EventHandler(this.button_save_Click);
            // 
            // button_deleteGroup
            // 
            this.button_deleteGroup.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_deleteGroup.FlatAppearance.BorderSize = 0;
            this.button_deleteGroup.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_deleteGroup.Image = global::TibiantisHelper.Properties.Resources.delete;
            this.button_deleteGroup.Location = new System.Drawing.Point(636, 3);
            this.button_deleteGroup.Name = "button_deleteGroup";
            this.button_deleteGroup.Size = new System.Drawing.Size(28, 24);
            this.button_deleteGroup.TabIndex = 4;
            this.button_deleteGroup.UseVisualStyleBackColor = true;
            this.button_deleteGroup.Click += new System.EventHandler(this.button_deleteGroup_Click);
            // 
            // button_edit
            // 
            this.button_edit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_edit.FlatAppearance.BorderSize = 0;
            this.button_edit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_edit.Image = global::TibiantisHelper.Properties.Resources.edit;
            this.button_edit.Location = new System.Drawing.Point(601, 3);
            this.button_edit.Name = "button_edit";
            this.button_edit.Size = new System.Drawing.Size(28, 24);
            this.button_edit.TabIndex = 5;
            this.button_edit.UseVisualStyleBackColor = true;
            this.button_edit.Click += new System.EventHandler(this.button_edit_Click);
            // 
            // label_playerControl
            // 
            this.label_playerControl.AutoSize = true;
            this.label_playerControl.Location = new System.Drawing.Point(2, 2);
            this.label_playerControl.Name = "label_playerControl";
            this.label_playerControl.Size = new System.Drawing.Size(49, 13);
            this.label_playerControl.TabIndex = 13;
            this.label_playerControl.Text = "Players";
            // 
            // panel_playerControls
            // 
            this.panel_playerControls.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.panel_playerControls.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel_playerControls.Controls.Add(this.label_playerControl);
            this.panel_playerControls.Controls.Add(this.button_addPlayer);
            this.panel_playerControls.Controls.Add(this.button_deletePlayer);
            this.panel_playerControls.Location = new System.Drawing.Point(467, 71);
            this.panel_playerControls.Name = "panel_playerControls";
            this.panel_playerControls.Size = new System.Drawing.Size(108, 22);
            this.panel_playerControls.TabIndex = 14;
            // 
            // button_addPlayer
            // 
            this.button_addPlayer.FlatAppearance.BorderSize = 0;
            this.button_addPlayer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_addPlayer.Image = global::TibiantisHelper.Properties.Resources.add16;
            this.button_addPlayer.Location = new System.Drawing.Point(54, -1);
            this.button_addPlayer.Margin = new System.Windows.Forms.Padding(0);
            this.button_addPlayer.Name = "button_addPlayer";
            this.button_addPlayer.Size = new System.Drawing.Size(18, 18);
            this.button_addPlayer.TabIndex = 7;
            this.button_addPlayer.UseVisualStyleBackColor = true;
            this.button_addPlayer.Click += new System.EventHandler(this.button_addPlayer_Click);
            // 
            // button_deletePlayer
            // 
            this.button_deletePlayer.FlatAppearance.BorderSize = 0;
            this.button_deletePlayer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_deletePlayer.Image = global::TibiantisHelper.Properties.Resources.delete16;
            this.button_deletePlayer.Location = new System.Drawing.Point(75, -1);
            this.button_deletePlayer.Margin = new System.Windows.Forms.Padding(0);
            this.button_deletePlayer.Name = "button_deletePlayer";
            this.button_deletePlayer.Size = new System.Drawing.Size(18, 18);
            this.button_deletePlayer.TabIndex = 12;
            this.button_deletePlayer.UseVisualStyleBackColor = true;
            this.button_deletePlayer.Click += new System.EventHandler(this.button_deletePlayer_Click);
            // 
            // button_icon
            // 
            this.button_icon.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.button_icon.FlatAppearance.BorderSize = 0;
            this.button_icon.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_icon.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.button_icon.Location = new System.Drawing.Point(0, 0);
            this.button_icon.MaximumSize = new System.Drawing.Size(73, 65);
            this.button_icon.MinimumSize = new System.Drawing.Size(73, 65);
            this.button_icon.Name = "button_icon";
            this.button_icon.Size = new System.Drawing.Size(73, 65);
            this.button_icon.TabIndex = 15;
            this.button_icon.UseVisualStyleBackColor = true;
            this.button_icon.MouseClick += new System.Windows.Forms.MouseEventHandler(this.button_icon_MouseClick);
            // 
            // objectListView1
            // 
            this.objectListView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.objectListView1.CellEditUseWholeCell = false;
            this.objectListView1.Cursor = System.Windows.Forms.Cursors.Default;
            this.objectListView1.FullRowSelect = true;
            this.objectListView1.GridLines = true;
            this.objectListView1.HideSelection = false;
            this.objectListView1.Location = new System.Drawing.Point(79, 0);
            this.objectListView1.Name = "objectListView1";
            this.objectListView1.ShowGroups = false;
            this.objectListView1.Size = new System.Drawing.Size(589, 65);
            this.objectListView1.TabIndex = 16;
            this.objectListView1.UseCompatibleStateImageBehavior = false;
            this.objectListView1.UseHotControls = false;
            this.objectListView1.View = System.Windows.Forms.View.Details;
            this.objectListView1.CellEditValidating += new BrightIdeasSoftware.CellEditEventHandler(this.objectListView1_CellEditValidating);
            // 
            // panel_main
            // 
            this.panel_main.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel_main.Controls.Add(this.checkBox_trayNotif);
            this.panel_main.Controls.Add(this.checkBox_soundEnabled);
            this.panel_main.Controls.Add(this.textBox_soundPath);
            this.panel_main.Controls.Add(this.button_browseSound);
            this.panel_main.Controls.Add(this.panel_playerControls);
            this.panel_main.Controls.Add(this.button_icon);
            this.panel_main.Controls.Add(this.objectListView1);
            this.panel_main.Location = new System.Drawing.Point(3, 42);
            this.panel_main.Name = "panel_main";
            this.panel_main.Size = new System.Drawing.Size(668, 96);
            this.panel_main.TabIndex = 17;
            // 
            // Control_TrackedPlayerGroup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel_main);
            this.Controls.Add(this.panel_header);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "Control_TrackedPlayerGroup";
            this.Size = new System.Drawing.Size(677, 150);
            this.Load += new System.EventHandler(this.Control_TrackedPlayerGroup_Load);
            this.panel_header.ResumeLayout(false);
            this.panel_header.PerformLayout();
            this.panel_playerControls.ResumeLayout(false);
            this.panel_playerControls.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.objectListView1)).EndInit();
            this.panel_main.ResumeLayout(false);
            this.panel_main.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label_name;
        private System.Windows.Forms.Button button_deleteGroup;
        private System.Windows.Forms.Button button_edit;
        private System.Windows.Forms.Button button_save;
        private System.Windows.Forms.TextBox textBox_soundPath;
        private System.Windows.Forms.Button button_browseSound;
        private System.Windows.Forms.Panel panel_header;
        private System.Windows.Forms.Button button_addPlayer;
        private System.Windows.Forms.Button button_deletePlayer;
        private System.Windows.Forms.Label label_playerControl;
        private System.Windows.Forms.Panel panel_playerControls;
        private System.Windows.Forms.Button button_icon;
        private BrightIdeasSoftware.ObjectListView objectListView1;
        private System.Windows.Forms.Panel panel_main;
        private System.Windows.Forms.CheckBox checkBox_trayNotif;
        private System.Windows.Forms.CheckBox checkBox_soundEnabled;
        private System.Windows.Forms.CheckBox checkBox_minimize;
    }
}
