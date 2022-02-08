
namespace TibiantisHelper
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
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader_blank = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button_browseSound = new System.Windows.Forms.Button();
            this.panel_header = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button_addPlayer = new System.Windows.Forms.Button();
            this.button_deletePlayer = new System.Windows.Forms.Button();
            this.button_save = new System.Windows.Forms.Button();
            this.button_deleteGroup = new System.Windows.Forms.Button();
            this.button_edit = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel_header.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label_name
            // 
            this.label_name.AutoSize = true;
            this.label_name.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_name.Location = new System.Drawing.Point(3, 5);
            this.label_name.Name = "label_name";
            this.label_name.Size = new System.Drawing.Size(110, 18);
            this.label_name.TabIndex = 0;
            this.label_name.Text = "Group Name";
            // 
            // listView1
            // 
            this.listView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView1.AutoArrange = false;
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader_blank,
            this.columnHeader_name});
            this.listView1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listView1.FullRowSelect = true;
            this.listView1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listView1.HideSelection = false;
            this.listView1.LabelWrap = false;
            this.listView1.Location = new System.Drawing.Point(83, 42);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(588, 64);
            this.listView1.TabIndex = 3;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader_blank
            // 
            this.columnHeader_blank.DisplayIndex = 1;
            this.columnHeader_blank.Text = "";
            this.columnHeader_blank.Width = 0;
            // 
            // columnHeader_name
            // 
            this.columnHeader_name.DisplayIndex = 0;
            this.columnHeader_name.Text = "Name";
            this.columnHeader_name.Width = 209;
            // 
            // checkBox1
            // 
            this.checkBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBox1.AutoSize = true;
            this.checkBox1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox1.Location = new System.Drawing.Point(3, 114);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(84, 17);
            this.checkBox1.TabIndex = 7;
            this.checkBox1.Text = "Tray Notif.";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            this.checkBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBox2.AutoSize = true;
            this.checkBox2.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox2.Location = new System.Drawing.Point(106, 114);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(62, 17);
            this.checkBox2.TabIndex = 8;
            this.checkBox2.Text = "Sound";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBox1.Enabled = false;
            this.textBox1.Location = new System.Drawing.Point(184, 112);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(157, 21);
            this.textBox1.TabIndex = 9;
            // 
            // button_browseSound
            // 
            this.button_browseSound.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button_browseSound.Enabled = false;
            this.button_browseSound.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_browseSound.Location = new System.Drawing.Point(353, 111);
            this.button_browseSound.Name = "button_browseSound";
            this.button_browseSound.Size = new System.Drawing.Size(87, 22);
            this.button_browseSound.TabIndex = 10;
            this.button_browseSound.Text = "Browse";
            this.button_browseSound.UseVisualStyleBackColor = true;
            // 
            // panel_header
            // 
            this.panel_header.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel_header.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
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
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(2, 2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "Players";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.button_addPlayer);
            this.panel1.Controls.Add(this.button_deletePlayer);
            this.panel1.Location = new System.Drawing.Point(469, 111);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(108, 22);
            this.panel1.TabIndex = 14;
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
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox1.Location = new System.Drawing.Point(3, 42);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(74, 64);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // Control_TrackedPlayerGroup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel_header);
            this.Controls.Add(this.button_browseSound);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.checkBox2);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.pictureBox1);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "Control_TrackedPlayerGroup";
            this.Size = new System.Drawing.Size(677, 145);
            this.panel_header.ResumeLayout(false);
            this.panel_header.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label_name;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeader_blank;
        private System.Windows.Forms.ColumnHeader columnHeader_name;
        private System.Windows.Forms.Button button_deleteGroup;
        private System.Windows.Forms.Button button_edit;
        private System.Windows.Forms.Button button_save;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button_browseSound;
        private System.Windows.Forms.Panel panel_header;
        private System.Windows.Forms.Button button_addPlayer;
        private System.Windows.Forms.Button button_deletePlayer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
    }
}
